using Chessy.Game.Model.Entity;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System.Master
{
    sealed class TryAttackUnitOnCell_M : SystemModel
    {
        internal TryAttackUnitOnCell_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryAttack(in byte idx_from, in byte idx_to, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            var canAttack = eMG.AttackUniqueCellsC(idx_from).Contains(idx_to)
                || eMG.AttackSimpleCellsC(idx_from).Contains(idx_to);

            if (canAttack && eMG.UnitPlayerTC(idx_from).Is(whoseMove))
            {
                sMG.UnitSs.AttackUnitFromToS_M.Attack(idx_from, idx_to);
            }
        }
    }
}