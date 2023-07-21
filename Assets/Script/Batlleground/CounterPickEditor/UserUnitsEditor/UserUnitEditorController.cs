using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserUnitEditorController : MonoBehaviour
{
    [SerializeField] private UserUnitCarouselItem[] unitCarouselItems;
    [SerializeField] private UnitIconStorage iconStorage;
    [SerializeField] private SelectionUnitController unitSelectionDialog;
    private string counterPickenemyType;
    private string savePath = "/counterPick";
    private bool disabledForSave = true;

    public void Initialize(string enemyType)
    {
        counterPickenemyType = enemyType;
        savePath += enemyType;
        LoadCounterPick();
    }

    public void SetUnit(int armyUnitSlotId, string unitType)
    {
        foreach (var item in unitCarouselItems)
        {
            if (item.id != armyUnitSlotId) continue;
            Sprite unitIcon = Sprite.Create(Texture2D.redTexture, new Rect(0.0f, 0.0f, 0, 0),
                new Vector2(0.5f, 0.5f), 100.0f);
            foreach (var icon in iconStorage.UnitIcons)
            {
                if (icon.unitName != unitType) continue;
                unitIcon = icon.icon;
                break;
            }

            item.SetUnit(unitType, unitIcon);
            disabledForSave = false;
            break;
        }
    }

    public void OpenUnitSelectionDialog(int id)
    {
        Instantiate(unitSelectionDialog, transform).Initialise(id);
    }

    public void SaveCounterPick()
    {
        if (!disabledForSave)
        {
            var units = PrepareCounterPick();
            var dataSaveService = new JsonDataSaveService();
            dataSaveService.SaveData(savePath, units);
            MainDataStorage.GetUserAgeCounterPicker()
                .AddCounterPick(counterPickenemyType, new Dictionary<string, int>(units));
        }

        Destroy(gameObject);
    }

    public void LoadCounterPick()
    {
        if (!File.Exists(Application.persistentDataPath + savePath)) return;
        var dataSaveService = new JsonDataSaveService();
        var loadedUnits = dataSaveService.LoadData<Dictionary<string, int>>(savePath);
        int count = 0;
        foreach (var keyValuePair in loadedUnits)
        {
            for (int i = 0; i < keyValuePair.Value; i++)
            {
                count++;
                SetUnit(count, keyValuePair.Key);
            }
        }

        MainDataStorage.GetUserAgeCounterPicker()
            .AddCounterPick(counterPickenemyType, new Dictionary<string, int>(loadedUnits));
    }

    private Dictionary<string, int> PrepareCounterPick()
    {
        var result = new Dictionary<string, int>();
        var selectedUnits = new List<string>();
        foreach (var item in unitCarouselItems)
        {
            selectedUnits.Add(item.unitType);
        }

        foreach (var selectedUnit in selectedUnits)
        {
            var coincidences = 0;
            for (var i = 0; i < selectedUnits.Count; i++)
            {
                if (selectedUnit != selectedUnits[i]) continue;
                coincidences += 1;
            }

            if (result.ContainsKey(selectedUnit)) continue;
            Debug.Log(coincidences + " " + selectedUnit);
            result.Add(selectedUnit, coincidences);
        }

        return result;
    }
}