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
            for (byte idx_0 = 0; idx_0 < e.LengthCells; idx_0++)
            {
                if (e.UnitTC(idx_0).HaveUnit && !e.UnitTC(idx_0).IsAnimal)
                {
                    var canExecute = false;
                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (e.UnitPlayerTC(idx_0).Is(PlayerTypes.First)) canExecute = true;
                    }
                    else canExecute = true;


                    if (canExecute)
                    {
                        if (e.RiverEs(idx_0).RiverTC.HaveRiverNear)
                        {
                            e.UnitWaterC(idx_0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            var needWater = WaterValues.NeedWaterForThirsty(e.UnitTC(idx_0).Unit);

                            if (e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                needWater *= 0.75f;
                            }


                            e.UnitWaterC(idx_0).Water -= needWater;

                            if (e.UnitWaterC(idx_0).Water <= 0)
                            {
                                float percent = HpValues.ThirstyPercent(e.UnitTC(idx_0).Unit);

                                sMM.AttackUnitS.AttackUnit(HpValues.MAX * percent, e.NextPlayer(e.UnitPlayerTC(idx_0)).Player, idx_0, sMM, e);


                                //E.ActionEs.AttackUnit(CellUnitStatHp_Values.MAX_HP * percent, E.NextPlayer(E.UnitPlayerTC(idx_0)).Player, idx_0);
                            }
                        }
                    }
                }
            }
        }
    }
}