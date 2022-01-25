using Game.Common;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEntities;
using static Game.Game.CellBuildE;
using static Game.Game.CellRiverE;
using System;

namespace Game.Game
{
    struct UpdateThirstyMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var ownUnit_0 = ref CellUnitEntities.Else(idx_0).OwnerC;
                ref var hp_0 = ref CellUnitEntities.Hp(idx_0).AmountC;
                ref var water_0 = ref CellUnitEntities.Water(idx_0).AmountC;

                ref var build_0 = ref Build<BuildingTC>(idx_0);


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
                        if (River(idx_0).HaveRiver)
                        {
                            CellUnitEntities.Water(idx_0).AmountC.Amount = CellUnitEntities.MaxWater(idx_0);
                        }
                        else
                        {
                            CellUnitEntities.Water(idx_0).AmountC.Take((int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.15f));


                            if (!water_0.Have)
                            {
                                float percent = 0;
                                switch (CellUnitEntities.Else(idx_0).UnitC.Unit)
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
                                CellUnitEntities.Hp(idx_0).AmountC.Take((int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * percent));


                                if (!hp_0.Have)
                                {
                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        CellBuildE.Remove(idx_0);
                                    }
                                    CellUnitEntities.Kill(idx_0);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}