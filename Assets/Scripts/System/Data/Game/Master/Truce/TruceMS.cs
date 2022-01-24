using Game.Common;
using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;
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
                ref var unit_0 = ref Unit(idx_0);
                ref var levUnit_0 = ref EntitiesPool.UnitElse.Level(idx_0);
                ref var ownUnit_0 = ref EntitiesPool.UnitElse.Owner(idx_0);

                ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref UnitTW<LevelTC>(idx_0);

                ref var build_0 = ref Build<BuildingTC>(idx_0);

                ref var curFireCom = ref Fire<HaveEffectC>(idx_0);


                curFireCom.Disable();


                ResetAll(idx_0);

                if (unit_0.Have)
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            if (tw_0.HaveTW)
                            {
                                InventorToolWeaponE.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player)++;
                                CellUnitTWE.Reset(idx_0);
                            }

                            AddToInventor(idx_0);
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            InventorToolWeaponE.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player)++;
                            CellUnitTWE.Reset(idx_0);
                        }

                        AddToInventor(idx_0);
                    }
                }


                if (build_0.Have)
                {
                    if (build_0.Is(BuildingTypes.Camp))
                    {
                        CellBuildE.Remove(idx_0);
                    }
                }

                else
                {
                    if (Resources(EnvironmentTypes.YoungForest, idx_0).Have)
                    {
                        Remove(EnvironmentTypes.YoungForest, idx_0);

                        SetNew(EnvironmentTypes.AdultForest, idx_0);
                    }

                    if (!Resources(EnvironmentTypes.Fertilizer, idx_0).Have
                        && !Resources(EnvironmentTypes.Mountain, idx_0).Have
                        && !Resources(EnvironmentTypes.AdultForest, idx_0).Have)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            SetNew(EnvironmentTypes.Fertilizer, idx_0);
                        }
                    }
                }
            }
        }
    }
}