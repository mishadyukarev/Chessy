using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.Workers.Game.Else.Data;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.ECS.Game.General.Systems.StartFill
{
    internal sealed class InitSystem : IEcsInitSystem
    {
        private EcsWorld _world;


        internal GameObject BackGroundGO;
        internal SpriteRenderer BackGroundSR;


        private static EcsEntity _infoUnitsEnt;
        internal static ref XyUnitsComponent XyUnitsCom => ref _infoUnitsEnt.Get<XyUnitsComponent>();
        internal static ref XyUnitsContitionComponent XyUnitsContitionCom => ref _infoUnitsEnt.Get<XyUnitsContitionComponent>();


        private static EcsEntity _infoBuildingsEnt;
        internal static ref XyBuildingsComponent XyBuildingsCom => ref _infoBuildingsEnt.Get<XyBuildingsComponent>();
        internal static ref UpgradesBuildingsComponent UpgradesBuildingsCom => ref _infoBuildingsEnt.Get<UpgradesBuildingsComponent>();


        private static EcsEntity _inventor;
        internal static ref UnitInventorComponent UnitInventorCom => ref _inventor.Get<UnitInventorComponent>();


        private static EcsEntity _infoCellsEnt;
        internal static ref XyStartCellsComponent XyStartCellsCom => ref _infoCellsEnt.Get<XyStartCellsComponent>();


        private static EcsEntity _fromInfoEnt;
        internal static ref FromInfoComponent FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();


        private static EcsEntity _mistakeEconomyEventEnt;
        internal static ref MistakeEconomyComponent MistakeEconomyCom => ref _mistakeEconomyEventEnt.Get<MistakeEconomyComponent>();


        public void Init()
        {
            BackGroundGO = GameObject.Instantiate(MainDataCommSys.ResourcesEnt_ResourcesCommonCom.PrefabConfig.BackGroundCollider2D,
                Main.Instance.transform.position + new Vector3(7, 5.5f, 2), Main.Instance.transform.rotation);

            MainDataCommSys.ToggleZoneEnt_ParentCom.Attach(BackGroundGO.transform);
            BackGroundSR = BackGroundGO.GetComponent<SpriteRenderer>();
            BackGroundSR.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);


            _infoUnitsEnt = _world.NewEntity()
                .Replace(new XyUnitsComponent(new Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>()))
                .Replace(new XyUnitsContitionComponent(new Dictionary<ConditionUnitTypes, Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>>()));

            _infoBuildingsEnt = _world.NewEntity()
                .Replace(new XyBuildingsComponent(new Dictionary<BuildingTypes, Dictionary<bool, List<int[]>>>()))
                .Replace(new UpgradesBuildingsComponent(new Dictionary<BuildingTypes, Dictionary<bool, int>>()));

            _inventor = _world.NewEntity()
                .Replace(new UnitInventorComponent(new Dictionary<UnitTypes, Dictionary<bool, int>>()));

            _fromInfoEnt = _world.NewEntity()
                .Replace(new FromInfoComponent());

            _mistakeEconomyEventEnt = _world.NewEntity()
                .Replace(new MistakeEconomyComponent(new Dictionary<ResourceTypes, UnityEvent>()));

            new MistakeEconomyEventDataWorker(_world);

            var listMaster = new List<int[]>();
            var listOther = new List<int[]>();

            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    if (y < 3 && x > 2 && x < 12)
                    {
                        listMaster.Add(new int[] { x, y });
                    }
                    else if (y > 7 && x > 2 && x < 12)
                    {
                        listOther.Add(new int[] { x, y });
                    }
                }
            var dict = new Dictionary<bool, List<int[]>>();
            dict.Add(true, listMaster);
            dict.Add(false, listOther);

            _infoCellsEnt = _world.NewEntity()
                .Replace(new XyStartCellsComponent(dict));








            if (PhotonNetwork.IsMasterClient)
            {
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, true, EconomyValues.AMOUNT_KING_MASTER);
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.King, false, EconomyValues.AMOUNT_KING_OTHER);

                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, true, EconomyValues.AMOUNT_PAWN_MASTER);
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Pawn, false, EconomyValues.AMOUNT_PAWN_OTHER);

                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, true, EconomyValues.AMOUNT_ROOK_MASTER);
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Rook, false, EconomyValues.AMOUNT_ROOK_OTHER);

                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, true, EconomyValues.AMOUNT_BISHOP_MASTER);
                UnitInventorCom.SetAmountUnitsInInventor(UnitTypes.Bishop, false, EconomyValues.AMOUNT_BISHOP_OTHER);


                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Food, true, EconomyValues.AMOUNT_FOOD_MASTER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Wood, true, EconomyValues.AMOUNT_WOOD_MASTER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Ore, true, EconomyValues.AMOUNT_ORE_MASTER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Iron, true, EconomyValues.AMOUNT_IRON_MASTER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Gold, true, EconomyValues.AMOUNT_GOLD_MASTER);

                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Food, false, EconomyValues.AMOUNT_FOOD_OTHER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Wood, false, EconomyValues.AMOUNT_WOOD_OTHER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Ore, false, EconomyValues.AMOUNT_ORE_OTHER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Iron, false, EconomyValues.AMOUNT_IRON_OTHER);
                ResourcesUIDataContainer.SetAmountResources(ResourceTypes.Gold, false, EconomyValues.AMOUNT_GOLD_OTHER);
            }
        }
    }
}
