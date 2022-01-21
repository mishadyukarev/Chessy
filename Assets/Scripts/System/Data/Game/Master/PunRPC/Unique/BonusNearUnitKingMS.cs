using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct BonusNearUnitKingMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = EntityMPool.UniqueAbilityC.Ability;


            ref var unit_0 = ref Unit(idx_0);
            ref var ownUnit_0 = ref CellUnitElseEs.Owner(idx_0);

            ref var condUnit_0 = ref CellUnitElseEs.Condition(idx_0);


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!CellUnitAbilityUniqueEs.Cooldown(uniq, idx_0).Have)
            {
                if (CellUnitStepEs.Have(idx_0, uniq))
                {
                    CellUnitAbilityUniqueEs.Cooldown(uniq, idx_0).Amount = 3;

                    CellUnitStepEs.Take(idx_0, uniq);
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    EntityPool.Rpc.SoundToGeneral(sender, uniq);

                    if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    {
                        CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    }

                    var around = CellSpaceSupport.GetXyAround(Cell<XyC>(idx_0).Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = IdxCell(xy);

                        ref var unit_1 = ref Unit(idx_1);
                        ref var ownUnit_1 = ref CellUnitElseEs.Owner(idx_1);

                        if (unit_1.Have)
                        {
                            if (ownUnit_1.Is(ownUnit_0.Player))
                            {
                                if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have)
                                {
                                    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}