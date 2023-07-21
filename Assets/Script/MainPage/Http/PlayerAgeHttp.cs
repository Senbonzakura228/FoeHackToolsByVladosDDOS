using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class PlayerAgeHttp
{
    public static async Task<string> GetPlayerAge(AccountParams accountParams, RequestService requestService)
    {
        var payload = new Payload(new PayloadParams[]
        {
            new PayloadParams()
            {
                ClassName = "ServerRequest",
                RequestId = requestService.currentRequestId,
                RequestClass = "StartupService",
                RequestData = JArray.Parse(@"[]"),
                RequestMethod = "getData"
            }
        });
        var result = await requestService.SendRequest(accountParams, payload);
        return result;
    }
}