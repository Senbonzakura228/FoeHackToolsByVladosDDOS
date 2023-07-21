using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class UnitPicker
{
    public async Task<UserArmyPoolRequestBody> PrepareEligibleUnits(EnemyArmyPreviewResponce enemyArmy)
    {
        var unitPool = MainDataStorage.GetUserAgeCounterPicker().GetCounterPickByUnitName(GetCommonUnitName(enemyArmy));

        return await PrepareArmyPoolRequestBody(unitPool);
    }

    private string GetCommonUnitName(EnemyArmyPreviewResponce enemyArmy)
    {
        var units = GetAllUnits(enemyArmy);
        var favoriteUnit = ("", 1);

        foreach (var currentUnit in units)
        {
            var coincidences = 0;
            foreach (var unit in units)
            {
                if (currentUnit == unit)
                {
                    coincidences += 1;
                }
            }

            if (coincidences > favoriteUnit.Item2)
            {
                favoriteUnit = (currentUnit, coincidences);
            }
        }

        return favoriteUnit.Item1;
    }

    private List<string> GetAllUnits(EnemyArmyPreviewResponce enemyArmy)
    {
        var units = new List<string>();

        foreach (var enemyArmyWave in enemyArmy.responseData)
        {
            foreach (var enemyUnit in enemyArmyWave.units)
            {
                units.Add(enemyUnit.unitTypeId);
            }
        }

        return units;
    }

    public async Task<UserArmyResponce> GetUserArmyResponce()
    {
        var userArmyResponce =
            await BattlegroundHttp.GetArmyInfo(MainDataStorage.AccountParams, MainDataStorage.RequestService);
        var userArmyToPrepare = JsonConvert.DeserializeObject(userArmyResponce).ToString();
        var userArmy = BattlegroundHttp.GetEntityByRequestMethod<UserArmyResponce>(userArmyToPrepare, "getArmyInfo");
        return userArmy;
    }

    private async Task<UserArmyPoolRequestBody> PrepareArmyPoolRequestBody(Dictionary<string, int> units)
    {
        var userArmy = await GetUserArmyResponce();

        var attacking = new List<int>();
        var defending = new List<int>();
        var arenaDefending = new List<int>();

        foreach (var keyValuePair in units)
        {
            foreach (var responseDataUnit in userArmy.responseData.units)
            {
                if (checkCompatibilityToAttacking(responseDataUnit, attacking.Count, keyValuePair.Key))
                {
                    for (int i = 0; i < keyValuePair.Value; i++)
                    {
                        attacking.Add(responseDataUnit.unitIds[i]);
                    }

                    break;
                }
            }
        }


        foreach (var responseDataUnit in userArmy.responseData.units)
        {
            if (checkCompatibilityToDefending(responseDataUnit, defending.Count))
            {
                defending.Add(responseDataUnit.unitId);
                if (defending.Count >= 8)
                {
                    break;
                }
            }
        }


        foreach (var responseDataUnit in userArmy.responseData.units)
        {
            if (checkCompatibilityToarenaDefending(responseDataUnit, arenaDefending.Count))
            {
                arenaDefending.Add(responseDataUnit.unitId);
                if (arenaDefending.Count >= 8)
                {
                    break;
                }
            }
        }

        var requestBody = new UserArmyPoolRequestBody()
        {
            __class__ = "ServerRequest",
            requestClass = "ArmyUnitManagementService",
            requestMethod = "updatePools",
            requestId = MainDataStorage.RequestService.currentRequestId,
            requestData = new dynamic
                []
                {
                    new List<object>
                    {
                        new UserArmyPool
                        {
                            __class__ = "ArmyPool",
                            units = attacking,
                            type = "attacking",
                        },
                        new UserArmyPool
                        {
                            __class__ = "ArmyPool",
                            units = defending,
                            type = "defending",
                        },
                        new UserArmyPool
                        {
                            __class__ = "ArmyPool",
                            units = arenaDefending,
                            type = "arena_defending",
                        }
                    },
                    new ArmyContext
                    {
                        __class__ = "ArmyContext",
                        battleType = "battleground",
                    }
                }
        };

        return requestBody;
    }

    private bool checkCompatibilityToAttacking(UserUnit unit, int currentUnitsCount, string unitType)
    {
        if (currentUnitsCount >= 8)
        {
            return false;
        }

        return unit.unitTypeId == unitType && unit.currentHitpoints == 10 && unit.count > 10;
    }

    private bool checkCompatibilityToDefending(UserUnit unit, int currentUnitsCount)
    {
        if (currentUnitsCount >= 8)
        {
            return false;
        }


        return unit.is_defending == true || false;
    }

    private bool checkCompatibilityToarenaDefending(UserUnit unit, int currentUnitsCount)
    {
        if (currentUnitsCount >= 8)
        {
            return false;
        }

        return unit.isArenaDefending == true || false;
    }
}