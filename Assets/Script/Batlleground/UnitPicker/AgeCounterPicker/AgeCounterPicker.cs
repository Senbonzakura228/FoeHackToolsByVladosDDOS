using System.Collections.Generic;

public class AgeCounterPicker
{
    protected static AgeCounterPicker instance;
    public Dictionary<string, string> UserAvailableUnitTypes;

    public Dictionary<string, string> EnemyUnitTypes;
    
    protected static Dictionary<string, Dictionary<string, int>> CounterPicks;
    protected Dictionary<string, int> DefaultCounterPick;

    public Dictionary<string, int> GetCounterPickByUnitName(string  name)
    {
        return CounterPicks.ContainsKey(name) ? CounterPicks[name] : DefaultCounterPick;
    }

    public static AgeCounterPicker GetInstance()
    {
        if (instance == null)
        {
            instance = new AgeCounterPicker();
        }
        return instance;
    }
    
    public void AddCounterPick(string name, Dictionary<string, int> pick)
    {
        RemoveCounterPick(name);
        CounterPicks.Add(
            name, pick
        );
    }

    public void RemoveCounterPick(string name)
    {
        if (!CounterPicks.ContainsKey(name))
            return;
        CounterPicks.Remove(name);
    }
}