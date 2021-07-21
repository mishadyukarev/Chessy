using Assets.Scripts;
using Assets.Scripts.Static;
using static Assets.Scripts.Main;

internal sealed class EconomyUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        var amountAddingFood = _eGM.BuildingsEnt_BuildingsCom.AmountBuildings(BuildingTypes.Farm, Instance.IsMasterClient)
            * _eGM.BuildingsEnt_UpgradeBuildingsCom.AmountUpgrades(BuildingTypes.Farm, Instance.IsMasterClient)
            - UnitInfoManager.AmountUnitsInGame(UnitTypes.Pawn, Instance.IsMasterClient)
            - UnitInfoManager.AmountUnitsInGame(UnitTypes.PawnSword, Instance.IsMasterClient)
            - UnitInfoManager.AmountUnitsInGame(UnitTypes.Rook, Instance.IsMasterClient)
            - UnitInfoManager.AmountUnitsInGame(UnitTypes.RookCrossbow, Instance.IsMasterClient)
            - UnitInfoManager.AmountUnitsInGame(UnitTypes.Bishop, Instance.IsMasterClient)
            - UnitInfoManager.AmountUnitsInGame(UnitTypes.BishopCrossbow, Instance.IsMasterClient);

        _eGM.FoodAddingEnt_AmountCom.Amount = amountAddingFood;

        if (amountAddingFood < 0)
            _eGM.FoodAddingEnt_TextMeshProUGUICom.SetText(amountAddingFood.ToString());
        else _eGM.FoodAddingEnt_TextMeshProUGUICom.SetText("+ " + amountAddingFood.ToString());

        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Food, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Wood, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Ore, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Iron, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Gold, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, Instance.IsMasterClient).ToString());
    }
}
