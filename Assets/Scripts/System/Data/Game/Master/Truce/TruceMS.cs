using Game.Common;
using UnityEngine;

namespace Game.Game
{
    sealed class TruceMS : SystemCellAbstract, IEcsRunSystem
    {
        public TruceMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            int random;

            foreach (byte idx_0 in CellWorker.Idxs)
            {
                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;

                var tw_0 = UnitEs(idx_0).ToolWeaponE.ToolWeaponTC;
                var twLevel_0 = UnitEs(idx_0).ToolWeaponE.LevelTC;

                var build_0 = BuildEs(idx_0).BuildingE.BuildTC;

                EffectEs(idx_0).FireE.Disable();


                TrailEs(idx_0).DestroyAll();

                if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)))
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            if (tw_0.HaveTW)
                            {
                                Es.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons.Amount++;
                                UnitEs(idx_0).ToolWeaponE.Reset();
                            }

                            UnitEs(idx_0).MainE.AddToInventorAndRemove(Es.InventorUnitsEs, Es.WhereUnitsEs);
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            Es.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons.Amount++;
                            UnitEs(idx_0).ToolWeaponE.Reset();
                        }

                        UnitEs(idx_0).MainE.AddToInventorAndRemove(Es.InventorUnitsEs, Es.WhereUnitsEs);
                    }
                }


                if (build_0.Have)
                {
                    if (build_0.Is(BuildingTypes.Camp))
                    {
                        Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                        BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);
                    }
                }

                else
                {
                    if (EnvironmentEs(idx_0).YoungForest.HaveEnvironment)
                    {
                        EnvironmentEs(idx_0).YoungForest.Destroy(Es.WhereEnviromentEs);

                        EnvironmentEs(idx_0).AdultForest.SetNewRandom(Es.WhereEnviromentEs);
                    }

                    if (!EnvironmentEs(idx_0).Fertilizer.HaveEnvironment
                        && !EnvironmentEs(idx_0).Mountain.HaveEnvironment
                        && !EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            EnvironmentEs(idx_0).Fertilizer.SetNew();
                        }
                    }
                }
            }
        }
    }
}