using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Photon.Realtime;

namespace Chessy.Game
{
    sealed class ShiftUnitS_M : SystemModelGameAbs
    {
        internal ShiftUnitS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Shift(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (e.UnitEs(cell_from).ForShift.Contains(cell_to) && e.UnitPlayerTC(cell_from).Is(e.WhoseMove.Player))
            {
                e.UnitStepC(cell_from).Steps -= e.UnitEs(cell_from).NeedSteps(cell_to).Steps;


                s.ShiftUnitS.Shift(cell_from, cell_to);

                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}