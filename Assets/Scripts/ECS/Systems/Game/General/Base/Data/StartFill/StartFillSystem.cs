using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.General.Systems.StartFill
{
    internal sealed class StartFillSystem : IEcsInitSystem
    {
        public void Init()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                InfoUnitsContainer.SetAmountUnitsInInventor(UnitTypes.King, true, EconomyValues.AMOUNT_KING_MASTER);
                InfoUnitsContainer.SetAmountUnitsInInventor(UnitTypes.King, false, EconomyValues.AMOUNT_KING_OTHER);

                InfoUnitsContainer.SetAmountUnitsInInventor(UnitTypes.Pawn, true, EconomyValues.AMOUNT_PAWN_MASTER);
                InfoUnitsContainer.SetAmountUnitsInInventor(UnitTypes.Pawn, false, EconomyValues.AMOUNT_PAWN_OTHER);

                InfoUnitsContainer.SetAmountUnitsInInventor(UnitTypes.Rook, true, EconomyValues.AMOUNT_ROOK_MASTER);
                InfoUnitsContainer.SetAmountUnitsInInventor(UnitTypes.Rook, false, EconomyValues.AMOUNT_ROOK_OTHER);

                InfoUnitsContainer.SetAmountUnitsInInventor(UnitTypes.Bishop, true, EconomyValues.AMOUNT_BISHOP_MASTER);
                InfoUnitsContainer.SetAmountUnitsInInventor(UnitTypes.Bishop, false, EconomyValues.AMOUNT_BISHOP_OTHER);


                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Food, true, EconomyValues.AMOUNT_FOOD_MASTER);
                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Wood, true, EconomyValues.AMOUNT_WOOD_MASTER);
                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Ore, true, EconomyValues.AMOUNT_ORE_MASTER);
                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Iron, true, EconomyValues.AMOUNT_IRON_MASTER);
                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Gold, true, EconomyValues.AMOUNT_GOLD_MASTER);

                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Food, false, EconomyValues.AMOUNT_FOOD_OTHER);
                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Wood, false, EconomyValues.AMOUNT_WOOD_OTHER);
                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Ore, false, EconomyValues.AMOUNT_ORE_OTHER);
                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Iron, false, EconomyValues.AMOUNT_IRON_OTHER);
                ResourcesDataUIWorker.SetAmountResources(ResourceTypes.Gold, false, EconomyValues.AMOUNT_GOLD_OTHER);
            }
        }
    }
}
