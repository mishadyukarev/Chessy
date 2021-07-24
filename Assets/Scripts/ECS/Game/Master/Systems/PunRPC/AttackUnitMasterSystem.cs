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

            var unitTypePrevious = CellUnitWorker.UnitType(FromXy);
            var unitTypeSelected = CellUnitWorker.UnitType(ToXy);

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

                    else if (CellUnitWorker.IsBot(ToXy))
                    {

                    }
                }

                if (CellUnitWorker.HaveOwner(FromXy))
                {
                    InfoUnitsWorker.TakeAmountUnitInGame(CellUnitWorker.UnitType(FromXy), CellUnitWorker.IsMasterClient(FromXy), FromXy);
                    CellUnitWorker.ResetPlayerUnit(FromXy);
                }
                else
                {
                    CellUnitWorker.ResetBotUnit(FromXy);
                }
            }

            if (!CellUnitWorker.HaveAmountHealth(ToXy))
            {
                if (CellUnitWorker.IsUnitType(UnitTypes.King, ToXy))
                    PhotonPunRPC.EndGameToMaster(CellUnitWorker.ActorNumber(FromXy));


                if (CellUnitWorker.HaveOwner(ToXy))
                {
                    CellUnitWorker.ResetPlayerUnit(ToXy);
                }
                else
                {
                    CellUnitWorker.ResetBotUnit(ToXy);
                }




                if (!CellUnitWorker.IsMelee(FromXy))
                {
                    InfoUnitsWorker.TakeAmountUnitInGame(CellUnitWorker.UnitType(FromXy), CellUnitWorker.IsMasterClient(FromXy), FromXy);
                    InfoUnitsWorker.AddAmountUnitInGame(CellUnitWorker.UnitType(FromXy), CellUnitWorker.IsMasterClient(FromXy), ToXy);
                    CellUnitWorker.ShiftPlayerUnitToBaseCell(FromXy, ToXy);
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        PhotonPunRPC.AttackUnitToGeneral(InfoFrom.Sender, _isAttacked);
        PhotonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
    }
}
