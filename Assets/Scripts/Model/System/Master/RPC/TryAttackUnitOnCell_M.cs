using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryAttackUnitOnCellM(in byte idxCellFrom, in byte idxCellTo, in Player sender)
        {
            var whoDoing = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

            var canAttack = _whereUniqueAttackCs[idxCellFrom].Can(idxCellTo)
                || _whereSimpleAttackCs[idxCellFrom].Can(idxCellTo);

            if (canAttack && _unitCs[idxCellFrom].PlayerT == whoDoing && !_unitCs[idxCellFrom].HaveCoolDownForAttackAnyUnit)
            {
                AttackUnitFromTo(idxCellFrom, idxCellTo);
            }
        }
    }
}