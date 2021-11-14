using Chessy.Common;
using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class GrowAdultForestMS : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyFilt = default;
        private EcsFilter<EnvC, EnvResC> _envFilt = default;

        private EcsFilter<UnitC, OwnerC> _unitMainFilt = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<CdownUniqC> _unitUniqFilt = default;
        private EcsFilter<UnitC, UnitEffectsC> _unitEffFilt = default;

        public void Run()
        {
            ForGrowAdultForestMC.Get(out var idx_0);

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var ownUnit_0 = ref _unitMainFilt.Get2(idx_0);
            ref var unitStep_0 = ref _statUnitF.Get2(idx_0);
            ref var cdUniq_0 = ref _unitUniqFilt.Get1(idx_0);
            ref var effUnit_0 = ref _unitEffFilt.Get2(idx_0);

            ref var env_0 = ref _envFilt.Get1(idx_0);
            ref var envRes_0 = ref _envFilt.Get2(idx_0);


            if (!cdUniq_0.HaveCooldown(UniqAbilTypes.GrowAdultForest))
            {
                if (unitStep_0.HaveMinSteps)
                {
                    if (env_0.Have(EnvTypes.YoungForest))
                    {
                        WhereEnvC.Remove(EnvTypes.YoungForest, idx_0);
                        env_0.Remove(EnvTypes.YoungForest);

                        env_0.Set(EnvTypes.AdultForest);
                        envRes_0.SetMaxAmountRes(EnvTypes.AdultForest);
                        WhereEnvC.Add(EnvTypes.AdultForest, idx_0);

                        unitStep_0.TakeSteps();

                        cdUniq_0.SetCooldown(UniqAbilTypes.GrowAdultForest, 5);

                        RpcSys.SoundToGeneral(sender, UniqAbilTypes.GrowAdultForest);

                        if (!effUnit_0.Have(UnitStatTypes.Steps)) effUnit_0.Set(UnitStatTypes.Steps);

                        var around = CellSpace.GetXyAround(_xyFilt.Get1(idx_0).Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = _xyFilt.GetIdxCell(xy_1);

                            ref var unit_1 = ref _statUnitF.Get1(idx_1);
                            ref var ownUnit_1 = ref _unitMainFilt.Get2(idx_1);
                            ref var effUnit_1 = ref _unitEffFilt.Get2(idx_1);   

                            if (unit_1.HaveUnit)
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
                RpcSys.SoundToGeneral(sender, ClipGameTypes.Mistake);
            }
        }
    }
}