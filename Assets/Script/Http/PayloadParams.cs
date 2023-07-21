using Newtonsoft.Json.Linq;

public class PayloadParams
{
    public int RequestId;
    public string ClassName;
    public string RequestClass;
    public JArray RequestData;
    public string RequestMethod;
}