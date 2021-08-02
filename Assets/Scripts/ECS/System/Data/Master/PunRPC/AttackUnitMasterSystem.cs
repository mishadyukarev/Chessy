using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Photon.Pun;
using static Assets.Scripts.Workers.CellBaseOperations;

internal sealed class AttackUnitMasterSystem : SystemMasterReduction
{
    private bool _isAttacked;

    internal int[] FromXy => _eMM.AttackEnt_FromToXyCom.FromXy;
    internal int[] ToXy => _eMM.AttackEnt_FromToXyCom.ToXy;


    public override void Run()
    {
        base.Run();

        CellUnitsDataContainer.GetCellsForAttack(RpcMasterDataContainer.InfoFrom.Sender,
            out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, FromXy);

        var isFindedSimple = availableCellsSimpleAttack.TryFindCell(ToXy);
        var isFindedUnique = availableCellsUniqueAttack.TryFindCell(ToXy);


        if (isFindedSimple || isFindedUnique)
        {
            CellUnitsDataContainer.ResetAmountSteps(FromXy);
            CellUnitsDataContainer.ResetConditionType(FromXy);

            int damageToPrevious = 0;
            int damageToSelelected = 0;

            var unitTypePrevious = CellUnitsDataContainer.UnitType(FromXy);
            var unitTypeSelected = CellUnitsDataContainer.UnitType(ToXy);

            damageToSelelected += CellUnitsDataContainer.SimplePowerDamage(unitTypePrevious);
            damageToSelelected -= CellUnitsDataContainer.PowerProtection(ToXy);


            if (CellUnitsDataContainer.IsMelee(FromXy))
            {
                PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                damageToPrevious += CellUnitsDataContainer.SimplePowerDamage(unitTypeSelected);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitsDataContainer.UniquePowerDamage(unitTypePrevious);
                }
            }

            else
            {
                PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitsDataContainer.UniquePowerDamage(unitTypePrevious);
                }
            }

            if (damageToSelelected < 0) damageToSelelected = 0;

            CellUnitsDataContainer.TakeAmountHealth(FromXy, damageToPrevious);
            CellUnitsDataContainer.TakeAmountHealth(ToXy, damageToSelelected);


            if (!CellUnitsDataContainer.HaveAmountHealth(FromXy))
            {
                if (CellUnitsDataContainer.IsUnitType(UnitTypes.King, FromXy))
                {
                    if (CellUnitsDataContainer.HaveOwner(ToXy))
                    {
                        PhotonPunRPC.EndGameToMaster(CellUnitsDataContainer.ActorNumber(ToXy));
                    }

                    else if (CellUnitsDataContainer.IsBot(ToXy))
                    {

                    }
                }

                var isMasterFromUnit = CellUnitsDataContainer.IsMasterClient(FromXy);

                InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataContainer.UnitType(FromXy), isMasterFromUnit, FromXy);
                InfoUnitsDataContainer.RemoveUnitInCondition(CellUnitsDataContainer.ConditionType(FromXy), CellUnitsDataContainer.UnitType(FromXy), isMasterFromUnit, FromXy);
                CellUnitsDataContainer.ResetUnit(FromXy);

                if (CellUnitsDataContainer.HaveOwner(FromXy))
                {
                    InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataContainer.UnitType(FromXy), CellUnitsDataContainer.IsMasterClient(FromXy), FromXy);
                }
            }

            if (!CellUnitsDataContainer.HaveAmountHealth(ToXy))
            {
                if (CellUnitsDataContainer.IsUnitType(UnitTypes.King, ToXy))
                    PhotonPunRPC.EndGameToMaster(CellUnitsDataContainer.ActorNumber(FromXy));

                var isMasterToUnit = CellUnitsDataContainer.IsMasterClient(ToXy);

                InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataContainer.UnitType(ToXy), isMasterToUnit, ToXy);
                InfoUnitsDataContainer.RemoveUnitInCondition(CellUnitsDataContainer.ConditionType(ToXy), CellUnitsDataContainer.UnitType(ToXy), isMasterToUnit, ToXy);
                CellUnitsDataContainer.ResetUnit(ToXy);

                if (CellUnitsDataContainer.IsMelee(FromXy))
                {
                    InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataContainer.UnitType(FromXy), CellUnitsDataContainer.IsMasterClient(FromXy), FromXy);
                    InfoUnitsDataContainer.AddAmountUnitInGame(CellUnitsDataContainer.UnitType(FromXy), CellUnitsDataContainer.IsMasterClient(FromXy), ToXy);
                    CellUnitsDataContainer.ShiftPlayerUnitToBaseCell(FromXy, ToXy);
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        PhotonPunRPC.AttackUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender, _isAttacked);
        PhotonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
    }
}
