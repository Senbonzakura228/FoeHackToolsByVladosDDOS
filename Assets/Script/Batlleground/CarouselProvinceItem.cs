using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarouselProvinceItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private StartBattleButton _startBattleButton;
    private Province province;

    private void Start()
    {
        _startBattleButton = GameObject.Find("StartBattlegroundButton").GetComponent<StartBattleButton>();
    }

    public void SetNameById(int id, string mapName)
    {
        _textMeshPro.text = mapName == "waterfall_archipelago" ? ProvinceNamesStorage.waterfall_archipelago[id] : ProvinceNamesStorage.volcano_archipelago[id];

        if (_textMeshPro.text == "skip")
        {
            Destroy(gameObject);
        }
    }

    public void SetProvince(Province province)
    {
        this.province = province;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_startBattleButton.disabled)
        {
            _startBattleButton.Initialise(province.id ?? 0, _textMeshPro.text);
        }
        else
        {
            _startBattleButton.Disable();
        }
    }
}