using Chessy.Game.System.Model;
using Photon.Realtime;

namespace Chessy.Game
{
    public struct ShiftUnitS_M
    {
        public ShiftUnitS_M(in byte idx_from, in byte idx_to, in Player sender, in EntitiesModel e)
        {
            if (e.UnitEs(idx_from).ForShift.Contains(idx_to) && e.UnitPlayerTC(idx_from).Is(e.WhoseMove.Player))
            {
                e.UnitStepC(idx_from).Steps -= e.UnitEs(idx_from).NeedSteps(idx_to).Steps;

                new UnitShiftS(idx_from, idx_to, e);

                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}