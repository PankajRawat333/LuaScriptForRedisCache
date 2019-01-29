local result = {}
local ruletypes = @RuleTypes
local assetId = @AssetId

for rule in ruletypes:gmatch("([^,]+)") do
	local ruleType = rule..'_hash'
    local val = redis.call('HGET', ruleType, assetId)
    result[#result + 1] = val
end

return result