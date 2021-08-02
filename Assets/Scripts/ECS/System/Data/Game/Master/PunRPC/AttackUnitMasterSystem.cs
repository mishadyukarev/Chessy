using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
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

        CellUnitsDataSystem.GetCellsForAttack(RpcMasterDataContainer.InfoFrom.Sender,
            out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, FromXy);

        var isFindedSimple = availableCellsSimpleAttack.TryFindCell(ToXy);
        var isFindedUnique = availableCellsUniqueAttack.TryFindCell(ToXy);


        if (isFindedSimple || isFindedUnique)
        {
            CellUnitsDataSystem.ResetAmountSteps(FromXy);
            CellUnitsDataSystem.ResetConditionType(FromXy);

            int damageToPrevious = 0;
            int damageToSelelected = 0;

            var unitTypePrevious = CellUnitsDataSystem.UnitType(FromXy);
            var unitTypeSelected = CellUnitsDataSystem.UnitType(ToXy);

            damageToSelelected += CellUnitsDataSystem.SimplePowerDamage(unitTypePrevious);
            damageToSelelected -= CellUnitsDataSystem.PowerProtection(ToXy);


            if (CellUnitsDataSystem.IsMelee(FromXy))
            {
                PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

                damageToPrevious += CellUnitsDataSystem.SimplePowerDamage(unitTypeSelected);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitsDataSystem.UniquePowerDamage(unitTypePrevious);
                }
            }

            else
            {
                PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

                if (isFindedUnique)
                {
                    damageToSelelected += CellUnitsDataSystem.UniquePowerDamage(unitTypePrevious);
                }
            }

            if (damageToSelelected < 0) damageToSelelected = 0;

            CellUnitsDataSystem.TakeAmountHealth(FromXy, damageToPrevious);
            CellUnitsDataSystem.TakeAmountHealth(ToXy, damageToSelelected);


            if (!CellUnitsDataSystem.HaveAmountHealth(FromXy))
            {
                if (CellUnitsDataSystem.IsUnitType(UnitTypes.King, FromXy))
                {
                    if (CellUnitsDataSystem.HaveOwner(ToXy))
                    {
                        PhotonPunRPC.EndGameToMaster(CellUnitsDataSystem.ActorNumber(ToXy));
                    }

                    else if (CellUnitsDataSystem.IsBot(ToXy))
                    {

                    }
                }

                var isMasterFromUnit = CellUnitsDataSystem.IsMasterClient(FromXy);

                InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(FromXy), isMasterFromUnit, FromXy);
                InfoUnitsDataContainer.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(FromXy), CellUnitsDataSystem.UnitType(FromXy), isMasterFromUnit, FromXy);
                CellUnitsDataSystem.ResetUnit(FromXy);

                if (CellUnitsDataSystem.HaveOwner(FromXy))
                {
                    InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), FromXy);
                }
            }

            if (!CellUnitsDataSystem.HaveAmountHealth(ToXy))
            {
                if (CellUnitsDataSystem.IsUnitType(UnitTypes.King, ToXy))
                    PhotonPunRPC.EndGameToMaster(CellUnitsDataSystem.ActorNumber(FromXy));

                var isMasterToUnit = CellUnitsDataSystem.IsMasterClient(ToXy);

                InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(ToXy), isMasterToUnit, ToXy);
                InfoUnitsDataContainer.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(ToXy), CellUnitsDataSystem.UnitType(ToXy), isMasterToUnit, ToXy);
                CellUnitsDataSystem.ResetUnit(ToXy);

                if (CellUnitsDataSystem.IsMelee(FromXy))
                {
                    InfoUnitsDataContainer.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), FromXy);
                    InfoUnitsDataContainer.AddAmountUnitInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), ToXy);
                    CellUnitsDataSystem.ShiftPlayerUnitToBaseCell(FromXy, ToXy);
                }
            }

            _isAttacked = true;
        }
        else _isAttacked = false;


        PhotonPunRPC.AttackUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender, _isAttacked);
        PhotonPunRPC.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
    }
}
