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

            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var levUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;

                ref var tw_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).ToolWeapon;
                ref var twLevel_0 = ref Es.CellEs.UnitEs.ToolWeapon(idx_0).LevelTW;

                ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;

                ref var curFireCom = ref Es.CellEs.FireEs.Fire(idx_0).Fire;


                curFireCom.Disable();


                Es.CellEs.TrailEs.ResetAll(idx_0);

                if (unit_0.Have)
                {
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            if (tw_0.HaveTW)
                            {
                                Es.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons++;
                                UnitEs.ToolWeapon(idx_0).Reset();
                            }

                            UnitEs.AddToInventorAndRemove(idx_0, Es.InventorUnitsEs, Es.WhereUnitsEs);
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            Es.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons++;
                            UnitEs.ToolWeapon(idx_0).Reset();
                        }

                        UnitEs.AddToInventorAndRemove(idx_0, Es.InventorUnitsEs, Es.WhereUnitsEs);
                    }
                }


                if (build_0.Have)
                {
                    if (build_0.Is(BuildingTypes.Camp))
                    {
                        Es.WhereBuildingEs.HaveBuild(Es.CellEs.BuildEs.Build(idx_0), idx_0).HaveBuilding.Have = false;
                        Es.CellEs.BuildEs.Build(idx_0).Remove();
                    }
                }

                else
                {
                    if (Es.CellEs.EnvironmentEs.YoungForest( idx_0).HaveEnvironment)
                    {
                        Es.CellEs.EnvironmentEs.YoungForest( idx_0).Destroy(Es.WhereEnviromentEs);

                        Es.CellEs.EnvironmentEs.AdultForest( idx_0).SetNew(Es.WhereEnviromentEs);
                    }

                    if (!Es.CellEs.EnvironmentEs.Fertilizer( idx_0).HaveEnvironment
                        && !Es.CellEs.EnvironmentEs.Mountain( idx_0).HaveEnvironment
                        && !Es.CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 3)
                        {
                            Es.CellEs.EnvironmentEs.Fertilizer( idx_0).SetNew();
                        }
                    }
                }
            }
        }
    }
}