﻿using Game.Common;

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
                if (Es.UnitTC(idx_0).HaveUnit && !Es.UnitEs(idx_0).IsAnimal)
                {
                    var canExecute = false;
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (Es.UnitPlayerTC(idx_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (Es.RiverEs(idx_0).RiverTC.HaveRiverNear)
                        {
                            //Es.UnitE(idx_0).WaterC.Set(CellUnitStatWaterValues.WATER_MAX_STANDART);
                        }
                        else
                        {
                            
                            Es.UnitWaterC(idx_0).Water -= CellUnitStatWater_Values.NeedWaterForThirsty(Es.UnitTC(idx_0).Unit);

                            if (Es.UnitWaterC(idx_0).Water <= 0)
                            {
                                float percent = CellUnitStatHp_Values.ThirstyPercent(Es.UnitTC(idx_0).Unit);

                                //Es.UnitE(idx_0).Take(Es, CellUnitStatWaterValues.WATER_MAX_STANDART * percent);

                                if (!Es.UnitTC(idx_0).HaveUnit)
                                {
                                    if (Es.BuildTC(idx_0).Is(BuildingTypes.Camp))
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