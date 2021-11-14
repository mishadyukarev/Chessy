using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class UpgradeUnitMasSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<UnitC, HpC, StepC, UnitEffectsC> _cellUnitDataFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);

            ref var unit_0 = ref _cellUnitDataFilt.Get1(idx_0);
            ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
            ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);
            ref var hpUnit_0 = ref _cellUnitDataFilt.Get2(idx_0);
            ref var stepUnit_0 = ref _cellUnitDataFilt.Get3(idx_0);
            ref var effUnit_0 =ref _cellUnitDataFilt.Get4(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (hpUnit_0.HaveMaxHp)
            {
                if (stepUnit_0.HaveMinSteps)
                {
                    if (InvResC.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        InvResC.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                        levUnit_0.SetLevel(LevelUnitTypes.Second);
                        WhereUnitsC.Add(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);

                        stepUnit_0.TakeSteps();

                        hpUnit_0.SetMaxHp();

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
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}