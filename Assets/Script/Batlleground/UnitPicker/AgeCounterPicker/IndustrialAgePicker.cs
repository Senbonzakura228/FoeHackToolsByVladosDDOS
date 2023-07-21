using System.Collections.Generic;

public class IndustrialAgePicker : AgeCounterPicker
{
    public new static AgeCounterPicker GetInstance()
    {
        if (instance == null)
        {
            instance = new IndustrialAgePicker();
        }

        return instance;
    }

    public IndustrialAgePicker()
    {
        DefaultCounterPick = new Dictionary<string, int>
        {
            {"howitzer", 3},
            {"rogue", 5}
        };

        CounterPicks = new Dictionary<string, Dictionary<string, int>>
        {
        };
        UserAvailableUnitTypes = new Dictionary<string, string>()
        {
            {"Howitzer", "howitzer"},
            {"Breech loader", "breech_loader"},
            {"Lancer", "lancer"},
            {"Rifleman", "rifleman"},
            {"Jaeger Car", "jaeger"},
            {"Rogue", "rogue"}
        };
        EnemyUnitTypes = new Dictionary<string, string>()
        {
            {"Howitzer", "howitzer"},
            {"Breech loader", "breech_loader"},
            {"Lancer", "lancer"},
            {"Rifleman", "rifleman"},
            {"Jaeger", "jaeger"}
        };
    }
}