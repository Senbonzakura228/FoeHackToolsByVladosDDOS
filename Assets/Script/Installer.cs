using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Installer : MonoBehaviour
{
    [SerializeField] private RequestIdController _requestIdController;

    private void Awake()
    {
        // Application.runInBackground = true;
        Screen.fullScreen = false;
        if (MainDataStorage.RequestService is null)
        {
            MainDataStorage.RequestService = new RequestService();
            _requestIdController.ChangeCurrentRequestId();
        }

        if (MainDataStorage.AccountParams is null)
        {
            MainDataStorage.AccountParams = new AccountParams()
            {
                UserKey = "AZGtNZ80Yei54OmMC4E74Wgt",
                Domain = "ru4.forgeofempires.com",
                region = "ru4",
                origin = "https://ru4.forgeofempires.com",
                instanceId = "4vebgm4y5f",
                metricsUvId = "ee20e3f5-ca4b-4f8a-8c23-5b997a51f5ae",
                sid = "lIYYbI2mR3XDzyQcWztJ-0urvMCLAQHTl2NPT9-K",
                cid = "1626356887",
                ig_conv_last_site = "https://ru4.forgeofempires.com/game/index",
                _uetsid = "f137e2e0d39c11eda707fd5ad69ba9ff",
                _uetvid = "f137e2e0d39c11eda707fd5ad69ba9ff"
            };
        }
    }


}