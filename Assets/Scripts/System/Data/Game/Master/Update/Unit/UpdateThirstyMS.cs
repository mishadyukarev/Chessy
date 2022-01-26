using Game.Common;
using System;
using static Game.Game.CellEs;
using static Game.Game.CellRiverEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct UpdateThirstyMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;
                ref var hp_0 = ref CellUnitEs.Hp(idx_0).AmountC;
                ref var water_0 = ref CellUnitEs.Water(idx_0).AmountC;

                ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;


                if (unit_0.Have && !unit_0.IsAnimal)
                {
                    var canExecute = false;
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (River(idx_0).RiverTC.HaveRiver)
                        {
                            CellUnitEs.Water(idx_0).AmountC.Amount = CellUnitEs.MaxWater(idx_0);
                        }
                        else
                        {
                            CellUnitEs.Water(idx_0).AmountC.Take((int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.15f));


                            if (!water_0.Have)
                            {
                                float percent = 0;
                                switch (CellUnitEs.Else(idx_0).UnitC.Unit)
                                {
                                    case UnitTypes.None: throw new Exception();
                                    case UnitTypes.King: percent = 0.4f; break;
                                    case UnitTypes.Pawn: percent = 0.5f; break;
                                    case UnitTypes.Archer: percent = 0.5f; break;
                                    case UnitTypes.Scout: percent = 0.5f; break;
                                    case UnitTypes.Elfemale: percent = 0.5f; break;
                                    case UnitTypes.Snowy: percent = 0.5f; break;
                                    default: throw new Exception();
                                }
                                CellUnitEs.Hp(idx_0).AmountC.Take((int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * percent));


                                if (!hp_0.Have)
                                {
                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        CellBuildEs.Remove(idx_0);
                                    }
                                    CellUnitEs.Kill(idx_0);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}