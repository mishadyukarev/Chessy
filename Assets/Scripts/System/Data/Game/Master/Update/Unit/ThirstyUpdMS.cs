using Game.Common;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellBuildE;
using static Game.Game.EntityCellRiverPool;

namespace Game.Game
{
    struct ThirstyUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);
                ref var hp_0 = ref CellUnitHpEs.Hp<AmountC>(idx_0);
                ref var water_0 = ref CellUnitWaterEs.Water<AmountC>(idx_0);

                ref var riverC_0 = ref River<RiverC>(idx_0);

                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var build_0 = ref Build<BuildingTC>(idx_0);


                if (unit_0.Have)
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
                            CellUnitWaterEs.TakeWater(idx_0);
                            if (!water_0.Have)
                            {
                                CellUnitWaterEs.ExecuteThirsty(idx_0);

                                if (!hp_0.Have)
                                {
                                    if (build_0.Is(BuildingTypes.Camp))
                                    {
                                        CellBuildE.Remove(idx_0);
                                    }

                                    Unit<UnitCellEC>(idx_0).Kill();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}