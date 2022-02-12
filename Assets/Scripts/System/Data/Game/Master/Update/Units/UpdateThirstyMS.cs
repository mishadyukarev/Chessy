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
                var unit_0 = UnitEs(idx_0).UnitE.UnitTC;
                var ownUnit_0 = Es.UnitE(idx_0).OwnerC;

                if (Es.UnitE(idx_0).HaveUnit && !unit_0.IsAnimal)
                {
                    var canExecute = false;
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First)) canExecute = true;
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
                            
                            Es.UnitE(idx_0).Water -= CellUnitStatWaterValues.NeedWaterThirsty(Es.UnitE(idx_0).Unit);

                            if (!Es.UnitE(idx_0).HaveWater)
                            {
                                Es.UnitE(idx_0).Thirsty(Es);
                            }
                        }
                    }
                }
            }
        }
    }
}