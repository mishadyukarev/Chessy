namespace Game.Game
{
    sealed class BonusNearUnitKingMS : SystemCellAbstract, IEcsRunSystem
    {
        internal BonusNearUnitKingMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = Es.MasterEs.AbilityC.Ability;


            var unit_0 = UnitEs(idx_0).MainE.UnitTC;
            var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;


            var sender = InfoC.Sender(MGOTypes.Master);

            if (!UnitEs(idx_0).CooldownAbility(uniq).HaveCooldown)
            {
                if (UnitStatEs(idx_0).StepE.Have(uniq))
                {
                    UnitEs(idx_0).CooldownAbility(uniq).SetAfterAbility();

                    UnitStatEs(idx_0).StepE.Take(uniq);
                    UnitEs(idx_0).MainE.ResetCondition();

                    Es.Rpc.SoundToGeneral(sender, uniq);

                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have)
                    //{
                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Damage, idx_0).Have = true;
                    //}

                    var around = CellEsWorker.GetXyAround(CellEs(idx_0).CellE.XyC.Xy);
                    foreach (var xy in around)
                    {
                        var idx_1 = CellEsWorker.GetIdxCell(xy);

                        var ownUnit_1 = UnitEs(idx_1).MainE.OwnerC;

                        if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
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