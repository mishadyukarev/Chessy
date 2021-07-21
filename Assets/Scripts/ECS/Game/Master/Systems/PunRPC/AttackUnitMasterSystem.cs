using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class AttackUnitMasterSystem : RPCMasterSystemReduction
{
    private bool _isAttacked;

    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    internal int[] FromXy => _eMM.AttackEnt_FromToXyCom.FromXy;
    internal int[] ToXy => _eMM.AttackEnt_FromToXyCom.ToXy;


    public override void Run()
    {
        base.Run();

        CellUnitWorker.GetCellsForAttack(InfoFrom.Sender,
            out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, FromXy);

        var isFindedSimple = availableCellsSimpleAttack.TryFindCell(ToXy);
        var isFindedUnique = availableCellsUniqueAttack.TryFindCell(ToXy);


        if (isFindedSimple || isFindedUnique)
        {
            _eGM.CellUnitEnt_CellUnitCom(FromXy).ResetAmountSteps();
            _eGM.CellUnitEnt_ProtectRelaxCom(FromXy).Reset();

            int damageToPrevious = 0;
            int damageToSelelected = 0;

            var unitTypePrevious = _eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType;
            var unitTypeSelected = _eGM.CellUnitEnt_UnitTypeCom(ToXy).UnitType;

            damageToSelelected += CellUnitWorker.SimplePowerDamage(unitTypePrevious);
            damageToSelelected -= CellUnitWorker.PowerProtection(ToXy);


            if (_eGM.CellUnitEnt_UnitTypeCom(FromXy).IsMelee)
            {
                PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                damageToPrevious += CellUnitWorker.SimplePowerDamage(unitTypeSelected);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitWorker.UniquePowerDamage(unitTypePrevious);
                }
            }

            else
            {
                PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitWorker.UniquePowerDamage(unitTypePrevious);
                }
            }

            if (damageToSelelected < 0) damageToSelelected = 0;

            _eGM.CellUnitEnt_CellUnitCom(FromXy).TakeAmountHealth(damageToPrevious);
            _eGM.CellUnitEnt_CellUnitCom(ToXy).TakeAmountHealth(damageToSelelected);


            if (!_eGM.CellUnitEnt_CellUnitCom(FromXy).HaveAmountHealth)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType == UnitTypes.King)
                {
                    if (_eGM.CellUnitEnt_CellOwnerCom(ToXy).HaveOwner)
                    {
                        PhotonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(ToXy).ActorNumber);
                    }

                    else if (_eGM.CellUnitEnt_CellOwnerBotCom(ToXy).IsBot)
                    {

                    }
                }

                if (_eGM.CellUnitEnt_CellOwnerCom(FromXy).HaveOwner)
                {
                    CellUnitWorker.ResetPlayerUnit(FromXy);

                }
                else
                {
                    CellUnitWorker.ResetBotUnit(FromXy);
                }
            }

            if (!_eGM.CellUnitEnt_CellUnitCom(ToXy).HaveAmountHealth)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(ToXy).UnitType == UnitTypes.King)
                    PhotonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(FromXy).ActorNumber);


                if (_eGM.CellUnitEnt_CellOwnerCom(ToXy).HaveOwner)
                {
                    CellUnitWorker.ResetPlayerUnit(ToXy);
                }
                else
                {
                    CellUnitWorker.ResetBotUnit(ToXy);
                }




                if (_eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType != UnitTypes.Rook
                    && _eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType != UnitTypes.RookCrossbow
                    && _eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType != UnitTypes.Bishop
                    && _eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType != UnitTypes.BishopCrossbow)
                {
                    CellUnitWorker.ShiftPlayerUnit(FromXy, ToXy);

                    //if (_eGM.CellUnitEnt_CellOwnerCom(FromXy).HaveOwner)
                    //{
                    //    CellUnitWorker.ResetPlayerUnit(false, FromXy);
                    //}
                    //else
                    //{
                    //    CellUnitWorker.ResetBotUnit(FromXy);
                    //}
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        PhotonPunRPC.AttackUnitToGeneral(InfoFrom.Sender, _isAttacked);
        PhotonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
    }
}
