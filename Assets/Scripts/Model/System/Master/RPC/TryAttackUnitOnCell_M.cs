using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        internal void TryAttackUnitOnCellM(in byte idxCellFrom, in byte idxCellTo, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _e.WhoseMovePlayerT : sender.GetPlayer();

            var canAttack = _e.AttackUniqueCellsC(idxCellFrom).Contains(idxCellTo)
                || _e.AttackSimpleCellsC(idxCellFrom).Contains(idxCellTo);

            if (canAttack && _e.UnitPlayerT(idxCellFrom).Is(whoseMove))
            {
                AttackUnitFromTo(idxCellFrom, idxCellTo);
            }
        }
    }
}