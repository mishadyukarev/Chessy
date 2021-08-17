using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.Workers;
using Leopotam.Ecs;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.Systems.Game.Master
{
    internal sealed class InitGameMasterSystem : IEcsInitSystem
    {
        private EcsWorld _currentGameWorld = default;

        private EcsFilter<DonerDataUIComponent> _donerFilter = default;
        private EcsFilter<InventorResourcesComponent> _inventorResFilter = default;
        private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;

        public void Init()
        {
            _currentGameWorld.NewEntity()
                .Replace(new InfoMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForGettingUnitMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForSettingUnitMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForAttackMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForShiftMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForDonerMasCom())
                .Replace(new NeedActiveSomethingMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForBuildingMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForSeedingMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ConditionMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForCircularAttackMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForCreatingUnitMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForDestroyMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForFireMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForReadyMasCom())
                .Replace(new NeedActiveSomethingMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForUpgradeMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForGiveToolWeaponComp());


            _donerFilter.Get1(0).SetDoned(false, true);

            int random;

            foreach (byte curIdxCell in _cellEnvFilter)
            {
                var curXyCell = _xyCellFilter.GetXyCell(curIdxCell);

                ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(curIdxCell);


                if (curXyCell[1] >= 4 && curXyCell[1] <= 6)
                {
                    random = UnityEngine.Random.Range(1, 100);
                    if (random <= START_MOUNTAIN_PERCENT)
                        curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.Mountain);
                    else
                    {
                        random = UnityEngine.Random.Range(1, 100);
                        if (random <= START_FOREST_PERCENT)
                        {
                            curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.AdultForest);
                        }

                        random = UnityEngine.Random.Range(1, 100);
                        if (random <= START_HILL_PERCENT)
                            curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.Hill);
                    }
                }
                else
                {

                    random = UnityEngine.Random.Range(1, 100);
                    if (random <= START_FOREST_PERCENT)
                    {
                        curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.AdultForest);
                    }
                    else
                    {
                        random = UnityEngine.Random.Range(1, 100);
                        if (random <= START_FERTILIZER_PERCENT)
                        {
                            curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.Fertilizer);
                        }
                    }
                }
            }

            CameraComponent.ResetRotation();
            CameraComponent.SetPosition(Main.Instance.transform.position + CameraComponent.PosForCamera);

            ref var unitInventorCom = ref _inventorUnitsFilter.Get1(0);
            ref var inventorResCom = ref _inventorResFilter.Get1(0);


            unitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, true, EconomyValues.AMOUNT_KING_MASTER);
            unitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, false, EconomyValues.AMOUNT_KING_OTHER);

            unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, true, EconomyValues.AMOUNT_PAWN_MASTER);
            unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, false, EconomyValues.AMOUNT_PAWN_OTHER);

            unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, true, EconomyValues.AMOUNT_ROOK_MASTER);
            unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, false, EconomyValues.AMOUNT_ROOK_OTHER);

            unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, true, EconomyValues.AMOUNT_BISHOP_MASTER);
            unitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, false, EconomyValues.AMOUNT_BISHOP_OTHER);


            inventorResCom.SetAmountResources(ResourceTypes.Food, true, EconomyValues.AMOUNT_FOOD_MASTER);
            inventorResCom.SetAmountResources(ResourceTypes.Wood, true, EconomyValues.AMOUNT_WOOD_MASTER);
            inventorResCom.SetAmountResources(ResourceTypes.Ore, true, EconomyValues.AMOUNT_ORE_MASTER);
            inventorResCom.SetAmountResources(ResourceTypes.Iron, true, EconomyValues.AMOUNT_IRON_MASTER);
            inventorResCom.SetAmountResources(ResourceTypes.Gold, true, EconomyValues.AMOUNT_GOLD_MASTER);

            inventorResCom.SetAmountResources(ResourceTypes.Food, false, EconomyValues.AMOUNT_FOOD_OTHER);
            inventorResCom.SetAmountResources(ResourceTypes.Wood, false, EconomyValues.AMOUNT_WOOD_OTHER);
            inventorResCom.SetAmountResources(ResourceTypes.Ore, false, EconomyValues.AMOUNT_ORE_OTHER);
            inventorResCom.SetAmountResources(ResourceTypes.Iron, false, EconomyValues.AMOUNT_IRON_OTHER);
            inventorResCom.SetAmountResources(ResourceTypes.Gold, false, EconomyValues.AMOUNT_GOLD_OTHER);
        }
    }
}
