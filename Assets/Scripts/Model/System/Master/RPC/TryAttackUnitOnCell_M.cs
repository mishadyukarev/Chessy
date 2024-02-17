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

            if (canAttack && unitCs[idxCellFrom].PlayerT == whoDoing && !UnitAttackC(idxCellFrom).HaveCoolDownForAttackAnyUnit)
            {
                AttackUnitFromTo(idxCellFrom, idxCellTo);
            }
        }
    }
}