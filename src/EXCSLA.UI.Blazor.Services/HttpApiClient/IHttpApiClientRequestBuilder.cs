using System;
using System.Threading.Tasks;

namespace EXCSLA.UI.Blazor.Services
{
    public interface IHttpApiClientRequestBuilder
    {
        Task Get(object data = null);
        HttpApiClientRequestBuilder OnBadRequest(Action todo);
        HttpApiClientRequestBuilder OnBadRequest(Func<Task> todo);
        HttpApiClientRequestBuilder OnBadRequest<T>(Action<T> todo);
        HttpApiClientRequestBuilder OnOK(Action todo);
        HttpApiClientRequestBuilder OnOK(Func<Task> todo);
        HttpApiClientRequestBuilder OnOK<T>(Action<T> todo);
        HttpApiClientRequestBuilder OnOK(string successMessage, string navigateTo = null);
        Task Post();
        Task Post(byte[] data);
        Task Post<T>(T data);
        Task Put(byte[] data);
        Task Put<T>(T data);
        Task Delete(byte[] data);
        Task Delete<T>(T data);
        Task Delete();
        void SetHeader(string key, string value);
    }
}