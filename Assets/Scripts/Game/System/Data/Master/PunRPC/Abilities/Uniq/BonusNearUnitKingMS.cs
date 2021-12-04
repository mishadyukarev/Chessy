using Leopotam.Ecs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class BonusNearUnitKingMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq);


            ref var unit_0 = ref Unit<UnitC>(idx_0);
            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

            ref var stepUnit_0 = ref Unit<StepUnitC>(idx_0);

            ref var condUnit_0 = ref Unit<ConditionC>(idx_0);
            ref var effUnit_0 = ref Unit<EffectsC>(idx_0);

            ref var uniq_0 = ref Unit<UniqAbilC>(idx_0);
            ref var cdUniq_0 = ref Unit<CooldownUniqC>(idx_0);


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!cdUniq_0.HaveCooldown(uniq))
            {
                if (stepUnit_0.Have(uniq))
                {
                    cdUniq_0.SetCooldown(uniq, 3);

                    stepUnit_0.Take(uniq);
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    RpcSys.SoundToGeneral(sender, uniq);

                    if (!effUnit_0.Have(UnitStatTypes.Damage)) effUnit_0.Set(UnitStatTypes.Damage);

                    var around = CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = IdxCell(xy);

                        ref var unit_1 = ref Unit<UnitC>(idx_1);
                        ref var ownUnit_1 = ref Unit<OwnerC>(idx_1);

                        ref var effUnitC_1 = ref Unit<EffectsC>(idx_1);


                        if (unit_1.Have)
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