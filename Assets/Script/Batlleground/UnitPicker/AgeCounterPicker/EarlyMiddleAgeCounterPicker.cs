using System.Collections.Generic;

public class EarlyMiddleAgeCounterPicker : AgeCounterPicker
{
    public EarlyMiddleAgeCounterPicker()
    {
        DefaultCounterPick = new Dictionary<string, int>
        {
            {"howitzer", 3},
            {"rogue", 5}
        };

        CounterPicks = new Dictionary<string, Dictionary<string, int>>
        {
            {
                "mounted_bowman", new Dictionary<string, int>
                {
                    {"howitzer", 3},
                    {"rogue", 5}
                }
            },
            {
                "spearman", new Dictionary<string, int>
                {
                    {"jaeger", 3},
                    {"rogue", 5}
                }
            },
            {
                "catapult", new Dictionary<string, int>
                {
                    {"jaeger", 3},
                    {"rogue", 5}
                }
            },
            {
                "armoredswordsman", new Dictionary<string, int>
                {
                    {"jaeger", 3},
                    {"rogue", 5}
                }
            },
            {
                "cataphract", new Dictionary<string, int>
                {
                    {"howitzer", 3},
                    {"rogue", 5}
                }
            },
        };
    }
}