using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using static Assets.Scripts.Static.CellBaseOperations;

internal sealed class AttackUnitMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

    internal int[] XyPreviousCell => _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.RPCMasterEnt_RPCMasterCom.XySelected;
    private bool _isAttacked;


    public override void Run()
    {
        base.Run();

        CellUnitWorker.GetCellsForAttack(InfoFrom.Sender,
            out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, XyPreviousCell);

        var isFindedSimple = TryFindCellInList(XySelectedCell, availableCellsSimpleAttack);
        var isFindedUnique = TryFindCellInList(XySelectedCell, availableCellsUniqueAttack);


        if (isFindedSimple || isFindedUnique)
        {
            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).ResetAmountSteps();
            _eGM.CellUnitEnt_ProtectRelaxCom(XyPreviousCell).ResetProtectedRelaxedType();

            int damageToPrevious = 0;
            int damageToSelelected = 0;

            var unitTypePrevious = _eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType;
            var unitTypeSelected = _eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType;

            damageToSelelected += CellUnitWorker.SimplePowerDamage(unitTypePrevious);
            damageToSelelected -= CellUnitWorker.PowerProtection(XySelectedCell);


            if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).IsMelee)
            {
                _photonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                damageToPrevious += CellUnitWorker.SimplePowerDamage(unitTypeSelected);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitWorker.UniquePowerDamage(unitTypePrevious);
                }
            }

            else
            {
                _photonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitWorker.UniquePowerDamage(unitTypePrevious);
                }
            }

            if (damageToSelelected < 0) damageToSelelected = 0;

            _eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).TakeAmountHealth(damageToPrevious);
            _eGM.CellUnitEnt_CellUnitCom(XySelectedCell).TakeAmountHealth(damageToSelelected);


            if (!_eGM.CellUnitEnt_CellUnitCom(XyPreviousCell).HaveHealth)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType == UnitTypes.King)
                {
                    if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).HaveOwner)
                    {
                        _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).ActorNumber);
                    }

                    else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).HaveBot)
                    {

                    }
                }

                if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).HaveOwner)
                {
                    CellUnitWorker.ResetPlayerUnit(true, XyPreviousCell);

                }
                else
                {
                    CellUnitWorker.ResetBotUnit(XyPreviousCell);
                }
            }

            if (!_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).HaveHealth)
            {
                if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType == UnitTypes.King)
                    _photonPunRPC.EndGameToMaster(_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).ActorNumber);


                if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).HaveOwner)
                {
                    CellUnitWorker.ResetPlayerUnit(true, XySelectedCell);
                }
                else
                {
                    CellUnitWorker.ResetBotUnit(XySelectedCell);
                }




                if (_eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.Rook
                    && _eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.RookCrossbow
                    && _eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.Bishop
                    && _eGM.CellUnitEnt_UnitTypeCom(XyPreviousCell).UnitType != UnitTypes.BishopCrossbow)
                {
                    CellUnitWorker.ShiftUnit(XyPreviousCell, XySelectedCell);

                    if (_eGM.CellUnitEnt_CellOwnerCom(XyPreviousCell).HaveOwner)
                    {
                        CellUnitWorker.ResetPlayerUnit(false, XyPreviousCell);
                    }
                    else
                    {
                        CellUnitWorker.ResetBotUnit(XyPreviousCell);
                    }
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        _photonPunRPC.AttackUnitToGeneral(InfoFrom.Sender, _isAttacked);
        _photonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, XyPreviousCell, XySelectedCell);
    }
}
