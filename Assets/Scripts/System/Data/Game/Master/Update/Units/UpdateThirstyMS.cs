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
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit && !E.UnitMainE(idx_0).IsAnimal)
                {
                    var canExecute = false;
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (E.UnitPlayerTC(idx_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (E.RiverEs(idx_0).RiverTC.HaveRiverNear)
                        {
                            //Es.UnitE(idx_0).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                        }
                        else
                        {
                            
                            E.UnitWaterC(idx_0).Water -= CellUnitStatWater_Values.NeedWaterForThirsty(E.UnitTC(idx_0).Unit);

                            if (E.UnitWaterC(idx_0).Water <= 0)
                            {
                                float percent = CellUnitStatHp_Values.ThirstyPercent(E.UnitTC(idx_0).Unit);

                                //Es.UnitE(idx_0).Take(Es, CellUnitStatWaterValues.WATER_MAX_STANDART * percent);

                                if (!E.UnitTC(idx_0).HaveUnit)
                                {
                                    if (E.BuildTC(idx_0).Is(BuildingTypes.Camp))
                                    {
                                        //Es.BuildTC(idx_0).Destroy(Es);
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