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
            foreach (byte idx_0 in CellWorker.Idxs)
            {
                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;

                if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)) && !unit_0.IsAnimal)
                {
                    var canExecute = false;
                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (RiverEs(idx_0).River.HaveRiver)
                        {
                            UnitStatEs(idx_0).WaterE.SetMax(UnitEs(idx_0).MainE, Es.UnitStatUpgradesEs);
                        }
                        else
                        {
                            UnitStatEs(idx_0).WaterE.Thirsty(UnitEs(idx_0).MainE.UnitTC.Unit);

                            if (!UnitStatEs(idx_0).WaterE.HaveWater)
                            {
                                UnitStatEs(idx_0).Hp.Thirsty(Es);
                            }
                        }
                    }
                }
            }
        }
    }
}