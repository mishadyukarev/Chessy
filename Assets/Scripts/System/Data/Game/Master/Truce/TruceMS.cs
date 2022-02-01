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

            foreach (byte idx_0 in CellEs.Idxs)
            {
                var unit_0 = UnitEs.Main(idx_0).UnitTC;
                var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;

                var tw_0 = UnitEs.ToolWeapon(idx_0).ToolWeaponTC;
                var twLevel_0 = UnitEs.ToolWeapon(idx_0).LevelTC;

                var build_0 = BuildEs.BuildingE(idx_0).BuildTC;

                ref var curFireCom = ref CellEs.FireEs.Fire(idx_0).Fire;


                curFireCom.Disable();


                CellEs.TrailEs.ResetAll(idx_0);

                if (UnitEs.Main(idx_0).HaveUnit(UnitStatEs))
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            if (tw_0.HaveTW)
                            {
                                Es.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons.Amount++;
                                UnitEs.ToolWeapon(idx_0).Reset();
                            }

                            UnitEs.Main(idx_0).AddToInventorAndRemove(Es.InventorUnitsEs, Es.WhereUnitsEs);
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            Es.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons.Amount++;
                            UnitEs.ToolWeapon(idx_0).Reset();
                        }

                        UnitEs.Main(idx_0).AddToInventorAndRemove(Es.InventorUnitsEs, Es.WhereUnitsEs);
                    }
                }


                if (build_0.Have)
                {
                    if (build_0.Is(BuildingTypes.Camp))
                    {
                        Es.WhereBuildingEs.HaveBuild(BuildEs.BuildingE(idx_0), idx_0).HaveBuilding.Have = false;
                        BuildEs.BuildingE(idx_0).Destroy(BuildEs, Es.WhereBuildingEs);
                    }
                }

                else
                {
                    if (CellEs.EnvironmentEs.YoungForest( idx_0).HaveEnvironment)
                    {
                        CellEs.EnvironmentEs.YoungForest( idx_0).Destroy(Es.WhereEnviromentEs);

                        CellEs.EnvironmentEs.AdultForest( idx_0).SetNew(Es.WhereEnviromentEs);
                    }

                    if (!CellEs.EnvironmentEs.Fertilizer( idx_0).HaveEnvironment
                        && !CellEs.EnvironmentEs.Mountain( idx_0).HaveEnvironment
                        && !CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            CellEs.EnvironmentEs.Fertilizer( idx_0).SetNew();
                        }
                    }
                }
            }
        }
    }
}