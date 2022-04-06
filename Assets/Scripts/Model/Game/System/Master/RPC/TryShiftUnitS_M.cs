using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Photon.Realtime;

namespace Chessy.Game
{
    sealed class TryShiftUnitS_M : SystemModel
    {
        internal TryShiftUnitS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void TryShift(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (eMG.CellsForShift(cell_from).Contains(cell_to) && eMG.UnitPlayerTC(cell_from).Is(eMG.WhoseMovePlayerTC.PlayerT))
            {
                eMG.StepUnitC(cell_from).Steps -= eMG.UnitNeedStepsForShiftC(cell_from).NeedSteps(cell_to);


                sMG.UnitSs.ShiftUnitS.Shift(cell_from, cell_to);

                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}