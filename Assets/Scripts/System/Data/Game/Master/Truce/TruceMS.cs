using Game.Common;
using UnityEngine;
using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityCellTrailPool;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntityCellFirePool;

namespace Game.Game
{
    struct TruceMS : IEcsRunSystem
    {
        public void Run()
        {
            int random;

            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerC>(idx_0);

                ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref UnitTW<LevelC>(idx_0);

                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var build_0 = ref Build<BuildC>(idx_0);

                ref var curFireCom = ref Fire<HaveEffectC>(idx_0);
                ref var trail_0 = ref Trail<TrailCellEC>(idx_0);


                curFireCom.Disable();


                trail_0.ResetAll();

                if (unit_0.Have)
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            if (tw_0.HaveTW)
                            {
                                InvTWC.Add(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player);
                                UnitTW<UnitTWCellEC>(idx_0).Reset();
                            }

                            Unit<UnitCellEC>(idx_0).AddToInventor();
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            InvTWC.Add(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player);
                            UnitTW<UnitTWCellEC>(idx_0).Reset();
                        }

                        Unit<UnitCellEC>(idx_0).AddToInventor();
                    }
                }


                if (build_0.Have)
                {
                    if (build_0.Is(BuildTypes.Camp))
                    {
                        buildCell_0.Remove();
                    }
                }

                else
                {
                    if (Environment<HaveEnvironmentC>(EnvTypes.YoungForest, idx_0).Have)
                    {
                        Environment<EnvCellEC>(EnvTypes.YoungForest, idx_0).Remove();

                        Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).SetNew();
                    }

                    if (!Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, idx_0).Have
                        && !Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_0).Have
                        && !Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).SetNew();
                        }
                    }
                }
            }
        }
    }
}