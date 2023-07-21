using System;
using System.Collections.Generic;

public class ProgressiveEraCounterPicker : AgeCounterPicker
{
    public new static AgeCounterPicker GetInstance()
    {
        if (instance == null)
        {
            instance = new ProgressiveEraCounterPicker();
        }

        return instance;
    }

    public ProgressiveEraCounterPicker()
    {
        DefaultCounterPick = new Dictionary<string, int>
        {
            {"tank", 3},
            {"rogue", 5}
        };

        CounterPicks = new Dictionary<string, Dictionary<string, int>>
        {
        };
        UserAvailableUnitTypes = new Dictionary<string, string>()
        {
            {"Tank", "tank"},
            {"Artillery", "rf_cannon"},
            {"Sniper", "sniper"},
            {"Soldier", "conscript"},
            {"Armored Car", "armored_car"},
            {"Champion", "ProgressiveEra_champion"},
            {"Rogue", "rogue"}
        };
        EnemyUnitTypes = new Dictionary<string, string>()
        {
            {"Tank", "tank"},
            {"Artillery", "rf_cannon"},
            {"Sniper", "sniper"},
            {"Soldier", "conscript"},
            {"Armored Car", "armored_car"}
        };
    }
}