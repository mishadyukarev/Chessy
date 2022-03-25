using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    static class ThirstyUnitsUpdateMS
    {
        public static void Run(in GameModeTC gameModeTC, in SystemsModelGame sMM, in EntitiesModelGame e)
        {
            for (byte cell_0 = 0; cell_0 < e.LengthCells; cell_0++)
            {
                if (e.UnitTC(cell_0).HaveUnit && !e.UnitTC(cell_0).IsAnimal)
                {
                    var canExecute = false;
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (e.RiverEs(cell_0).RiverTC.HaveRiverNear)
                        {
                            e.UnitWaterC(cell_0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            var needWater = WaterValues.NeedWaterForThirsty(e.UnitTC(cell_0).Unit);

                            if (e.PlayerInfoE(e.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                needWater *= 0.75f;
                            }


                            e.UnitWaterC(cell_0).Water -= needWater;

                            if (e.UnitWaterC(cell_0).Water <= 0)
                            {
                                float percent = HpValues.ThirstyPercent(e.UnitTC(cell_0).Unit);

                                sMM.UnitSystems.AttackUnitS.Attack(HpValues.MAX * percent, e.NextPlayer(e.UnitPlayerTC(cell_0)).Player, cell_0);


                                //E.ActionEs.AttackUnit(CellUnitStatHp_Values.MAX_HP * percent, E.NextPlayer(E.UnitPlayerTC(cell_0)).Player, cell_0);
                            }
                        }
                    }
                }
            }
        }
    }
}