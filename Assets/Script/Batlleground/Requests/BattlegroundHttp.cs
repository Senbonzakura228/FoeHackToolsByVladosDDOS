using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class BattlegroundHttp
{
    public static T GetEntityByRequestMethod<T>(string entity, string requestMethod)
    {
        var desyBG =
            JsonConvert.DeserializeObject<dynamic>(entity);
        T result = default;
        foreach (var dd in desyBG)
        {
            if (dd is JObject)
            {
                foreach (var d in dd as JObject)
                {
                    if (d.Value != null && d.Key == "requestMethod")
                    {
                        if ((dd as JObject).TryGetValue("requestMethod", out JToken requestMethodToken))
                        {
                            string method = (string) requestMethodToken;
                            if (method == requestMethod)
                            {
                                result = JsonConvert.DeserializeObject<T>(dd.ToString());
                                return result;
                            }
                        }
                    }
                }
            }
        }

        return result;
    }

    public static async Task<string> GetBattleGround(AccountParams accountParams, RequestService requestService)
    {
        var payload = new Payload(new PayloadParams[]
        {
            new PayloadParams()
            {
                ClassName = "ServerRequest",
                RequestId = requestService.currentRequestId,
                RequestClass = "GuildBattlegroundService",
                RequestData = JArray.Parse(@"[]"),
                RequestMethod = "getBattleground"
            }
        });
        var result = await requestService.SendRequest(accountParams, payload);
        return result;
    }

    public static async Task<string> GetArmyPreview(AccountParams accountParams, RequestService requestService,
        int provinceId)
    {
        string rqData = "[{\"__class__\":\"BattlegroundBattleType\"," +
                        "\"attackerPlayerId\":0,\"defenderPlayerId\":0,\"type\":\"battleground\"" +
                        ",\"currentWaveId\":0,\"totalWaves\":0,\"provinceId\":" + provinceId + ",\"battlesWon\":0}]";
        var payload = new Payload(new PayloadParams[]
        {
            new PayloadParams()
            {
                ClassName = "ServerRequest",
                RequestId = requestService.currentRequestId,
                RequestClass = "BattlefieldService",
                RequestData =
                    JArray.Parse(rqData),
                RequestMethod = "getArmyPreview"
            }
        });
        var result = await requestService.SendRequest(accountParams, payload);
        return result;
    }

    public static async Task<string> GetArmyInfo(AccountParams accountParams, RequestService requestService)
    {
        var payload = new Payload(new PayloadParams[]
        {
            new PayloadParams()
            {
                ClassName = "ServerRequest",
                RequestId = requestService.currentRequestId,
                RequestClass = "ArmyUnitManagementService",
                RequestData = JArray.Parse(@"[{""__class__"":""ArmyContext"",""battleType"":""battleground""}]"),
                RequestMethod = "getArmyInfo"
            }
        });
        var result = await requestService.SendRequest(accountParams, payload);
        return result;
    }

    public static async Task<string> UpdateArmyPool(AccountParams accountParams, RequestService requestService,
        UserArmyPoolRequestBody userArmyPoolRequestBody)
    {
        var requestBody = JsonConvert.SerializeObject(userArmyPoolRequestBody.requestData);
        var payload = new Payload(new PayloadParams[]
        {
            new PayloadParams()
            {
                ClassName = "ServerRequest",
                RequestId = requestService.currentRequestId,
                RequestClass = "ArmyUnitManagementService",
                RequestData = JArray.Parse(requestBody),
                RequestMethod = "updatePools"
            }
        });
        var result = await requestService.SendRequest(accountParams, payload);
        return result;
    }

    public static async Task<string> StartBattle(AccountParams accountParams, RequestService requestService,
        int provinceId)
    {
        string rqData = "[{\"__class__\":\"BattlegroundBattleType\"," +
                        "\"attackerPlayerId\":0,\"defenderPlayerId\":0,\"type\":\"battleground\"" +
                        ",\"currentWaveId\":0,\"totalWaves\":0,\"provinceId\":" + provinceId +
                        ",\"battlesWon\":0}, true]";
        var payload = new Payload(new PayloadParams[]
        {
            new PayloadParams()
            {
                ClassName = "ServerRequest",
                RequestId = requestService.currentRequestId,
                RequestClass = "BattlefieldService",
                RequestData =
                    JArray.Parse(rqData),
                RequestMethod = "startByBattleType"
            }
        });
        var result = await requestService.SendRequest(accountParams, payload);
        return result;
    }

    public static async Task<string> StartSecondWaveBattle(AccountParams accountParams, RequestService requestService,
        int provinceId, int battlesWon)
    {
        string rqData = "[{\"__class__\":\"BattlegroundBattleType\"," +
                        "\"attackerPlayerId\":0,\"defenderPlayerId\":0,\"type\":\"battleground\"" +
                        ",\"currentWaveId\":0,\"totalWaves\":0,\"provinceId\":" + provinceId + ",\"battlesWon\":" +
                        battlesWon + "}, true]";
        var payload = new Payload(new PayloadParams[]
        {
            new PayloadParams()
            {
                ClassName = "ServerRequest",
                RequestId = requestService.currentRequestId,
                RequestClass = "BattlefieldService",
                RequestData =
                    JArray.Parse(rqData),
                RequestMethod = "startByBattleType"
            }
        });
        var result = await requestService.SendRequest(accountParams, payload);
        return result;
    }
}