using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
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

        CellUnitsDataWorker.GetCellsForAttack(InfoFrom.Sender,
            out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, FromXy);

        var isFindedSimple = availableCellsSimpleAttack.TryFindCell(ToXy);
        var isFindedUnique = availableCellsUniqueAttack.TryFindCell(ToXy);


        if (isFindedSimple || isFindedUnique)
        {
            CellUnitsDataWorker.ResetAmountSteps(FromXy);
            CellUnitsDataWorker.ResetProtectedRelaxType(FromXy);

            int damageToPrevious = 0;
            int damageToSelelected = 0;

            var unitTypePrevious = CellUnitsDataWorker.UnitType(FromXy);
            var unitTypeSelected = CellUnitsDataWorker.UnitType(ToXy);

            damageToSelelected += CellUnitsDataWorker.SimplePowerDamage(unitTypePrevious);
            damageToSelelected -= CellUnitsDataWorker.PowerProtection(ToXy);


            if (CellUnitsDataWorker.IsMelee(FromXy))
            {
                PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                damageToPrevious += CellUnitsDataWorker.SimplePowerDamage(unitTypeSelected);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitsDataWorker.UniquePowerDamage(unitTypePrevious);
                }
            }

            else
            {
                PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitsDataWorker.UniquePowerDamage(unitTypePrevious);
                }
            }

            if (damageToSelelected < 0) damageToSelelected = 0;

            CellUnitsDataWorker.TakeAmountHealth(FromXy, damageToPrevious);
            CellUnitsDataWorker.TakeAmountHealth(ToXy, damageToSelelected);


            if (!CellUnitsDataWorker.HaveAmountHealth(FromXy))
            {
                if (CellUnitsDataWorker.IsUnitType(UnitTypes.King, FromXy))
                {
                    if (CellUnitsDataWorker.HaveOwner(ToXy))
                    {
                        PhotonPunRPC.EndGameToMaster(CellUnitsDataWorker.ActorNumber(ToXy));
                    }

                    else if (CellUnitsDataWorker.IsBot(ToXy))
                    {

                    }
                }

                CellUnitsDataWorker.ResetUnit(FromXy);

                if (CellUnitsDataWorker.HaveOwner(FromXy))
                {
                    InfoUnitsWorker.TakeAmountUnitInGame(CellUnitsDataWorker.UnitType(FromXy), CellUnitsDataWorker.IsMasterClient(FromXy), FromXy);
                }
            }

            if (!CellUnitsDataWorker.HaveAmountHealth(ToXy))
            {
                if (CellUnitsDataWorker.IsUnitType(UnitTypes.King, ToXy))
                    PhotonPunRPC.EndGameToMaster(CellUnitsDataWorker.ActorNumber(FromXy));


                CellUnitsDataWorker.ResetUnit(ToXy);

                if (!CellUnitsDataWorker.IsMelee(FromXy))
                {
                    InfoUnitsWorker.TakeAmountUnitInGame(CellUnitsDataWorker.UnitType(FromXy), CellUnitsDataWorker.IsMasterClient(FromXy), FromXy);
                    InfoUnitsWorker.AddAmountUnitInGame(CellUnitsDataWorker.UnitType(FromXy), CellUnitsDataWorker.IsMasterClient(FromXy), ToXy);
                    CellUnitsDataWorker.ShiftPlayerUnitToBaseCell(FromXy, ToXy);
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        PhotonPunRPC.AttackUnitToGeneral(InfoFrom.Sender, _isAttacked);
        PhotonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
    }
}
