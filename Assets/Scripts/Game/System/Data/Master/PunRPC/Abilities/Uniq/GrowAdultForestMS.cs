using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class GrowAdultForestMS : IEcsRunSystem
    {
        public void Run()
        {
            ForGrowAdultForestMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

            ref var stepUnit_0 = ref Unit<StepUnitWC>(idx_0);
            ref var cdUniq_0 = ref Unit<CooldownUniqC>(idx_0);
            ref var effUnit_0 = ref Unit<EffectsC>(idx_0);

            ref var envCell_0 = ref Environment<EnvCellC>(idx_0);
            ref var env_0 = ref Environment<EnvC>(idx_0);
            ref var envRes_0 = ref Environment<EnvResC>(idx_0);


            if (!cdUniq_0.HaveCooldown(uniq_cur))
            {
                if (stepUnit_0.Have(uniq_cur))
                {
                    if (env_0.Have(EnvTypes.YoungForest))
                    {
                        envCell_0.Remove(EnvTypes.YoungForest);

                        envCell_0.SetNew(EnvTypes.AdultForest);

                        stepUnit_0.Take(uniq_cur);

                        cdUniq_0.SetCooldown(uniq_cur, 5);

                        RpcSys.SoundToGeneral(sender, uniq_cur);

                        if (!effUnit_0.Have(UnitStatTypes.Steps)) effUnit_0.Set(UnitStatTypes.Steps);

                        var around = CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = IdxCell(xy_1);

                            ref var unit_1 = ref Unit<UnitC>(idx_1);
                            ref var ownUnit_1 = ref Unit<OwnerC>(idx_1);
                            ref var effUnit_1 = ref Unit<EffectsC>(idx_1);

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