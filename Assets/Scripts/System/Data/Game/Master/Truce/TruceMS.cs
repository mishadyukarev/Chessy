using Game.Common;
using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellBuildEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireE;
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
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var levUnit_0 = ref CellUnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;

                ref var tw_0 = ref CellUnitEs.ToolWeapon(idx_0).ToolWeaponC;
                ref var twLevel_0 = ref CellUnitEs.ToolWeapon(idx_0).LevelC;

                ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;

                ref var curFireCom = ref CellFireEs.Fire(idx_0).Fire;


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
                                CellUnitEs.Reset(idx_0);
                            }

                            AddToInventor(idx_0);
                        }
                    }
                    else
                    {

                        if (tw_0.HaveTW)
                        {
                            InventorToolWeaponE.ToolWeapons<AmountC>(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player)++;
                            CellUnitEs.Reset(idx_0);
                        }

                        AddToInventor(idx_0);
                    }
                }


                if (build_0.Have)
                {
                    if (build_0.Is(BuildingTypes.Camp))
                    {
                        CellBuildEs.Remove(idx_0);
                    }
                }

                else
                {
                    if (Environment(EnvironmentTypes.YoungForest, idx_0).Resources.Have)
                    {
                        Remove(EnvironmentTypes.YoungForest, idx_0);

                        SetNew(EnvironmentTypes.AdultForest, idx_0);
                    }

                    if (!Environment(EnvironmentTypes.Fertilizer, idx_0).Resources.Have
                        && !Environment(EnvironmentTypes.Mountain, idx_0).Resources.Have
                        && !Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
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