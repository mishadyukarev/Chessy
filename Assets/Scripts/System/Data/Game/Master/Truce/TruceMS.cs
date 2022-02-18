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

            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                Es.HaveFire(idx_0) = false;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    Es.CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                }
                

                if (Es.UnitTC(idx_0).HaveUnit)
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (Es.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                        {
                            if (Es.UnitEs(idx_0).ExtraToolWeaponTC.HaveToolWeapon)
                            {
                                Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).LevelE(Es.UnitEs(idx_0).ExtraTWLevelTC.Level).ToolWeapons(Es.UnitEs(idx_0).ExtraToolWeaponTC.ToolWeapon).Amount++;
                                Es.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                            }

                            Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).UnitsInfoE(Es.UnitTC(idx_0).Unit).HaveInInventor = true;
                            Es.UnitTC(idx_0).Unit = UnitTypes.None;
                        }
                    }
                    else
                    {

                        if (Es.UnitEs(idx_0).ExtraToolWeaponTC.HaveToolWeapon)
                        {
                            Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).LevelE(Es.UnitEs(idx_0).ExtraTWLevelTC.Level).ToolWeapons(Es.UnitEs(idx_0).ExtraToolWeaponTC.ToolWeapon).Amount++;

                            Es.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                        }

                        Es.PlayerE(Es.UnitPlayerTC(idx_0).Player).UnitsInfoE(Es.UnitTC(idx_0).Unit).HaveInInventor = true;
                        Es.UnitTC(idx_0).Unit = UnitTypes.None;
                    }
                }


                if (Es.BuildTC(idx_0).HaveBuilding)
                {
                    if (Es.BuildTC(idx_0).Is(BuildingTypes.Camp))
                    {
                        //Es.WhereBuildingEs.HaveBuild(BuildEs(idx_0).BuildingE, idx_0).HaveBuilding.Have = false;
                        //Es.BuildE(idx_0).BuildingE.Destroy(Es);
                    }
                }

                else
                {
                    if (Es.YoungForestC(idx_0).HaveAny)
                    {
                        Es.YoungForestC(idx_0).Resources = 0;

                       // Es.AdultForestC(idx_0).SetRandomResources();
                    }

                    if (!Es.EnvironmentEs(idx_0).FertilizeC.HaveAny
                        && !Es.EnvironmentEs(idx_0).MountainC.HaveAny
                        && !Es.AdultForestC(idx_0).HaveAny)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            //Es.EnvironmentEs(idx_0).FertilizeC.Resources = CellEnvironmentValues.
                        }
                    }
                }
            }
        }
    }
}