using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class UpgUnitMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<UnitEffectsC> _effUnitF = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);

            ref var unit_0 = ref _unitF.Get1(idx_0);
            ref var levUnit_0 = ref _unitF.Get2(idx_0);
            ref var ownUnit_0 = ref _unitF.Get3(idx_0);

            
            
            ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);
            ref var stepUnit_0 = ref _statUnitF.Get2(idx_0);

            ref var effUnit_0 = ref _effUnitF.Get1(idx_0);


            var whoseMove = WhoseMoveC.WhoseMove;

            if (hpUnit_0.HaveMax)
            {
                if (stepUnit_0.HaveMin)
                {
                    if (InvResC.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        InvResC.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        levUnit_0.Set(LevelTypes.Second);

                        stepUnit_0.TakeSteps();

                        hpUnit_0.SetMax();

                        RpcSys.SoundToGeneral(sender, ClipTypes.UpgradeMelee);
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