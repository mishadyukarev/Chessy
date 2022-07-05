using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryAttackUnitOnCellM(in byte idxCellFrom, in byte idxCellTo, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? _e.WhoseMovePlayerT : sender.GetPlayer();

            var canAttack = _e.WhereUnitCanAttackUniqueAttackToEnemyC(idxCellFrom).Can(idxCellTo)
                || _e.WhereUnitCanAttackSimpleAttackToEnemyC(idxCellFrom).Can(idxCellTo);

            if (canAttack && _e.UnitPlayerT(idxCellFrom).Is(whoseMove))
            {
                AttackUnitFromTo(idxCellFrom, idxCellTo);
            }
        }
    }
}