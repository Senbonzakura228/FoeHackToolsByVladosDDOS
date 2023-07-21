using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartBattleButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite inactiveImage;
    [SerializeField] private Sprite activeImage;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI text;
    private int provinceId;

    [SerializeField] private BattlegroundController _battlegroundController;
    public bool disabled = true;

    private void Start()
    {
        Disable();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeBattleOnStatus();
    }

    private void ChangeBattleOnStatus()
    {
        if (disabled) return;
        Debug.Log(123);
        if (!_battlegroundController.BattleIsOn)
        {
            _battlegroundController.StartBattle(provinceId);
            _image.sprite = activeImage;
        }
        else
        {
            _battlegroundController.EndBattle();
            _image.sprite = inactiveImage;
        }
    }

    public void Initialise(int provinceId, string text)
    {
        this.text.text = text;
        this.disabled = false;
        this.provinceId = provinceId;
        _image.sprite = inactiveImage;
        _image.color = normalColor;
    }

    public void Disable()
    {
        this.disabled = true;
        text.text = "";
        _image.sprite = inactiveImage;
        _image.color = inactiveColor;
    }
}