using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Photon.Realtime;

namespace Chessy.Game
{
    public sealed class ShiftUnitS_M : SystemModelGameAbs
    {
        readonly ShiftUnitS _shiftUnitS;

        public ShiftUnitS_M(in ShiftUnitS shiftUnitS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _shiftUnitS = shiftUnitS;
        }

        public void Shift(in byte idx_from, in byte idx_to, in Player sender)
        {
            if (eMGame.UnitEs(idx_from).ForShift.Contains(idx_to) && eMGame.UnitPlayerTC(idx_from).Is(eMGame.WhoseMove.Player))
            {
                eMGame.UnitStepC(idx_from).Steps -= eMGame.UnitEs(idx_from).NeedSteps(idx_to).Steps;

                _shiftUnitS.Shift(idx_from, idx_to);

                eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}