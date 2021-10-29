using Leopotam.Ecs;
using Photon.Pun;

namespace Scripts.Game
{
    internal sealed class AttackMastSys : IEcsRunSystem
    {
        private EcsFilter<ForAttackMasCom> _forAttackFilter = default;

        private EcsFilter<CellUnitDataCom, HpComponent, DamageComponent, StepComponent, ToolWeaponC, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;

        public void Run()
        {
            ref var forAttackMasCom = ref _forAttackFilter.Get1(0);

            var fromIdx = forAttackMasCom.IdxFromCell;
            var toIdxAttack = forAttackMasCom.IdxToCell;

            ref var fromUnitC = ref _cellUnitFilter.Get1(fromIdx);
            ref var fromHpUnitC = ref _cellUnitFilter.Get2(fromIdx);
            ref var fromDamUnitC = ref _cellUnitFilter.Get3(fromIdx);
            ref var fromStepUnitC = ref _cellUnitFilter.Get4(fromIdx);
            ref var twUnitC_from = ref _cellUnitFilter.Get5(fromIdx);
            ref var fromOwnUnitCom = ref _cellUnitFilter.Get6(fromIdx);

            ref var toUnitC = ref _cellUnitFilter.Get1(toIdxAttack);
            ref var toHpUnitC = ref _cellUnitFilter.Get2(toIdxAttack);
            ref var toDamUnitC =ref _cellUnitFilter.Get3(toIdxAttack);
            ref var toStepUnitC = ref _cellUnitFilter.Get4(toIdxAttack);
            ref var twUnitC_to = ref _cellUnitFilter.Get5(toIdxAttack);
            ref var toOwnUnitCom = ref _cellUnitFilter.Get6(toIdxAttack);
            ref var toBuildDatCom = ref _cellBuildFilter.Get1(toIdxAttack);
            ref var toEnvDatCom = ref _cellEnvFilter.Get1(toIdxAttack);


            var simpUniqueType = CellsAttackC.FindByIdx(fromOwnUnitCom.PlayerType, fromIdx, toIdxAttack);

            if (simpUniqueType != default)
            {
                fromUnitC.DefStat(StatTypes.Steps);
                fromStepUnitC.DefAmountSteps();
                fromUnitC.DefCondType();


                float powerDamFrom = 0;
                float powerDamTo = 0;

      
                powerDamFrom += fromDamUnitC.PowerDamageAttack(fromUnitC, twUnitC_from, simpUniqueType);

                if (fromUnitC.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);
                

                if (toUnitC.IsMelee)
                {
                    powerDamTo += toDamUnitC.PowerDamageOnCell(toUnitC, twUnitC_to, toBuildDatCom.BuildType, toEnvDatCom.Envronments);
                }


                float min = 0;
                float max = 0;
                float minusTo = 0;
                float minusFrom = 0;

                if (powerDamTo > powerDamFrom)
                {
                    max = powerDamTo * 2f;
                    min = powerDamTo / 2f;
                    if(min < powerDamFrom) minusTo = 100 * powerDamFrom / max;

                    max = powerDamFrom * 2;
                    minusFrom = 100 * powerDamTo / max;
                }
                else
                {
                    max = powerDamTo * 2f;
                    minusTo = 100 * powerDamFrom / max;

                    max = powerDamFrom * 2;
                    min = powerDamFrom / 2;
                    if (min < powerDamTo) minusFrom = 100 * powerDamTo / max;
                }







                if (!fromUnitC.IsMelee) minusFrom = 0;
                if (twUnitC_from.HaveShield)
                {
                    minusFrom = 0;
                    twUnitC_from.TakeShieldProtect();
                }

                if (minusFrom >= 0)
                {
                    fromHpUnitC.TakeAmountHealth((int)minusFrom);
                    if (fromHpUnitC.AmountHealth <= 10) fromHpUnitC.TakeAmountHealth(10);
                }
                else throw new System.Exception();
                



                if (!toUnitC.IsMelee) minusTo = UnitValues.StandAmountHealth(toUnitC.UnitType);
                if (twUnitC_to.HaveShield)
                {
                    minusTo = 0;
                    twUnitC_to.TakeShieldProtect();
                }

                if (minusTo >= 0)
                {
                    toHpUnitC.TakeAmountHealth((int)minusTo);
                    if (toHpUnitC.AmountHealth <= 10) toHpUnitC.TakeAmountHealth(10);
                }
                else throw new System.Exception();
                


                if (!toHpUnitC.HaveAmountHealth)
                {
                    if (toUnitC.Is(UnitTypes.King))
                    {
                        EndGameDataUIC.PlayerWinner = fromOwnUnitCom.PlayerType;
                    }
                    else if(toUnitC.Is(UnitTypes.Scout))
                    {
                        InventorUnitsC.AddUnitsInInventor(toOwnUnitCom.PlayerType, toUnitC.UnitType, LevelUnitTypes.Wood);
                    }

                    toUnitC.DefUnitType();


                    if (fromUnitC.IsMelee)
                    {
                        toUnitC.ReplaceUnit(fromUnitC);
                        toHpUnitC.AmountHealth = fromHpUnitC.AmountHealth;
                        toStepUnitC.AmountSteps = fromStepUnitC.AmountSteps;
                        toOwnUnitCom.PlayerType = fromOwnUnitCom.PlayerType;

                        fromUnitC.DefUnitType();

                        if (!toHpUnitC.HaveAmountHealth)
                        {
                            toUnitC.DefUnitType();
                        }
                    }
                }

                else if (!fromHpUnitC.HaveAmountHealth)
                {
                    if (fromUnitC.Is(UnitTypes.King))
                    {
                        fromOwnUnitCom.PlayerType = toOwnUnitCom.PlayerType;
                        EndGameDataUIC.PlayerWinner = toOwnUnitCom.PlayerType;
                    }

                    fromUnitC.DefUnitType();
                }
            }
        }
    }
}