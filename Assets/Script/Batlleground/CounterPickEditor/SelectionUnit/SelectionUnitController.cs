using System;
using UnityEngine;

public class SelectionUnitController : MonoBehaviour
{
    [SerializeField] private SelectionUnitCarouselItem carouselItem;
    [SerializeField] private UnitIconStorage iconStorage;
    private int armyUnitSlotId;
    [SerializeField] private RectTransform itemSpawnPoint;

    public void Initialise(int id)
    {
        armyUnitSlotId = id;
        var currentAge = MainDataStorage.GetUserAgeCounterPicker();
        foreach (var unitType in currentAge.UserAvailableUnitTypes)
        {
            Sprite unitIcon = Sprite.Create(Texture2D.redTexture, new Rect(0.0f, 0.0f, 0, 0),
                new Vector2(0.5f, 0.5f), 100.0f);
            foreach (var icon in iconStorage.UnitIcons)
            {
                if (icon.unitName != unitType.Value) continue;
                unitIcon = icon.icon;
                break;
            }

            var item = Instantiate(carouselItem, itemSpawnPoint);
            item.SetUnit(unitType.Value, unitIcon);
        }
    }

    public void SelectUnit(string unitType)
    {
        var userUnitsEditor = gameObject.GetComponentInParent<UserUnitEditorController>();
        userUnitsEditor.SetUnit(armyUnitSlotId, unitType);
        Destroy(gameObject);
    }
}