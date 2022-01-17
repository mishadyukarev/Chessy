using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct BonusNearUnitKingMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var uniq);


            ref var unit_0 = ref Unit<UnitTC>(idx_0);
            ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);

            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            ref var condUnit_0 = ref Unit<ConditionUnitC>(idx_0);


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!Unit<CooldownC>(uniq, idx_0).HaveCooldown)
            {
                if (CellUnitStepEs.Have(idx_0, uniq))
                {
                    Unit<CooldownC>(uniq, idx_0).Cooldown = 3;

                    stepUnit_0.Take(uniq);
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, uniq);

                    if (!Unit<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    {
                        Unit<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    }

                    var around = CellSpaceC.GetXyAround(Cell<XyC>(idx_0).Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = IdxCell(xy);

                        ref var unit_1 = ref Unit<UnitTC>(idx_1);
                        ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);

                        if (unit_1.Have)
                        {
                            if (ownUnit_1.Is(ownUnit_0.Player))
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