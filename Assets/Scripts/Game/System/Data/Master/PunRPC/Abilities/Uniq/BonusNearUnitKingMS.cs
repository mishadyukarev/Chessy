using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class BonusNearUnitKingMS : IEcsRunSystem
    {
        private EcsFilter<XyC> _cellXyFilt = default;
        private EcsFilter<UnitC, OwnerC> _mainUnitF;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, UnitEffectsC> _effUnitF = default;
        private EcsFilter<UniqAbilC, CooldownUniqC> _uniqUnitF = default;

        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);

            ref var unit_0 = ref _mainUnitF.Get1(idx_0);
            ref var ownUnit_0 = ref _mainUnitF.Get2(idx_0);

            ref var stepUnit_0 =ref _statUnitF.Get1(idx_0);

            ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
            ref var effUnit_0 = ref _effUnitF.Get2(idx_0);

            ref var uniq_0 = ref _uniqUnitF.Get1(idx_0);
            ref var cdUniq_0 = ref _uniqUnitF.Get2(idx_0);
     

            var sender = InfoC.Sender(MGOTypes.Master);

            if (!cdUniq_0.HaveCooldown(UniqAbilTypes.BonusNear))
            {
                if (stepUnit_0.HaveMinSteps)
                {
                    cdUniq_0.SetCooldown(UniqAbilTypes.BonusNear, 3);

                    stepUnit_0.TakeSteps();
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    RpcSys.SoundToGeneral(sender, UniqAbilTypes.BonusNear);

                    if (!effUnit_0.Have(UnitStatTypes.Damage)) effUnit_0.Set(UnitStatTypes.Damage);

                    var around = CellSpace.GetXyAround(_cellXyFilt.Get1(idx_0).Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = _cellXyFilt.GetIdxCell(xy);

                        ref var unit_1 = ref _mainUnitF.Get1(idx_1);
                        ref var ownUnit_1 = ref _mainUnitF.Get2(idx_1);

                        ref var effUnitC_1 = ref _effUnitF.Get2(idx_1);
                        

                        if (unit_1.HaveUnit)
                        {
                            if (ownUnit_1.Is(ownUnit_0.Owner))
                            {
                                if (!effUnitC_1.Have(UnitStatTypes.Damage))
                                {
                                    effUnitC_1.Set(UnitStatTypes.Damage);
                                }
                            }
                        }
                    }
                }
                else
                {
                    RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else RpcSys.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}