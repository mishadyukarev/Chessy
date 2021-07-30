using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Systems.StartFill
{
    internal sealed class StartFillSystem : IEcsInitSystem
    {
        public void Init()
        {
            if (Main.Instance.IsMasterClient)
            {
                InventorUnitsDataWorker.SetAmountUnitsInInventor(UnitTypes.King, true, EconomyValues.AMOUNT_KING_MASTER);
                InventorUnitsDataWorker.SetAmountUnitsInInventor(UnitTypes.King, false, EconomyValues.AMOUNT_KING_OTHER);

                InventorUnitsDataWorker.SetAmountUnitsInInventor(UnitTypes.Pawn, true, EconomyValues.AMOUNT_PAWN_MASTER);
                InventorUnitsDataWorker.SetAmountUnitsInInventor(UnitTypes.Pawn, false, EconomyValues.AMOUNT_PAWN_OTHER);

                InventorUnitsDataWorker.SetAmountUnitsInInventor(UnitTypes.Rook, true, EconomyValues.AMOUNT_ROOK_MASTER);
                InventorUnitsDataWorker.SetAmountUnitsInInventor(UnitTypes.Rook, false, EconomyValues.AMOUNT_ROOK_OTHER);

                InventorUnitsDataWorker.SetAmountUnitsInInventor(UnitTypes.Bishop, true, EconomyValues.AMOUNT_BISHOP_MASTER);
                InventorUnitsDataWorker.SetAmountUnitsInInventor(UnitTypes.Bishop, false, EconomyValues.AMOUNT_BISHOP_OTHER);


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
