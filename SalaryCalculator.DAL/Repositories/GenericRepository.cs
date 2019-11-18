using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SalaryCalculator.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly HttpClient _client = new HttpClient();
        public async Task<IQueryable<T>> Query(string path)
        {
            var result = await _client.GetStringAsync(path);
            if (result == null) return null;
            var data = JsonConvert.DeserializeObject<ICollection<T>>(result);
            return data.AsQueryable();

        }


    }
}