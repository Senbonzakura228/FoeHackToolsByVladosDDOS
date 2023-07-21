using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class AuthorizationController : MonoBehaviour
{
    [SerializeField] private OpenMainMenuButton openMainMenuButton;
    [SerializeField] private TMP_InputField sid;
    [SerializeField] private TMP_InputField UserKey;

    private void Start()
    {
        StartupInitialise();
    }

    private void StartupInitialise()
    {
        if (PlayerPrefs.HasKey("accountUserKey"))
        {
            UserKey.text = PlayerPrefs.GetString("accountUserKey");
            MainDataStorage.AccountParams.UserKey = this.UserKey.text;
        }

        if (PlayerPrefs.HasKey("accountSid"))
        {
            sid.text = PlayerPrefs.GetString("accountSid");
            MainDataStorage.AccountParams.sid = this.sid.text;
        }

        openMainMenuButton.CheckDisable();
    }


    private void OnChangeInputsValue()
    {
        if (sid.text == "" || UserKey.text == "") return;
        openMainMenuButton.CheckDisable();
        ;
    }


    public void OnSetSid()
    {
        MainDataStorage.AccountParams.sid = this.sid.text;
        PlayerPrefs.SetString("accountSid", this.sid.text);
        OnChangeInputsValue();
        PlayerPrefs.Save();
    }

    public void OnSetUserKey()
    {
        MainDataStorage.AccountParams.UserKey = this.UserKey.text;
        PlayerPrefs.SetString("accountUserKey", this.UserKey.text);
        PlayerPrefs.Save();
        OnChangeInputsValue();
    }

    public void OnValueChange()
    {
        if (sid.text == "" || UserKey.text == "")
        {
            openMainMenuButton.CheckDisable();
        }
    }
}