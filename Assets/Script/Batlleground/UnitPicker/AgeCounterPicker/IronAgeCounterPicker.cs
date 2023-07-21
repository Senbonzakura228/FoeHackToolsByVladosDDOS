using System.Collections.Generic;

public class IronAgeCounterPicker : AgeCounterPicker
{
    public IronAgeCounterPicker()
    {
        DefaultCounterPick = new Dictionary<string, int>
        {
            {"balista", 8}
        };

        CounterPicks = new Dictionary<string, Dictionary<string, int>>
        {
            {
                "balista", new Dictionary<string, int>
                {
                    {"balista", 8}
                }
            },
            {
                "mounted_legionnaire", new Dictionary<string, int>
                {
                    {"militiaman", 3},
                    {"rogue", 5}
                }
            },
            {
                "archer", new Dictionary<string, int>
                {
                    {"balista", 8}
                }
            },
            {
                "legionnaire", new Dictionary<string, int>
                {
                    {"balista", 8}
                }
            },
            {
                "militiaman", new Dictionary<string, int>
                {
                    {"legionnaire", 3},
                    {"rogue", 5}
                }
            },
        };
    }
}