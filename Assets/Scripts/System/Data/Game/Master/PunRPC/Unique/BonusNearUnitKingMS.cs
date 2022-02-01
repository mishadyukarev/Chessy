namespace Game.Game
{
    sealed class BonusNearUnitKingMS : SystemCellAbstract, IEcsRunSystem
    {
        public BonusNearUnitKingMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = Es.MasterEs.UniqueAbilityC.Ability;


            var unit_0 = UnitEs.Main(idx_0).UnitTC;
            var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!UnitEs.CooldownAbility(uniq, idx_0).HaveCooldown)
            {
                if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq))
                {
                    UnitEs.CooldownAbility(uniq, idx_0).SetAfterAbility();

                    UnitEs.StatEs.Step(idx_0).Steps.Amount -= CellUnitStepValues.NeedSteps(uniq);
                    UnitEs.Main(idx_0).ResetCondition();

                    Es.Rpc.SoundToGeneral(sender, uniq);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = CellEs.GetXyAround(CellEs.CellE(idx_0).XyC.Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = CellEs.GetIdxCell(xy);

                        var ownUnit_1 = UnitEs.Main(idx_1).OwnerC;

                        if (UnitEs.Main(idx_1).HaveUnit(UnitStatEs))
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