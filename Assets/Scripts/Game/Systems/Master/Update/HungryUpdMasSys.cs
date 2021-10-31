using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class HungryUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;

        public void Run()
        {
            for (var player = Support.MinPlayerType; player < Support.MaxPlayerType; player++)
            {
                var res = ResTypes.Food;

                if (InventResC.IsMinusRes(player, res))
                {
                    InventResC.ResetRes(player, res);

                    for (var unit = UnitTypes.Scout; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = (LevelUnitTypes)2; levUnit > 0; levUnit--)
                        {
                            foreach (var idx_0 in WhereUnitsC.IdxsUnits(player, unit, levUnit))
                            {
                                ref var unitC_0 = ref _cellUnitMainFilt.Get1(idx_0);
                                ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
                                ref var ownUnitC_0 = ref _cellUnitMainFilt.Get3(idx_0);

                                if (unitC_0.Is(UnitTypes.Scout))
                                {
                                    InventorUnitsC.AddUnit(player, UnitTypes.Scout, LevelUnitTypes.Wood);
                                }

                                WhereUnitsC.Remove(ownUnitC_0.Owner, unitC_0.Unit, levUnitC_0.Level, idx_0);
                                unitC_0.NoneUnit();

                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}