using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureRedisCacheTest
{
    public interface ICacheService
    {
        Task<string> GetAsync(string hashKey, string ruleKey);

        Task<bool> HashExistsAsync(string hashKey, string fieldKey);

        Task<bool> HashDeleteAsync(string hashKey, string fieldKey);

        Task HashSetAsync(string hashKey, string fieldKey, string value);

        IEnumerable<string> GetAllFieldKeys(string hashKey);
    }
}