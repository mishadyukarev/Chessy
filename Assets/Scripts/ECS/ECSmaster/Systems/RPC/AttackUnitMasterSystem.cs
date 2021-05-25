using Photon.Pun;

internal sealed class AttackUnitMasterSystem : RPCMasterSystemReduction
{
    internal int[] XyPreviousCell => _eMM.RPCMasterEnt_RPCMasterCom.XyPrevious;
    internal int[] XySelectedCell => _eMM.RPCMasterEnt_RPCMasterCom.XySelected;
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    private bool _isAttacked;

    internal AttackUnitMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();

        _eGM.CellEnt_CellUnitCom(XyPreviousCell).GetCellsForAttack(Info.Sender,
            out var availableCellsSimpleAttack, out var availableCellsUniqueAttack);

        var isFindedSimple = _eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(XySelectedCell, availableCellsSimpleAttack);
        var isFindedUnique = _eGM.CellBaseOperEnt_CellBaseOperCom.TryFindCellInList(XySelectedCell, availableCellsUniqueAttack);


        if (isFindedSimple || isFindedUnique)
        {
            _eGM.CellEnt_CellUnitCom(XyPreviousCell).AmountSteps = 0;
            _eGM.CellEnt_CellUnitCom(XyPreviousCell).IsProtected = false;
            _eGM.CellEnt_CellUnitCom(XyPreviousCell).IsRelaxed = false;

            int damageToPrevious = 0;
            int damageToSelelected = 0;


            damageToSelelected += _eGM.CellEnt_CellUnitCom(XyPreviousCell).SimplePowerDamage;
            damageToSelelected -= _eGM.CellEnt_CellUnitCom(XySelectedCell).PowerProtection;


            if (_eGM.CellEnt_CellUnitCom(XyPreviousCell).IsMelee)
            {
                damageToPrevious += _eGM.CellEnt_CellUnitCom(XySelectedCell).SimplePowerDamage;

                if (isFindedUnique)
                {
                    damageToSelelected += _eGM.CellEnt_CellUnitCom(XyPreviousCell).UniquePowerDamage;
                }
            }

            else
            {
                if (isFindedUnique)
                {
                    damageToSelelected += _eGM.CellEnt_CellUnitCom(XyPreviousCell).UniquePowerDamage;
                }
            }

            if (damageToSelelected < 0) damageToSelelected = 0;

            _eGM.CellEnt_CellUnitCom(XyPreviousCell).AmountHealth -= damageToPrevious;
            _eGM.CellEnt_CellUnitCom(XySelectedCell).AmountHealth -= damageToSelelected;


            if (!_eGM.CellEnt_CellUnitCom(XyPreviousCell).HaveHealth)
            {
                if (_eGM.CellEnt_CellUnitCom(XyPreviousCell).UnitType == UnitTypes.King)
                    _photonPunRPC.EndGameToMaster(_eGM.CellEnt_CellUnitCom(XySelectedCell).ActorNumber);

                _eGM.CellEnt_CellUnitCom(XyPreviousCell).ResetUnit();
            }

            if (!_eGM.CellEnt_CellUnitCom(XySelectedCell).HaveHealth)
            {
                if (_eGM.CellEnt_CellUnitCom(XySelectedCell).UnitType == UnitTypes.King)
                    _photonPunRPC.EndGameToMaster(_eGM.CellEnt_CellUnitCom(XyPreviousCell).ActorNumber);

                _eGM.CellEnt_CellUnitCom(XySelectedCell).ResetUnit();
                if (_eGM.CellEnt_CellUnitCom(XyPreviousCell).UnitType != UnitTypes.Rook && _eGM.CellEnt_CellUnitCom(XyPreviousCell).UnitType != UnitTypes.Bishop)
                {
                    _eGM.CellEnt_CellUnitCom(XySelectedCell).SetUnit(XyPreviousCell);
                    _eGM.CellEnt_CellUnitCom(XyPreviousCell).ResetUnit();
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        _photonPunRPC.AttackUnitToGeneral(Info.Sender, _isAttacked, _isAttacked);
        _photonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked);
    }
}
