using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class BattlegroundController : MonoBehaviour
{
    public Battleground Battleground;
    public Carouselnitialiser Carouselnitialiser;
    public bool BattleIsOn = false;
    [SerializeField] private AttritionController _attritionController;

    void Start()
    {
        initBattleground();
    }

    private async Task initBattleground()
    {
        var battleground =
            await BattlegroundHttp.GetBattleGround(MainDataStorage.AccountParams, MainDataStorage.RequestService);
        var BG = JsonConvert.DeserializeObject(battleground).ToString();
        Battleground = BattlegroundHttp.GetEntityByRequestMethod<BattlegroundResponse>(BG, "getBattleground")
            .responseData;
        _attritionController.SetValue(Battleground.currentPlayerParticipant.attrition.level);
        Carouselnitialiser.Initialise(Battleground);
    }

    public void StartBattle(int provinceId)
    {
        BattleIsOn = true;
        DoBattle(provinceId);
    }

    public void EndBattle()
    {
        BattleIsOn = false;
    }

    public async Task DoBattle(int provinceId)
    {
        var enemyArmy = await GetEnemyArmy(provinceId);
        await UpdateArmyPool(enemyArmy);
        bool isTwoWave = enemyArmy.responseData.Length > 1;

        await DoBattleRequest(provinceId, isTwoWave);
        if (BattleIsOn)
        {
            await DoBattle(provinceId);
        }
    }

    private async Task<EnemyArmyPreviewResponce> GetEnemyArmy(int provinceId)
    {
        var army = await BattlegroundHttp
            .GetArmyPreview(MainDataStorage.AccountParams, MainDataStorage.RequestService, provinceId);
        var enemyArmyResponse = JsonConvert.DeserializeObject(army).ToString();
        return BattlegroundHttp.GetEntityByRequestMethod<EnemyArmyPreviewResponce>(enemyArmyResponse, "getArmyPreview");
    }

    private async Task UpdateArmyPool(EnemyArmyPreviewResponce enemyArmy)
    {
        var armyPoolRequestBody = await new UnitPicker().PrepareEligibleUnits(enemyArmy);
        await BattlegroundHttp.UpdateArmyPool(MainDataStorage.AccountParams, MainDataStorage.RequestService,
            armyPoolRequestBody);
    }

    public async Task DoBattleRequest(int provinceID, bool isTwoWave)
    {
        var battleResult = await BattlegroundHttp.StartBattle(MainDataStorage.AccountParams,
            MainDataStorage.RequestService, provinceID);
        StartByBattleTypeResponce startByBattleTypeResponse = BattlegroundHttp
            .GetEntityByRequestMethod<StartByBattleTypeResponce>(battleResult, "startByBattleType");
        if (!isTwoWave)
        {
            _attritionController.SetValue(BattlegroundHttp
                .GetEntityByRequestMethod<PlayerParticipantResponse>(battleResult, "getPlayerParticipant")
                .responseData.attrition.level);
        }
        
        if (isTwoWave)
        {
            var battlesWon = BattlegroundHttp
                .GetEntityByRequestMethod<StartByBattleTypeResponce>(battleResult, "startByBattleType").responseData
                .battleType.battlesWon;
            var secondBattleResult = await BattlegroundHttp.StartSecondWaveBattle(MainDataStorage.AccountParams,
                MainDataStorage.RequestService,
                provinceID, battlesWon);
            _attritionController.SetValue(BattlegroundHttp
                .GetEntityByRequestMethod<PlayerParticipantResponse>(secondBattleResult, "getPlayerParticipant")
                .responseData.attrition.level);
        }
    }
}