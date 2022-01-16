using Game.Common;
using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.EntityCellFirePool;
using static Game.Game.CellUnitTWE;

namespace Game.Game
{
    struct TruceMS : IEcsRunSystem
    {
        public void Run()
        {
            int random;

            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var levUnit_0 = ref Unit<LevelTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);

                ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref UnitTW<LevelTC>(idx_0);

                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var build_0 = ref Build<BuildingC>(idx_0);

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
                                EntInventorToolWeapon.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).Add();
                                UnitTW<UnitTWCellEC>(idx_0).Reset();
                            }

                            Unit<UnitCellEC>(idx_0).AddToInventor();
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            EntInventorToolWeapon.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).Add();
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

                        SetNew(EnvTypes.AdultForest, idx_0);
                    }

                    if (!Environment<HaveEnvironmentC>(EnvTypes.Fertilizer, idx_0).Have
                        && !Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_0).Have
                        && !Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            SetNew(EnvTypes.Fertilizer, idx_0);
                        }
                    }
                }
            }
        }
    }
}