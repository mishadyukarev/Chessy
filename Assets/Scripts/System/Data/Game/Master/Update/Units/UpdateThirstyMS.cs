using Game.Common;

namespace Game.Game
{
    sealed class UpdateThirstyMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateThirstyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitE(idx_0).HaveUnit && !Es.UnitE(idx_0).IsAnimal)
                {
                    var canExecute = false;
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (Es.UnitE(idx_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (Es.RiverEs(idx_0).RiverE.HaveRiverNear)
                        {
                            Es.UnitE(idx_0).SetMaxWater(Es.UnitStatUpgradesEs);
                        }
                        else
                        {
                            
                            Es.UnitE(idx_0).TakeWater(CellUnitStatWaterValues.NeedWaterThirsty(Es.UnitE(idx_0).Unit));

                            if (!Es.UnitE(idx_0).HaveWater)
                            {
                                float percent = CellUnitStatHpValues.ThirstyPercent(Es.UnitE(idx_0).Unit);

                                Es.UnitE(idx_0).TakeHp(Es, CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS * percent);

                                if (!Es.UnitE(idx_0).HaveUnit)
                                {
                                    if (Es.BuildingE(idx_0).Is(BuildingTypes.Camp))
                                    {
                                        Es.BuildingE(idx_0).Destroy(Es);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}