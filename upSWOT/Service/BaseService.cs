using Newtonsoft.Json;
using System.Text.RegularExpressions;
using upSWOT.Models;

namespace upSWOT.Service
{
    public abstract class BaseService
    {
        private HttpClient Client { get; }
        static readonly Regex PagePattern = new Regex("page=(?<pagenr>[0-9]+)");

        protected BaseService( string baseAddress)
        {

            Client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        protected async Task<T> Get<T>(string path)
        {
            var response = await Client.GetAsync(path);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) : default(T);
        }

        protected async Task<IEnumerable<T>> GetPages<T>(string url)
        {
            var result = new List<T>();
            var nextPage = -1;

            do
            {
                var dto = await Get<PageDto<T>>(nextPage == -1 ? url : $"{url}{(url.Contains("?") ? "&" : "?")}page={nextPage}");
                if(dto == null)
                {
                    return null;
                } 
                result.AddRange(dto.Results);

                nextPage = GetNextPageNumber(dto.Info.Next);
            }
            while (nextPage != -1);

            return result;
        }
        

        public int GetNextPageNumber(string url)
        {
            if (string.IsNullOrEmpty(url))
                return -1;

            var result = PagePattern.Match(url).Groups["pagenr"].Value;

            if (string.IsNullOrEmpty(result))
                return -1;

            return Convert.ToInt32(result);
        }
    }
}
