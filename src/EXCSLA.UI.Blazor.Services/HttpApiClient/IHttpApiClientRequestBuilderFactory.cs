namespace EXCSLA.UI.Blazor.Services
{
    public interface IHttpApiClientRequestBuilderFactory
    {
        HttpApiClientRequestBuilder Create(string url);
    }
}