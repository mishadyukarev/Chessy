using Game.Common;
using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GrowAdultForestMS : IEcsRunSystem
    {
        private EcsFilter<EnvC, EnvResC> _envFilt = default;

        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<CooldownUniqC> _uniqUnitF = default;
        private EcsFilter<EffectsC> _effUnitF = default;

        public void Run()
        {
            ForGrowAdultForestMC.Get(out var idx_0);

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var ownUnit_0 = ref _unitF.Get2(idx_0);

            ref var unitStep_0 = ref _statUnitF.Get1(idx_0);
            ref var cdUniq_0 = ref _uniqUnitF.Get1(idx_0);
            ref var effUnit_0 = ref _effUnitF.Get1(idx_0);

            ref var env_0 = ref _envFilt.Get1(idx_0);
            ref var envRes_0 = ref _envFilt.Get2(idx_0);


            if (!cdUniq_0.HaveCooldown(UniqAbilTypes.GrowAdultForest))
            {
                if (unitStep_0.HaveMin)
                {
                    if (env_0.Have(EnvTypes.YoungForest))
                    {
                        env_0.Remove(EnvTypes.YoungForest);

                        env_0.SetNew(EnvTypes.AdultForest);
                        envRes_0.SetMax(EnvTypes.AdultForest);

                        unitStep_0.Take();

                        cdUniq_0.SetCooldown(UniqAbilTypes.GrowAdultForest, 5);

                        RpcSys.SoundToGeneral(sender, UniqAbilTypes.GrowAdultForest);

                        if (!effUnit_0.Have(UnitStatTypes.Steps)) effUnit_0.Set(UnitStatTypes.Steps);

                        var around = CellSpaceC.XyAround(EntityPool.Cell<XyC>(idx_0).Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = EntityPool.IdxCell(xy_1);

                            ref var unit_1 = ref _unitF.Get1(idx_1);
                            ref var ownUnit_1 = ref _unitF.Get2(idx_1);
                            ref var effUnit_1 = ref _effUnitF.Get1(idx_1);   

                            if (unit_1.Have)
                            {
                                if (ownUnit_1.Is(ownUnit_0.Owner))
                                {
                                    if (!effUnit_1.Have(UnitStatTypes.Steps))
                                    {
                                        effUnit_1.Set(UnitStatTypes.Steps);
                                    }
                                }
                            }
                        }

                    }

                    else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else
            {
                RpcSys.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}