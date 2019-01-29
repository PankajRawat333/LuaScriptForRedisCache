local r = {}
return redis.call('HGET','FTOD_hash','Plant101')