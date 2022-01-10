using static Game.Game.EntityCellPool;

namespace Game.Game
{
    struct GrowAdultForestMS : IEcsRunSystem
    {
        public void Run()
        {
            ForGrowAdultForestMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq_cur);

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);


            if (!Unit<CooldownC>(uniq_cur, idx_0).HaveCooldown)
            {
                if (stepUnit_0.Have(uniq_cur))
                {
                    if (Environment<HaveEnvironmentC>(EnvTypes.YoungForest, idx_0).Have)
                    {
                        Environment<EnvCellEC>(EnvTypes.YoungForest, idx_0).Remove();

                        Environment<EnvCellEC>(EnvTypes.AdultForest, idx_0).SetNew();

                        stepUnit_0.Take(uniq_cur);

                        Unit<CooldownC>(uniq_cur, idx_0).Cooldown = 5;

                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, uniq_cur);

                        if (!Unit<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
                        {
                            Unit<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
                        }
                        var around = CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = IdxCell(xy_1);

                            ref var unit_1 = ref Unit<UnitC>(idx_1);
                            ref var ownUnit_1 = ref Unit<OwnerC>(idx_1);

                            if (unit_1.Have)
                            {
                                if (ownUnit_1.Is(ownUnit_0.Owner))
                                {
                                    if (!Unit<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                    {
                                        Unit<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                    }
                                }
                            }
                        }

                    }

                    else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else
            {
                EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}