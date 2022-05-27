using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Photon.Realtime;

namespace Chessy.Game
{
    sealed class TryShiftUnitS_M : SystemModel
    {
        internal TryShiftUnitS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void TryShift(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (eMG.CellsForShift(cell_from).Contains(cell_to) && eMG.UnitPlayerTC(cell_from).Is(eMG.WhoseMovePlayerT))
            {
                eMG.StepUnitC(cell_from).Steps -= eMG.UnitNeedStepsForShiftC(cell_from).NeedSteps(cell_to);


                sMG.UnitSs.ShiftOnOtherCellS.Shift(cell_from, cell_to);

                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}