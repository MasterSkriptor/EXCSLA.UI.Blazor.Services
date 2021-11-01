using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace EXCSLA.UI.Blazor.Services
{
    public class HttpApiClientRequestBuilderFactory : IHttpApiClientRequestBuilderFactory
    {
        private readonly NavigationManager _navigationManager;
        private readonly HttpClient _httpClient;
        private readonly IAlertService _alertService;

        public HttpApiClientRequestBuilderFactory(NavigationManager navigationManager, HttpClient httpClient, IAlertService alertService)
        {
            _navigationManager = navigationManager;
            _httpClient = httpClient;
            _alertService = alertService;
        }

        public HttpApiClientRequestBuilder Create(string url)
        {
            return new HttpApiClientRequestBuilder(_navigationManager, url, _httpClient, _alertService);
        }
    }
}
