using System.Collections.Generic;

public class SpaceAgeMarsPicker : AgeCounterPicker
{
    public new static AgeCounterPicker GetInstance()
    {
        if (instance == null)
        {
            instance = new SpaceAgeMarsPicker();
        }

        return instance;
    }

    public SpaceAgeMarsPicker()
    {
        DefaultCounterPick = new Dictionary<string, int>
        {
            {"tesla_walker", 3},
            {"rogue", 5}
        };

        CounterPicks = new Dictionary<string, Dictionary<string, int>>
        {
        };
        UserAvailableUnitTypes = new Dictionary<string, string>()
        {
            {"Tesla", "tesla_walker"},
            {"Sentinel", "sentinel"},
            {"Sniperbot", "sniperbot"},
            {"Space marine", "space_marine"},
            {"Steel warden", "steel_warden"},
            {"Rogue", "rogue"}
        };
        EnemyUnitTypes = new Dictionary<string, string>()
        {
            {"Tesla", "tesla_walker"},
            {"Sentinel", "sentinel"},
            {"Sniperbot", "sniperbot"},
            {"Space marine", "space_marine"},
            {"Steel warden", "steel_warden"}
        };
    }
}