using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class UpgradeUnitMasSys : IEcsRunSystem
    {
        private EcsFilter<ForUpgradeUnitCom> _forUpgradeUnitFilt = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent, UnitEffectsC> _cellUnitDataFilt = default;
        private EcsFilter<InventResC> _invResFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            ref var idx_0 = ref _forUpgradeUnitFilt.Get1(0).idxCellForUpgrade;

            ref var unit_0 = ref _cellUnitDataFilt.Get1(idx_0);
            ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
            ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);
            ref var hpUnit_0 = ref _cellUnitDataFilt.Get2(idx_0);
            ref var stepUnit_0 = ref _cellUnitDataFilt.Get3(idx_0);
            ref var effUnit_0 =ref _cellUnitDataFilt.Get4(idx_0);


            ref var invResCom = ref _invResFilt.Get1(0);



            var whoseMove = WhoseMoveC.WhoseMove;

            if (hpUnit_0.HaveMaxHpUnit(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Hp), UnitsUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Hp)))
            {
                if (stepUnit_0.HaveMinSteps)
                {
                    if (InventResC.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        InventResC.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                        levUnit_0.SetLevel(LevelUnitTypes.Iron);
                        WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                        stepUnit_0.TakeSteps();

                        hpUnit_0.SetMaxHp(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Hp), UnitsUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Hp));

                        RpcSys.SoundToGeneral(sender, ClipGameTypes.UpgradeUnitMelee);
                    }
                    else
                    {
                        RpcSys.MistakeEconomyToGeneral(sender, needRes);
                    }
                }
                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHealth, sender);
            }
        }
    }
}