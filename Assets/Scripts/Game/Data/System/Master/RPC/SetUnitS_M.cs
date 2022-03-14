using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public struct SetUnitS_M
    {
        public SetUnitS_M(in byte idx_0, in UnitTypes unitT, in Player sender, in EntitiesModel e)
        {
            var whoseMove = e.WhoseMove.Player;

            if (e.CellEs(idx_0).CellE.IsStartedCell(whoseMove) && !e.UnitTC(idx_0).HaveUnit)
            {
                SetUnitS.Set(unitT, whoseMove, idx_0, e);


                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}