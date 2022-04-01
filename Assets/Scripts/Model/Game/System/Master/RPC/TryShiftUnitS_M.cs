using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Photon.Realtime;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game
{
    sealed class TryShiftUnitS_M : SystemModelGameAbs
    {
        internal TryShiftUnitS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void TryShift(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (eMG.UnitEs(cell_from).ForShift.Contains(cell_to) && eMG.UnitPlayerTC(cell_from).Is(eMG.WhoseMove.PlayerT))
            {
                eMG.UnitStepC(cell_from).Steps -= eMG.UnitEs(cell_from).NeedSteps(cell_to).Steps;


                sMG.UnitSs.ShiftUnitS.Shift(cell_from, cell_to);

                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}