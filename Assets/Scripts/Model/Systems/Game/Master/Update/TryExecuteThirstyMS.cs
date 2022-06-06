using Chessy.Common;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class TryExecuteThirstyMS : SystemModel
    {
        internal TryExecuteThirstyMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryExecute()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.UnitTC(cellIdx0).HaveUnit && !eMG.UnitTC(cellIdx0).IsAnimal)
                {
                    var canExecute = false;
                    if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (eMG.UnitPlayerTC(cellIdx0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (eMG.RiverTC(cellIdx0).HaveRiverNear)
                        {
                            eMG.WaterUnitC(cellIdx0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            eMG.WaterUnitC(cellIdx0).Water -= WaterValues.NeedWaterForThirsty(eMG.UnitT(cellIdx0));

                            if (eMG.WaterUnitC(cellIdx0).Water <= 0)
                            {
                                var percent = HpValues.ThirstyPercent(eMG.UnitTC(cellIdx0).UnitT);

                                sMG.UnitSs.AttackUnitS.Attack(HpValues.MAX * percent, eMG.UnitPlayerTC(cellIdx0).PlayerT.NextPlayer(), cellIdx0);


                                //E.ActionEs.AttackUnit(CellUnitStatHp_Values.MAX_HP * percent, E.NextPlayer(E.UnitPlayerTC(cell_0)).Player, cell_0);
                            }
                        }
                    }
                }

            }
        }
    }
}