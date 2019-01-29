using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureRedisCacheTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CacheService cacheService = new CacheService();
            var parameters = new { AssetId = "DIST0012CAT", RuleTypes = "FAULT_CODE,FUEL,MILESTONE,INCREMENTAL" };
            cacheService.PrepareLuaScript(parameters);
            //var summary = BenchmarkRunner.Run<AzureRedisCacheBenchmarking>();

            //AzureRedisCacheBenchmarking azureRedisCacheBenchmarking = new AzureRedisCacheBenchmarking();
            //azureRedisCacheBenchmarking.GetRedisValuesByLoopManualBenchmark();
            //azureRedisCacheBenchmarking.GetRedisValuesByLuaManualBenchmark();
            //azureRedisCacheBenchmarking.GetRedisValuesByLoopManualBenchmark();
            //azureRedisCacheBenchmarking.GetRedisValuesByLuaManualBenchmark();
            Console.Read();
        }
    }
}
