using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class Payload
{
    private PayloadParams[] _params;

    public Payload(PayloadParams[] payloadParams)
    {
        _params = payloadParams;
    }

    public JArray ToJsonObject()
    {
        var payloadParams = new JArray();
            
        foreach (var param in _params)
        {
            var j = new JObject
            {
                ["__class__"] = param.ClassName,
                ["requestData"] = new JArray(param.RequestData),
                ["requestClass"] = param.RequestClass,
                ["requestMethod"] = param.RequestMethod,
                ["requestId"] = param.RequestId
            };

            payloadParams.Add(j);
        }
        return payloadParams;
    }
        
    public override string ToString()
    {
        return ToJsonObject().ToString(Formatting.None);
    }
}