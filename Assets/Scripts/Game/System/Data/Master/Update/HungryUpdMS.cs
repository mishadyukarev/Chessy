using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class HungryUpdMS : IEcsRunSystem
    {
        private EcsFilter<BuildC, OwnerC> _buildF = default;
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                var res = ResTypes.Food;



                if (InvResC.IsMinusRes(res, player))
                {
                    InvResC.ResetRes(res, player);

                    for (var unit = UnitTypes.Elfemale; unit >= UnitTypes.Pawn; unit--)
                    {
                        for (var levUnit = LevelTypes.Second; levUnit > LevelTypes.None; levUnit--)
                        {
                            foreach (var idx_0 in WhereUnitsC.Idxs(unit, levUnit, player))
                            {
                                ref var unit_0 = ref _unitF.Get1(idx_0);
                                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                                ref var build_0 = ref _buildF.Get1(idx_0);
                                ref var ownBuild_0 = ref _buildF.Get2(idx_0);


                                if (unit_0.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale}))
                                {
                                    ScoutHeroCooldownC.SetStandCooldown(ownUnit_0.Owner, unit_0.Unit);
                                    InvUnitsC.AddUnit(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner);
                                }

                                if (build_0.Is(BuildTypes.Camp))
                                {
                                    build_0.Remove(ownBuild_0.Owner);
                                }

                                unit_0.Remove(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner);

                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}