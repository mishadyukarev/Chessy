using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class HungryUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<BuildC, OwnerC> _cellBuildFilt = default;
        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;

        public void Run()
        {
            for (var player = Support.MinPlayerType; player < Support.MaxPlayerType; player++)
            {
                var res = ResTypes.Food;

                if (InvResC.IsMinusRes(player, res))
                {
                    InvResC.ResetRes(player, res);

                    for (var unit = UnitTypes.Pawn; unit >= UnitTypes.Elfemale; unit++)
                    {
                        for (var levUnit = (LevelUnitTypes)2; levUnit > 0; levUnit--)
                        {
                            foreach (var idx_0 in WhereUnitsC.IdxsUnits(player, unit, levUnit))
                            {
                                ref var unit_0 = ref _cellUnitMainFilt.Get1(idx_0);
                                ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
                                ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);

                                ref var build_0 = ref _cellBuildFilt.Get1(idx_0);
                                ref var ownBuild_0 = ref _cellBuildFilt.Get2(idx_0);


                                if (unit_0.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale}))
                                {
                                    ScoutHeroCooldownC.SetStandCooldown(ownUnit_0.Owner, unit_0.Unit);
                                    InvUnitsC.AddUnit(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level);
                                }

                                if (build_0.Is(BuildTypes.Camp))
                                {
                                    WhereBuildsC.Remove(ownBuild_0.Owner, build_0.Type, idx_0);
                                    build_0.Remove();
                                }

                                WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                                unit_0.Reset();

                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}