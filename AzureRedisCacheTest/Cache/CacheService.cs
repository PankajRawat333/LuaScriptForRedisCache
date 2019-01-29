using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AzureRedisCacheTest
{
    public class CacheService
    {
        private readonly IDatabase cache;
        private static readonly string connectionString = ConfigurationManager.AppSettings["RedisConnection"];
        private static LoadedLuaScript ruleLuaScript;
        
        public CacheService()
        {
            this.cache = Connection.GetDatabase();
            ruleLuaScript = LoadLuaScriptForGetRule(@"Lua/getMultiHashWithParameter.lua");
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(connectionString);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }        

        private LoadedLuaScript LoadLuaScriptForGetRule(string scriptUrl)
        {
            string script = File.ReadAllText(scriptUrl);
            LuaScript luaScript = LuaScript.Prepare(script);
            var endpoints = Connection.GetEndPoints();
            var endpoint = endpoints[0];
            IServer server = Connection.GetServer(endpoint);
            return luaScript.Load(server);
        }
        public void PrepareLuaScript(dynamic parameters)
        {
            RedisResult result = this.cache.ScriptEvaluate(ruleLuaScript, parameters);
            if (!result.IsNull)
            {
                var type = result.Type;
            }
            var test = result.IsNull;
        }
    }
}