using static Game.Game.CellEs;

namespace Game.Game
{
    struct BonusNearUnitKingMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = EntitiesMaster.UniqueAbilityC.Ability;


            ref var unit_0 = ref CellUnitEs.Else(idx_0).UnitC;
            ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;

            ref var condUnit_0 = ref CellUnitEs.Else(idx_0).ConditionC;


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!CellUnitEs.CooldownUnique(uniq, idx_0).Cooldown.Have)
            {
                if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq))
                {
                    CellUnitEs.CooldownUnique(uniq, idx_0).Cooldown.Amount = 3;

                    CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq));
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    Entities.Rpc.SoundToGeneral(sender, uniq);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = CellSpaceSupport.GetXyAround(Cell(idx_0).XyC.Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = IdxCell(xy);

                        ref var unit_1 = ref CellUnitEs.Else(idx_1).UnitC;
                        ref var ownUnit_1 = ref CellUnitEs.Else(idx_1).OwnerC;

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
                    Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else Entities.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}