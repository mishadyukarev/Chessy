using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellUnitPool;

namespace Game.Game
{
    struct BonusNearUnitKingMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq);


            ref var unit_0 = ref Unit<UnitC>(idx_0);
            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            ref var condUnit_0 = ref Unit<ConditionC>(idx_0);


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!Unit<CooldownC>(uniq, idx_0).HaveCooldown)
            {
                if (stepUnit_0.Have(uniq))
                {
                    Unit<CooldownC>(uniq, idx_0).Cooldown = 3;

                    stepUnit_0.Take(uniq);
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, uniq);

                    if (!Unit<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    {
                        Unit<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    }

                    var around = CellSpaceC.XyAround(Cell<XyC>(idx_0).Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = IdxCell(xy);

                        ref var unit_1 = ref Unit<UnitC>(idx_1);
                        ref var ownUnit_1 = ref Unit<OwnerC>(idx_1);

                        if (unit_1.Have)
                        {
                            if (ownUnit_1.Is(ownUnit_0.Owner))
                            {
                                if (!Unit<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have)
                                {
                                    Unit<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}