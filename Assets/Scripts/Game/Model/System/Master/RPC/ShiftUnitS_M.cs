using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.System.Model;
using Photon.Realtime;

namespace Chessy.Game
{
    public sealed class ShiftUnitS_M : SystemModelGameAbs
    {
        readonly UnitEs _unitEs;
        readonly ShiftUnitS _shiftUnitS;

        public ShiftUnitS_M(in UnitEs unitEs, in ShiftUnitS shiftUnitS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _unitEs = unitEs;
            _shiftUnitS = shiftUnitS;
        }

        public void Shift(in byte idx_to, in Player sender)
        {
            if (_unitEs.ForShift.Contains(idx_to) && _unitEs.MainE.PlayerTC.Is(eMGame.WhoseMove.Player))
            {
                _unitEs.StatsE.StepC.Steps -= _unitEs.NeedSteps(idx_to).Steps;


                _shiftUnitS.Shift(idx_to);

                eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}