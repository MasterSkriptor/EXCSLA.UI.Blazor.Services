using EXCSLA.Shared.Core.ValueObjects.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace EXCSLA.UI.Blazor.Services
{
    public class HttpApiClientRequestBuilder : IHttpApiClientRequestBuilder
    {
        private readonly NavigationManager _navigationManager;
        private readonly HttpClient _httpClient;
        private readonly IAlertService _alertService;
        private readonly string _uri;
        private Func<HttpResponseMessage, Task> _onBadRequest;
        private Func<HttpResponseMessage, Task> _onOK;


        public HttpApiClientRequestBuilder(NavigationManager navigationManager, string uri, HttpClient httpClient,
            IAlertService alertService)
        {
            _uri = navigationManager.ToAbsoluteUri(uri).ToString();
            _navigationManager = navigationManager;
            _httpClient = httpClient;
            _alertService = alertService;
        }

        public async Task Get(object data = null)
        {
            var uriBuilder = new UriBuilder(_uri);
            if (data != null)
            {
                uriBuilder.Query = ToQueryString(data);

            }
            await ExecuteHttpQuery(async () =>
                await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString())));
        }

        public async Task Post(byte[] data)
        {
            await ExecuteHttpQuery(async () =>
            {
                return await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, _uri)
                {
                    Content = new ByteArrayContent(data)
                });
            });
        }

        public async Task Post<T>(T data)
        {
            await ExecuteHttpQuery(async () =>
            {
                string requestJson = JsonSerializer.Serialize(data);
                return await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, _uri)
                {
                    Content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json")
                });
            });
        }

        public async Task Post()
        {
            await ExecuteHttpQuery(async () => await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, _uri)));
        }

        public async Task Put(byte[] data)
        {
            await ExecuteHttpQuery(async () =>
            {
                return await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, _uri)
                {
                    Content = new ByteArrayContent(data)
                });
            });
        }

        public async Task Put<T>(T data)
        {
            await ExecuteHttpQuery(async () =>
            {
                string requestJson = JsonSerializer.Serialize(data);
                return await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, _uri)
                {
                    Content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json")
                });
            });
        }

        public async Task Delete(byte[] data)
        {
            await ExecuteHttpQuery(async () =>
            {
                return await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, _uri)
                {
                    Content = new ByteArrayContent(data)
                });
            });
        }

        public async Task Delete<T>(T data)
        {
            await ExecuteHttpQuery(async () =>
            {
                string requestJson = JsonSerializer.Serialize(data);
                return await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, _uri)
                {
                    Content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json")
                });
            });
        }

        public async Task Delete()
        {
            await ExecuteHttpQuery(async () => await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, _uri)));
        }

        public void SetHeader(string key, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(key, value);
        }

        #region Builder Properties

        public HttpApiClientRequestBuilder OnOK<T>(Action<T> todo)
        {
            _onOK = async (HttpResponseMessage r) =>
            {
                T response = await JsonSerializer.DeserializeAsync<T>(await r.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                todo(response);
            };
            return this;
        }

        public HttpApiClientRequestBuilder OnOK(Action todo)
        {
            _onOK = (HttpResponseMessage r) =>
            {
                todo();
                return Task.CompletedTask;
            };
            return this;
        }

        public HttpApiClientRequestBuilder OnOK(Func<Task> todo)
        {
            _onOK = async (HttpResponseMessage r) =>
            {
                await todo();
            };
            return this;
        }

        public HttpApiClientRequestBuilder OnOK(string successMessage, string navigateTo = null)
        {
            OnOK(() =>
            {
                if (!string.IsNullOrEmpty(successMessage))
                    _alertService.AddAlert(successMessage, AlertType.Success);
                if (!string.IsNullOrEmpty(navigateTo))
                    _navigationManager.NavigateTo(navigateTo);
            });
            return this;
        }

        public HttpApiClientRequestBuilder OnBadRequest<T>(Action<T> todo)
        {
            _onBadRequest = async (HttpResponseMessage r) =>
            {
                T response = await JsonSerializer.DeserializeAsync<T>(await r.Content.ReadAsStreamAsync());
                todo(response);
            };
            return this;
        }

        public HttpApiClientRequestBuilder OnBadRequest(Action todo)
        {
            _onBadRequest = (HttpResponseMessage r) =>
            {
                todo();
                return Task.CompletedTask;
            };
            return this;
        }

        public HttpApiClientRequestBuilder OnBadRequest(Func<Task> todo)
        {
            _onBadRequest = async (HttpResponseMessage r) =>
            {
                await todo();
            };
            return this;
        }
        #endregion


        private string ToQueryString(object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType ?
                    valueType.GetGenericArguments()[0] :
                    valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                   Uri.EscapeDataString(x.Key), "=",
                   Uri.EscapeDataString(x.Value.ToString()))));
        }

        private async Task ExecuteHttpQuery(Func<Task<HttpResponseMessage>> httpCall)
        {
            HttpResponseMessage response = null;
            try
            {
                try
                {
                    response = await httpCall();

                }
                catch
                {
                    _alertService.AddAlert("Connection down, server is down or you are not connected to the internet.", AlertType.Danger);
                    throw;
                }
                await HandleHttpResponse(response);
            }
            finally
            {
                response?.Dispose();
            }
        }

        private async Task HandleHttpResponse(HttpResponseMessage response)
        {
            //Console.WriteLine($"HttpResponseMessage called with response: {response.StatusCode.ToString()}");

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                case System.Net.HttpStatusCode.NoContent:
                case System.Net.HttpStatusCode.Created:
                    if (_onOK != null)
                        await _onOK(response);
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    if (_onBadRequest != null)
                        await _onBadRequest(response);
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                case System.Net.HttpStatusCode.Forbidden:
                    break;
                case System.Net.HttpStatusCode.InternalServerError:
                    _alertService.AddAlert("A server error occured, sorry.", AlertType.Danger);
                    break;
                    //other case , we do nothing, I'll add this case as needed
            }
        }
    }
}