using Chessy.Common;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    static class ThirstyUnitsUpdateMS
    {
        public static void Run(in SystemsModel sMM, in EntitiesModel E)
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
                            E.UnitWaterC(idx_0).Water = WaterValues.MAX;
                        }
                        else
                        {
                            var needWater = WaterValues.NeedWaterForThirsty(E.UnitTC(idx_0).Unit);

                            if (E.PlayerInfoE(E.UnitPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Snowy))
                            {
                                needWater *= 0.75f;
                            }


                            E.UnitWaterC(idx_0).Water -= needWater;

                            if (E.UnitWaterC(idx_0).Water <= 0)
                            {
                                float percent = HpValues.ThirstyPercent(E.UnitTC(idx_0).Unit);

                                sMM.AttackUnitS.AttackUnit(HpValues.MAX * percent, E.NextPlayer(E.UnitPlayerTC(idx_0)).Player, idx_0, sMM, E);


                                //E.ActionEs.AttackUnit(CellUnitStatHp_Values.MAX_HP * percent, E.NextPlayer(E.UnitPlayerTC(idx_0)).Player, idx_0);
                            }
                        }
                    }
                }
            }
        }
    }
}