using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyUnitCarouselItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    private string unitType;

    public void SetUnit(string unitType, Sprite sprite)
    {
        this.unitType = unitType;
        SetIcon(sprite);
    }

    private void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var counterPickEditorController = gameObject.GetComponentInParent<CounterPickEditorController>();
        counterPickEditorController.OpenUserUnitsEditorDialog(unitType);
    }
}