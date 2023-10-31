using Simple.OData.Client;
using System.Linq.Expressions;
using BlazorOdata.Shared;

namespace BlazorOdata.Client.Model
{
    public interface IOdataManager 
    {
        string BaseUrlOdata { get; }
        
        ODataClient oDataClient { get; }

        Task<IEnumerable<Template>> GetAllAsync();

        Task<IEnumerable<Template>> GetAllAsync<T>(Expression<Func<T, bool>> filter);

        Task<IEnumerable<Template>> GetAllAsync(string filter);
    }

    public class OdataManager : IOdataManager
    {
        private readonly HttpClient _httpClient;
        protected ODataClient _odataClient;

        public System.Uri BaseUrl => _httpClient.BaseAddress;

        public string BaseUrlOdata => BaseUrl + "odata/";

        public OdataManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Template>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Template>> GetAllAsync<T>(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Template>> GetAllAsync(string filter)
        {
            throw new NotImplementedException();
        }

        public ODataClient oDataClient
        {
            get
            {
                _odataClient = new ODataClient(new ODataClientSettings(BaseUrlOdata)
                {
                    IgnoreResourceNotFoundException = true,
                    OnTrace = (x, y) => Console.WriteLine(string.Format(x, y)),
                });

                return _odataClient;
            }
        }

    }
}
