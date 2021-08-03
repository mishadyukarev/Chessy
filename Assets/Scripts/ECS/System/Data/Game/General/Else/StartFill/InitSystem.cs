using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Game.General.Systems.StartFill
{
    internal sealed class InitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        private static EcsEntity _xyUnitsEnt;
        internal static ref XyUnitsComponent XyUnitsCom => ref _xyUnitsEnt.Get<XyUnitsComponent>();
        internal static ref UnitInventorComponent UnitInventorCom => ref _xyUnitsEnt.Get<UnitInventorComponent>();


        public void Init()
        {
            _xyUnitsEnt = _world.NewEntity()
                .Replace(new XyUnitsComponent(new Dictionary<UnitTypes, Dictionary<bool, List<int[]>>>()))
                .Replace(new UnitInventorComponent(new Dictionary<UnitTypes, Dictionary<bool, int>>()));

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
