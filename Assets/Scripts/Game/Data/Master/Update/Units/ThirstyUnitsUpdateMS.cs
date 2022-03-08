using Chessy.Common;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class ThirstyUnitsUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal ThirstyUnitsUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit && !E.IsAnimal(E.UnitTC(idx_0).Unit))
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
                            E.UnitWaterC(idx_0).Water = E.UnitInfo(E.UnitPlayerTC(idx_0), E.UnitLevelTC(idx_0)).WaterKingPawnMax;
                        }
                        else
                        {
                            
                            E.UnitWaterC(idx_0).Water -= WaterValues.NeedWaterForThirsty(E.UnitTC(idx_0).Unit);

                            if (E.UnitWaterC(idx_0).Water <= 0)
                            {
                                float percent = Hp_VALUES.ThirstyPercent(E.UnitTC(idx_0).Unit);

                                E.AttackUnitE(idx_0).Set(Hp_VALUES.HP * percent, E.NextPlayer(E.UnitPlayerTC(idx_0)).Player);


                                //E.ActionEs.AttackUnit(CellUnitStatHp_Values.MAX_HP * percent, E.NextPlayer(E.UnitPlayerTC(idx_0)).Player, idx_0);
                            }
                        }
                    }
                }
            }
        }
    }
}