using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TryAttackUnitOnCell(in byte idxCellFrom, in byte idxCellTo, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _eMG.WhoseMovePlayerT : sender.GetPlayer();

            var canAttack = _eMG.AttackUniqueCellsC(idxCellFrom).Contains(idxCellTo)
                || _eMG.AttackSimpleCellsC(idxCellFrom).Contains(idxCellTo);

            if (canAttack && _eMG.UnitPlayerTC(idxCellFrom).Is(whoseMove))
            {
                AttackUnitFromTo(idxCellFrom, idxCellTo);
            }
        }
    }
}