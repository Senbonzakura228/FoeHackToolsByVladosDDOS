using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterPickEditorController : MonoBehaviour
{
    [SerializeField] private UnitIconStorage iconStorage;
    [SerializeField] private EnemyUnitCarouselItem carouselItem;
    [SerializeField] private UserUnitEditorController userUnitEditorController;
    [SerializeField] private RectTransform dialogSpawnPoint;

    private void Awake()
    {
        Initialise();
    }

    private void Initialise()
    {
        var currentAge = MainDataStorage.GetUserAgeCounterPicker();
        foreach (var unitType in currentAge.EnemyUnitTypes)
        {
            Sprite unitIcon = Sprite.Create(Texture2D.redTexture, new Rect(0.0f, 0.0f, 0, 0),
                new Vector2(0.5f, 0.5f), 100.0f);
            foreach (var icon in iconStorage.UnitIcons)
            {
                if (icon.unitName != unitType.Value) continue;
                unitIcon = icon.icon;
                break;
            }

            var item = Instantiate(carouselItem, transform);
            item.SetUnit(unitType.Value, unitIcon);
        }
    }

    public void OpenUserUnitsEditorDialog(string unitType)
    {
        Instantiate(userUnitEditorController, dialogSpawnPoint).Initialize(unitType);
    }
}