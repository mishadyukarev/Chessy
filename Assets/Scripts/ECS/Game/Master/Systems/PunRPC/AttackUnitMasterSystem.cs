using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using static Assets.Scripts.Workers.CellBaseOperations;

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
            CellUnitWorker.ResetAmountSteps(FromXy);
            CellUnitWorker.ResetProtectedRelaxType(FromXy);

            int damageToPrevious = 0;
            int damageToSelelected = 0;

            var unitTypePrevious = _eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType;
            var unitTypeSelected = _eGM.CellUnitEnt_UnitTypeCom(ToXy).UnitType;

            damageToSelelected += CellUnitWorker.SimplePowerDamage(unitTypePrevious);
            damageToSelelected -= CellUnitWorker.PowerProtection(ToXy);


            if (CellUnitWorker.IsMelee(FromXy))
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

            CellUnitWorker.TakeAmountHealth(FromXy, damageToPrevious);
            CellUnitWorker.TakeAmountHealth(ToXy, damageToSelelected);


            if (!CellUnitWorker.HaveAmountHealth(FromXy))
            {
                if (CellUnitWorker.IsUnitType(UnitTypes.King, FromXy))
                {
                    if (CellUnitWorker.HaveOwner(ToXy))
                    {
                        PhotonPunRPC.EndGameToMaster(CellUnitWorker.ActorNumber(ToXy));
                    }

                    else if (_eGM.CellUnitEnt_CellOwnerBotCom(ToXy).IsBot)
                    {

                    }
                }

                if (CellUnitWorker.HaveOwner(FromXy))
                {
                    CellUnitWorker.ResetPlayerUnit(FromXy);

                }
                else
                {
                    CellUnitWorker.ResetBotUnit(FromXy);
                }
            }

            if (!CellUnitWorker.HaveAmountHealth(ToXy))
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(ToXy).UnitType == UnitTypes.King)
                    PhotonPunRPC.EndGameToMaster(CellUnitWorker.ActorNumber(FromXy));


                if (CellUnitWorker.HaveOwner(ToXy))
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
