using static Game.Game.CellEs;

namespace Game.Game
{
    struct BonusNearUnitKingMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = EntityMPool.UniqueAbilityC.Ability;


            ref var unit_0 = ref CellUnitEntities.Else(idx_0).UnitC;
            ref var ownUnit_0 = ref CellUnitEntities.Else(idx_0).OwnerC;

            ref var condUnit_0 = ref CellUnitEntities.Else(idx_0).ConditionC;


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!CellUnitEntities.CooldownUnique(uniq, idx_0).Cooldown.Have)
            {
                if (CellUnitEntities.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq))
                {
                    CellUnitEntities.CooldownUnique(uniq, idx_0).Cooldown.Amount = 3;

                    CellUnitEntities.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq));
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    EntityPool.Rpc.SoundToGeneral(sender, uniq);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = CellSpaceSupport.GetXyAround(Cell<XyC>(idx_0).Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = IdxCell(xy);

                        ref var unit_1 = ref CellUnitEntities.Else(idx_1).UnitC;
                        ref var ownUnit_1 = ref CellUnitEntities.Else(idx_1).OwnerC;

                        if (unit_1.Have)
                        {
                            if (ownUnit_1.Is(ownUnit_0.Player))
                            {
                                //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have)
                                //{
                                //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_1).Have = true;
                                //}
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