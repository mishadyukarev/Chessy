namespace Game.Game
{
    sealed class BonusNearUnitKingMS : SystemAbstract, IEcsRunSystem
    {
        public BonusNearUnitKingMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = Es.MasterEs.UniqueAbilityC.Ability;


            ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
            ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;

            ref var condUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).ConditionC;


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!Es.CellEs.UnitEs.Unique(uniq, idx_0).Cooldown.Have)
            {
                if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq))
                {
                    Es.CellEs.UnitEs.Unique(uniq, idx_0).Cooldown.Amount = 3;

                    Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq));
                    if (condUnit_0.HaveCondition) condUnit_0.Reset();

                    Es.Rpc.SoundToGeneral(sender, uniq);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = Es.CellEs.GetXyAround(Es.CellEs.CellE(idx_0).XyC.Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = Es.CellEs.GetIdxCell(xy);

                        ref var unit_1 = ref Es.CellEs.UnitEs.Main(idx_1).UnitC;
                        ref var ownUnit_1 = ref Es.CellEs.UnitEs.Main(idx_1).OwnerC;

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
                    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else Es.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
        }
    }
}