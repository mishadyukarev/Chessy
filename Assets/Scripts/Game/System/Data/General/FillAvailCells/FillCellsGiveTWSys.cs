using Leopotam.Ecs;

namespace Game.Game
{
    internal sealed class FillCellsGiveTWSys : IEcsRunSystem
    {
        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < EntityDataPool.AmountAllCells; idx_0++)
            {
                ref var unit_0 = ref EntityDataPool.GetUnitCellC<UnitC>(idx_0);
                ref var owUnit_0 = ref EntityDataPool.GetUnitCellC<OwnerC>(idx_0);


                if (unit_0.HaveUnit)
                {
                    if (unit_0.Is(UnitTypes.Pawn))
                    {
                        //cellsGiveTWCom.Add(ToolWeaponTypes.Axe,)
                    }

                    else if (unit_0.Is(UnitTypes.Archer))
                    {

                    }
                }
            }
        }
    }
}
