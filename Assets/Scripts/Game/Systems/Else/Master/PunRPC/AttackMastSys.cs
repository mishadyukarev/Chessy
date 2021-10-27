using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class AttackMastSys : IEcsRunSystem
    {
        private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        public void Run()
        {
            ref var forAttackMasCom = ref _forAttackFilter.Get1(0);

            var fromIdx = forAttackMasCom.IdxFromCell;
            var toIdxAttack = forAttackMasCom.IdxToCell;

            ref var fromUnitDatCom = ref _cellUnitFilter.Get1(fromIdx);
            ref var fromOwnUnitCom = ref _cellUnitFilter.Get2(fromIdx);

            ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdxAttack);
            ref var toOwnUnitCom = ref _cellUnitFilter.Get2(toIdxAttack);
            ref var toBuildDatCom = ref _cellBuildFilter.Get1(toIdxAttack);
            ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdxAttack);

            AttackTypes simpUniqueType = default;


            if (CellsAttackC.FindByIdx(fromOwnUnitCom.PlayerType, AttackTypes.Simple, fromIdx, toIdxAttack))
                simpUniqueType = AttackTypes.Simple;

            if (CellsAttackC.FindByIdx(fromOwnUnitCom.PlayerType, AttackTypes.Unique, fromIdx, toIdxAttack))
                simpUniqueType = AttackTypes.Unique;





            if (simpUniqueType != default)
            {
                fromUnitDatCom.DefAmountSteps();
                fromUnitDatCom.DefCondType();


                float powerDamFrom = 0;
                float powerDamTo = 0;

      
                powerDamFrom += fromUnitDatCom.PowerDamageAttack(simpUniqueType);

                if (fromUnitDatCom.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);
                

                if (toUnitDatCom.IsMelee)
                {
                    powerDamTo += toUnitDatCom.PowerDamageOnCell(toBuildDatCom.BuildType, toEnvDatCom.Envronments);
                }


                float maxPowerDamage = powerDamTo * 1.5f;


                var percentFrom = powerDamFrom * 100 / maxPowerDamage;
                float minusFrom = powerDamTo * percentFrom / 100;


                var percentTo = powerDamFrom * 100 / maxPowerDamage;
                float minusTo = powerDamFrom * percentTo / 100;


                if (minusFrom > 0) fromUnitDatCom.TakeAmountHealth((int)minusFrom);
                if (minusTo > 0) toUnitDatCom.TakeAmountHealth((int)minusTo);


                //if (toUnitDatCom.Is(UnitTypes.Pawn))
                //{
                //    if (toUnitDatCom.TWExtraType == ToolWeaponTypes.Shield)
                //    {
                //        if (toUnitDatCom.ShieldProtection >= 1)
                //        {
                //            toUnitDatCom.TakeShieldProtect();
                //            damageTo = 0;

                //            if (toUnitDatCom.ShieldProtection == 0)
                //            {
                //                toUnitDatCom.TWExtraType = ToolWeaponTypes.None;
                //            }
                //        }
                //    }
                //}
                //if (fromUnitDatCom.Is(UnitTypes.Pawn))
                //{
                //    if (fromUnitDatCom.TWExtraType == ToolWeaponTypes.Shield)
                //    {
                //        if (fromUnitDatCom.ShieldProtection >= 1)
                //        {
                //            fromUnitDatCom.TakeShieldProtect();
                //            damageFrom = 0;

                //            if (fromUnitDatCom.ShieldProtection == 0)
                //            {
                //                fromUnitDatCom.TWExtraType = ToolWeaponTypes.None;
                //            }
                //        }
                //    }
                //}



                if (!toUnitDatCom.HaveAmountHealth)
                {
                    if (toUnitDatCom.Is(UnitTypes.King))
                    {
                        EndGameDataUIC.PlayerWinner = fromOwnUnitCom.PlayerType;
                    }
                    else if(toUnitDatCom.Is(UnitTypes.Scout))
                    {
                        InventorUnitsC.AddUnitsInInventor(toOwnUnitCom.PlayerType, toUnitDatCom.UnitType, LevelUnitTypes.Wood);
                    }

                    toUnitDatCom.DefUnitType();


                    if (fromUnitDatCom.IsMelee)
                    {
                        toUnitDatCom.ReplaceUnit(fromUnitDatCom);
                        toOwnUnitCom.PlayerType = fromOwnUnitCom.PlayerType;

                        fromUnitDatCom.DefUnitType();

                        if (!toUnitDatCom.HaveAmountHealth)
                        {
                            toUnitDatCom.DefUnitType();
                        }
                    }
                }

                else if (!fromUnitDatCom.HaveAmountHealth)
                {
                    if (fromUnitDatCom.Is(UnitTypes.King))
                    {
                        fromOwnUnitCom.PlayerType = toOwnUnitCom.PlayerType;
                        EndGameDataUIC.PlayerWinner = toOwnUnitCom.PlayerType;
                    }

                    fromUnitDatCom.DefUnitType();
                }
            }
        }
    }
}