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

            ref var fromUnitC = ref _cellUnitFilter.Get1(fromIdx);
            ref var fromOwnUnitCom = ref _cellUnitFilter.Get2(fromIdx);

            ref var toUnitC = ref _cellUnitFilter.Get1(toIdxAttack);
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
                fromUnitC.DefStat(StatTypes.Steps);
                fromUnitC.DefAmountSteps();
                fromUnitC.DefCondType();


                float powerDamFrom = 0;
                float powerDamTo = 0;

      
                powerDamFrom += fromUnitC.PowerDamageAttack(simpUniqueType);

                if (fromUnitC.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackArcher);
                

                if (toUnitC.IsMelee)
                {
                    powerDamTo += toUnitC.PowerDamageOnCell(toBuildDatCom.BuildType, toEnvDatCom.Envronments);
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
                if (fromUnitC.HaveShield)
                {
                    minusFrom = 0;
                    fromUnitC.TakeShieldProtect();
                }

                if (minusFrom >= 0)
                {
                    fromUnitC.TakeAmountHealth((int)minusFrom);
                    if (fromUnitC.AmountHealth <= 10) fromUnitC.TakeAmountHealth(10);
                }
                else throw new System.Exception();
                



                if (!toUnitC.IsMelee) minusTo = UnitValues.StandAmountHealth(toUnitC.UnitType);
                if (toUnitC.HaveShield)
                {
                    minusTo = 0;
                    toUnitC.TakeShieldProtect();
                }

                if (minusTo >= 0)
                {
                    toUnitC.TakeAmountHealth((int)minusTo);
                    if (toUnitC.AmountHealth <= 10) toUnitC.TakeAmountHealth(10);
                }
                else throw new System.Exception();
                


                if (!toUnitC.HaveAmountHealth)
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
                        toOwnUnitCom.PlayerType = fromOwnUnitCom.PlayerType;

                        fromUnitC.DefUnitType();

                        if (!toUnitC.HaveAmountHealth)
                        {
                            toUnitC.DefUnitType();
                        }
                    }
                }

                else if (!fromUnitC.HaveAmountHealth)
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