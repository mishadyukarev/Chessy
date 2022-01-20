using Game.Common;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.EntityCellRiverPool;
using System;

namespace Game.Game
{
    struct ThirstyUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit(idx_0);
                ref var ownUnit_0 = ref CellUnitElseEs.Owner(idx_0);
                ref var hp_0 = ref CellUnitHpEs.Hp(idx_0);
                ref var water_0 = ref CellUnitWaterEs.Water<AmountC>(idx_0);

                ref var riverC_0 = ref River<RiverC>(idx_0);

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
                        if (riverC_0.HaveNearRiver)
                        {
                            CellUnitWaterEs.SetMaxWater(idx_0);
                        }
                        else
                        {
                            CellUnitWaterEs.Water<AmountC>(idx_0).Take((int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.15f));


                            if (!water_0.Have)
                            {
                                float percent = 0;
                                switch (CellUnitEs.Unit(idx_0).Unit)
                                {
                                    case UnitTypes.None: throw new Exception();
                                    case UnitTypes.King: percent = 0.4f; break;
                                    case UnitTypes.Pawn: percent = 0.5f; break;
                                    case UnitTypes.Archer: percent = 0.5f; break;
                                    case UnitTypes.Scout: percent = 0.5f; break;
                                    case UnitTypes.Elfemale: percent = 0.5f; break;
                                    default: throw new Exception();
                                }
                                CellUnitHpEs.Hp(idx_0).Take((int)(CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * percent));


                                if (!hp_0.Have)
                                {
                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        CellBuildE.Remove(idx_0);
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