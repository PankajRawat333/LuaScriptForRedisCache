local result = {}
local ruletypes = {"FAULT_CODE","FUEL","MILESTONE","INCREMENTAL"}
local assetId = "VLA59542CAT"

for _,rule in ipairs(ruletypes) do
	local ruleType = rule..'_hash'
    local val = redis.call('HGET', ruleType, assetId)
    result[#result + 1] = val
end

return result