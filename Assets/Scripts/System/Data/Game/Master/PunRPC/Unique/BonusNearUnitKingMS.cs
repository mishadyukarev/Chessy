using static Game.Game.CellEs;

namespace Game.Game
{
    struct BonusNearUnitKingMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = Entities.MasterEs.UniqueAbilityC.Ability;


            ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
            ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;

            ref var condUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).ConditionC;


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!Entities.CellEs.UnitEs.CooldownUnique(uniq, idx_0).Cooldown.Have)
            {
                if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq))
                {
                    Entities.CellEs.UnitEs.CooldownUnique(uniq, idx_0).Cooldown.Amount = 3;

                    Entities.CellEs.UnitEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq));
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    Entities.Rpc.SoundToGeneral(sender, uniq);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = CellSpaceSupport.GetXyAround(Entities.CellEs.CellE(idx_0).XyC.Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = Entities.CellEs.IdxCell(xy);

                        ref var unit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).UnitC;
                        ref var ownUnit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).OwnerC;

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