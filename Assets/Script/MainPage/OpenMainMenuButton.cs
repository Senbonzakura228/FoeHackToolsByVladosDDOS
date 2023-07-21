using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenMainMenuButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform redirectedPage;
    [SerializeField] private Image _image;
    [SerializeField] private Color disabledColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color disabledTextColor;
    private PageController pageController;
    private bool isDisabled;

    private void Start()
    {
        pageController = gameObject.GetComponentInParent<PageController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RedirectClick(redirectedPage);
    }

    private async void RedirectClick(RectTransform page)
    {
        await InitializePlayerAge();
        if (isDisabled) return;
        pageController.ChangePage(page);
    }

    public void CheckDisable()
    {
        if (MainDataStorage.AccountParams.sid != "" && MainDataStorage.AccountParams.UserKey != "")
        {
            isDisabled = false;
            _image.color = activeColor;
        }
        else
        {
            isDisabled = true;
            _image.color = disabledColor;
        }
    }
    
    public async Task InitializePlayerAge()
    {
        var startupResponse =
            await PlayerAgeHttp.GetPlayerAge(MainDataStorage.AccountParams, MainDataStorage.RequestService);
        startupResponse = JsonConvert.DeserializeObject(startupResponse).ToString();
        var startupPlayerData = BattlegroundHttp.GetEntityByRequestMethod<StartupResponse>(startupResponse, "getData");

        Debug.Log(startupPlayerData.responseData.user_data.era);

        switch (startupPlayerData.responseData.user_data.era)
        {
            case "IndustrialAge":
                MainDataStorage.UserAge = Ages.IndustrialAge;
                break;
            case "ProgressiveEra":
                MainDataStorage.UserAge = Ages.ProgressiveEra;
                break;
            case "SpaceAgeMars":
                MainDataStorage.UserAge = Ages.SpaceAgeMars;
                break;
        }
    }
    
}