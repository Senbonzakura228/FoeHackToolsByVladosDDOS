using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RequestService
{
    private const string GameVersion = "1.261";
    private int _currentRequestId;

    public Action RequestIdChangeHandler;
    //  private readonly string _proxyUri = "http://:8000";

    public int currentRequestId
    {
        get => _currentRequestId;
        set
        {
            _currentRequestId = +value;
            RequestIdChangeHandler.Invoke();
        }
    }

    public void StartupRequestIdSet(int value)
    {
        _currentRequestId = value;
    }

    public async Task<string> SendRequest(AccountParams accountParams, Payload payload)
    {
        var tcs = new TaskCompletionSource<bool>();
        await RequestQueueService.AddRequestToQueue(tcs);
        var webRequest = (HttpWebRequest) WebRequest.Create(accountParams.RequestsAddress);

        var encryptor = new Encryptor(accountParams.UserKey);

        byte[] payloadBody = Encoding.ASCII.GetBytes(payload.ToString());

        //    Debug.Log(payload.ToString());

        webRequest.AutomaticDecompression =
            DecompressionMethods.GZip | DecompressionMethods.Deflate;
        webRequest.Method = "POST";
        webRequest.ContentType = "application/json";
        webRequest.KeepAlive = true;
        webRequest.Accept = "*/*";
        webRequest.Host = accountParams.Domain;
        webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

        webRequest.Headers["Client-Identification"] =
            $"version={GameVersion}; requiredVersion={GameVersion}; platform=bro; platformType=html5; platformVersion=web";
        webRequest.Headers["Origin"] = accountParams.origin;
        webRequest.Headers["Signature"] = (encryptor.getEncryptedData(payload));
        webRequest.UserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36";
        webRequest.Referer = $"https://{accountParams.region}.forgeofempires.com/game/index?";
        webRequest.Headers["Accept-Encoding"] = "gzip, deflate, br";
        webRequest.Headers["Accept-Language"] = "en-US,en;q=0.9";
        webRequest.Headers["Cookie"] =
            // $"instanceId={accountParams.instanceId};" +
            $" metricsUvId={accountParams.metricsUvId};" +
            $" sid={accountParams.sid};"
            //  + $" cid={accountParams.cid};"
            //  + $" ig_conv_last_site={accountParams.ig_conv_last_site};" +
            // $" _uetsid={accountParams._uetsid};" +
            // $" _uetvid={accountParams._uetvid}"
            ;
        webRequest.Headers["sec-ch-ua"] =
            "\"Google Chrome\";v=\"114\", \"Not(A:Brand\";v=\"8\", \"Chromium\";v=\"114\"";
        webRequest.Headers["sec-ch-ua-mobile"] = "?0";
        webRequest.Headers["sec-ch-ua-platform"] = "\"Windows\"";
        webRequest.Headers["sec-fetch-dest"] = "empty";
        webRequest.Headers["sec-fetch-mode"] = "cors";
        webRequest.Headers["sec-fetch-site"] = "same-origin";

        Task<string> Response;
        using (Stream requestStream = await webRequest.GetRequestStreamAsync())
        {
            await requestStream.WriteAsync(payloadBody, 0, payloadBody.Length);
            await requestStream.FlushAsync();
        }

        using (var response = (HttpWebResponse) await webRequest.GetResponseAsync())
        using (Stream streamResponse = response.GetResponseStream())
        using (StreamReader streamReader = new StreamReader(streamResponse))
        {
            Response = streamReader.ReadToEndAsync();
            Debug.Log(Response.Result);
        }

        currentRequestId += 1;
        if (!tcs.Task.IsCompleted)
        {
            tcs.SetResult(true);
        }

        return await Response;
    }
}