using Game.Common;
using UnityEngine;

namespace Game.Game
{
    sealed class TruceMS : SystemAbstract, IEcsRunSystem
    {
        internal TruceMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            int random;

            foreach (byte idx_0 in CellWorker.Idxs)
            {

                Es.EffectEs(idx_0).FireE.Disable();


                Es.TrailEs(idx_0).DestroyAll();

                if (Es.UnitEs(idx_0).UnitE.HaveUnit)
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (Es.UnitE(idx_0).Is(PlayerTypes.First))
                        {
                            if (Es.ExtraTWE(idx_0).HaveToolWeapon)
                            {
                                Es.InventorToolWeaponEs.ToolWeapons(Es.ExtraTWE(idx_0).ToolWeapon, Es.ExtraTWE(idx_0).LevelT, Es.UnitE(idx_0).Owner).Add();
                                Es.UnitEs(idx_0).ExtraToolWeaponE.Reset();
                            }

                            Es.UnitE(idx_0).AddToInventorAndRemove(Es.InventorUnitsEs);
                        }
                    }
                    else
                    {

                        if (Es.ExtraTWE(idx_0).HaveToolWeapon)
                        {
                            Es.InventorToolWeaponEs.ToolWeapons(Es.ExtraTWE(idx_0).ToolWeapon, Es.ExtraTWE(idx_0).LevelT, Es.UnitE(idx_0).Owner).Add();
                            Es.UnitEs(idx_0).ExtraToolWeaponE.Reset();
                        }

                        Es.UnitE(idx_0).AddToInventorAndRemove(Es.InventorUnitsEs);
                    }
                }


                if (Es.BuildingE(idx_0).HaveBuilding)
                {
                    if (Es.BuildingE(idx_0).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                        BuildEs(idx_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (Es.EnvironmentEs(idx_0).YoungForest.HaveEnvironment)
                    {
                        Es.EnvironmentEs(idx_0).YoungForest.Destroy();

                        Es.EnvironmentEs(idx_0).AdultForest.SetRandomResources();
                    }

                    if (!Es.EnvironmentEs(idx_0).Fertilizer.HaveEnvironment
                        && !Es.EnvironmentEs(idx_0).Mountain.HaveEnvironment
                        && !Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            Es.EnvironmentEs(idx_0).Fertilizer.SetRandomResources();
                        }
                    }
                }
            }
        }
    }
}