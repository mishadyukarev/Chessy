using Game.Common;
using UnityEngine;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct TruceMS : IEcsRunSystem
    {
        public void Run()
        {
            int random;

            foreach (byte idx_0 in Entities.CellEs.Idxs)
            {
                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                ref var levUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;

                ref var tw_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeaponC;
                ref var twLevel_0 = ref Entities.CellEs.UnitEs.ToolWeapon(idx_0).LevelC;

                ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;

                ref var curFireCom = ref Entities.CellEs.FireEs.Fire(idx_0).Fire;


                curFireCom.Disable();


                Entities.CellEs.TrailEs.ResetAll(idx_0);

                if (unit_0.Have)
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            if (tw_0.HaveTW)
                            {
                                InventorToolWeaponE.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player)++;
                                Entities.CellEs.UnitEs.Reset(idx_0);
                            }

                            Entities.CellEs.UnitEs.AddToInventor(idx_0);
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            InventorToolWeaponE.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player)++;
                            Entities.CellEs.UnitEs.Reset(idx_0);
                        }

                        Entities.CellEs.UnitEs.AddToInventor(idx_0);
                    }
                }


                if (build_0.Have)
                {
                    if (build_0.Is(BuildingTypes.Camp))
                    {
                        Entities.WhereBuildingEs.HaveBuild(Entities.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                        Entities.CellEs.BuildEs.Build(idx_0).Remove();
                    }
                }

                else
                {
                    if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).Resources.Have)
                    {
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).Remove();

                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).SetNew();
                    }

                    if (!Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Have
                        && !Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_0).Resources.Have
                        && !Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Fertilizer, idx_0).SetNew();
                        }
                    }
                }
            }
        }
    }
}