using Chessy.Common;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class TryGiveHpToBotUnitsMS : SystemModel
    {
        internal TryGiveHpToBotUnitsMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryGive()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.UnitTC(cellIdx0).HaveUnit)
                {
                    if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (eMG.UnitPlayerTC(cellIdx0).Is(PlayerTypes.Second))
                        {
                            eMG.HpUnitC(cellIdx0).Health = HpValues.MAX;
                        }
                    }
                }
            }
        }
    }
}