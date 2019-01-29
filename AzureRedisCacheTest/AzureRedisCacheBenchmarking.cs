using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace AzureRedisCacheTest
{
    [HtmlExporter]
    public class AzureRedisCacheBenchmarking
    {
        private CacheService cacheService;

        public enum TSFRuleType
        {
            FUEL, //FUEL HI Low
            FAULT_CODE, //Fault Code
            MILESTONE, // Milestone type of rule
            INCREMENTAL // Incremental type of rule
        }

        public AzureRedisCacheBenchmarking()
        {
            this.cacheService = new CacheService();
        }

        [Benchmark]
        public void GetRedisValuesByLoop()
        {
            foreach (string ruleType in Enum.GetNames(typeof(TSFRuleType)).ToList())
            {
                string hashKey = ruleType + "_hash";
                var cacheValue = cacheService.GetAsync(hashKey, "Plant001").GetAwaiter().GetResult();
            }
        }

        [Benchmark]
        public void GetRedisValuesByLua()
        {
            var parameters = new { AssetId = "DIST0012CAT", RuleTypes = "FAULT_CODE,FUEL,MILESTONE,INCREMENTAL" };
            this.cacheService.PrepareLuaScript(parameters);
        }

        public void GetRedisValuesByLoopManualBenchmark()
        {
            int errorCount = 0;
            int successCount = 0;
            for (int i = 0; i < 5; i++)
            {
                foreach (string ruleType in Enum.GetNames(typeof(TSFRuleType)).ToList())
                {
                    string hashKey = ruleType + "_hash";
                    try
                    {
                        var cacheValue = cacheService.GetAsync(hashKey, "Plant001").GetAwaiter().GetResult();
                        successCount++;
                    }
                    catch
                    {
                        errorCount++;
                    }
                }
            }
            Console.WriteLine("GetHash Result for 50 request");
            Console.WriteLine($"SuccessCount {successCount}");
            Console.WriteLine($"ErrorCount {errorCount}");
        }

        public void GetRedisValuesByLuaManualBenchmark()
        {
            int errorCount = 0;
            int successCount = 0;
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    var parameters = new { AssetId = "DIST0012CAT", RuleTypes = "FAULT_CODE,FUEL,MILESTONE,INCREMENTAL" };
                    this.cacheService.PrepareLuaScript(parameters);
                    successCount++;
                }
                catch
                {
                    errorCount++;
                }
            }
            Console.WriteLine("Lua Result for 50 request");
            Console.WriteLine($"SuccessCount {successCount}");
            Console.WriteLine($"ErrorCount {errorCount}");
        }
    }
}