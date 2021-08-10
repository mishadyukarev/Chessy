using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

internal sealed class AttackUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoMasterFilter;
    private EcsFilter<ForAttackMasCom> _forAttackFilter;
    private EcsFilter<IdxUnitsComponent> _idxUnitsFilter;

    //private bool _isAttacked;

    //private Player Sender => _infoMasterFilter.Get1(0).FromInfo.Sender;

    //internal int[] FromXy => _attackFilter.Get2(0).FromXy;
    //internal int[] ToXy => _attackFilter.Get2(0).ToXy;

    public void Run()
    {
        ref var forAttackMasCom = ref _forAttackFilter.Get1(0);
        ref var idxUnitsCom = ref _idxUnitsFilter.Get1(0);


        //CellUnitsDataSystem.GetCellsForAttack(Sender, out var availableCellsSimpleAttack, out var availableCellsUniqueAttack, FromXy);

        //var isFindedSimple = availableCellsSimpleAttack.TryFindCell(ToXy);
        //var isFindedUnique = availableCellsUniqueAttack.TryFindCell(ToXy);


        //if (isFindedSimple || isFindedUnique)
        //{
        //    CellUnitsDataSystem.ResetAmountSteps(FromXy);
        //    CellUnitsDataSystem.ResetConditionType(FromXy);

        //    int damageToPrevious = 0;
        //    int damageToSelelected = 0;

        //    var unitTypePrevious = CellUnitsDataSystem.UnitType(FromXy);
        //    var unitTypeSelected = CellUnitsDataSystem.UnitType(ToXy);

        //    damageToSelelected += CellUnitsDataSystem.SimplePowerDamage(unitTypePrevious);
        //    damageToSelelected -= CellUnitsDataSystem.PowerProtection(ToXy);


        //    if (CellUnitsDataSystem.IsMelee(FromXy))
        //    {
        //        RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);

        //        damageToPrevious += CellUnitsDataSystem.SimplePowerDamage(unitTypeSelected);

        //        if (isFindedUnique)
        //        {
        //            damageToSelelected += CellUnitsDataSystem.UniquePowerDamage(unitTypePrevious);
        //        }
        //    }

        //    else
        //    {
        //        RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);

        //        if (isFindedUnique)
        //        {
        //            damageToSelelected += CellUnitsDataSystem.UniquePowerDamage(unitTypePrevious);
        //        }
        //    }

        //    if (damageToSelelected < 0) damageToSelelected = 0;

        //    CellUnitsDataSystem.TakeAmountHealth(FromXy, damageToPrevious);
        //    CellUnitsDataSystem.TakeAmountHealth(ToXy, damageToSelelected);


        //    if (!CellUnitsDataSystem.HaveAmountHealth(FromXy))
        //    {
        //        if (CellUnitsDataSystem.IsUnitType(UnitTypes.King, FromXy))
        //        {
        //            if (CellUnitsDataSystem.HaveOwner(ToXy))
        //            {
        //                RPCGameSystem.EndGameToMaster(CellUnitsDataSystem.ActorNumber(ToXy));
        //            }

        //            else if (CellUnitsDataSystem.IsBot(ToXy))
        //            {

        //            }
        //        }

        //        var isMasterFromUnit = CellUnitsDataSystem.IsMasterClient(FromXy);

        //        xyUnitsCom.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(FromXy), isMasterFromUnit, FromXy);
        //        MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(FromXy), CellUnitsDataSystem.UnitType(FromXy), isMasterFromUnit, FromXy);
        //        CellUnitsDataSystem.ResetUnit(FromXy);

        //        if (CellUnitsDataSystem.HaveOwner(FromXy))
        //        {
        //            xyUnitsCom.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), FromXy);
        //        }
        //    }

        //    if (!CellUnitsDataSystem.HaveAmountHealth(ToXy))
        //    {
        //        if (CellUnitsDataSystem.IsUnitType(UnitTypes.King, ToXy))
        //            RPCGameSystem.EndGameToMaster(CellUnitsDataSystem.ActorNumber(FromXy));

        //        var isMasterToUnit = CellUnitsDataSystem.IsMasterClient(ToXy);

        //        xyUnitsCom.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(ToXy), isMasterToUnit, ToXy);
        //        MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(ToXy), CellUnitsDataSystem.UnitType(ToXy), isMasterToUnit, ToXy);
        //        CellUnitsDataSystem.ResetUnit(ToXy);

        //        if (CellUnitsDataSystem.IsMelee(FromXy))
        //        {
        //            xyUnitsCom.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), FromXy);
        //            xyUnitsCom.AddAmountUnitInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), ToXy);
        //            CellUnitsDataSystem.ShiftPlayerUnitToBaseCell(FromXy, ToXy);
        //        }
        //    }

        //    _isAttacked = true;
        //}
        //else _isAttacked = false;


        //RPCGameSystem.AttackUnitToGeneral(Sender, _isAttacked);
        //RPCGameSystem.AttackUnitToGeneral(RpcTarget.All, false, _isAttacked, FromXy, ToXy);
    }
}
