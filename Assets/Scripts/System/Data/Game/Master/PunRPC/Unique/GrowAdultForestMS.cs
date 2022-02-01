namespace Game.Game
{
    sealed class GrowAdultForestMS : SystemCellAbstract, IEcsRunSystem
    {
        public GrowAdultForestMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = Es.MasterEs.GrowAdultForest<IdxC>().Idx;
            var uniq_cur = Es.MasterEs.AbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);

            var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;


            if (!UnitEs(idx_0).CooldownAbility(uniq_cur).HaveCooldown)
            {
                if (UnitStatEs(idx_0).StepE.Have(uniq_cur))
                {
                    if (EnvironmentEs(idx_0).YoungForest.HaveEnvironment)
                    {
                        EnvironmentEs(idx_0).YoungForest.Destroy(Es.WhereEnviromentEs);

                        EnvironmentEs(idx_0).AdultForest.SetNew(Es.WhereEnviromentEs);

                        UnitStatEs(idx_0).StepE.Take(uniq_cur);

                        UnitEs(idx_0).CooldownAbility(uniq_cur).SetAfterAbility();

                        Es.Rpc.SoundToGeneral(sender, uniq_cur);

                        //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
                        //{
                        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
                        //}
                        var around = CellEsWorker.GetXyAround(CellEs(idx_0).CellE.XyC.Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = CellEsWorker.GetIdxCell(xy_1);

                            var ownUnit_1 = UnitEs(idx_1).MainE.OwnerC;

                            if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
                            {
                                if (ownUnit_1.Is(ownUnit_0.Player))
                                {
                                    //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                    //{
                                    //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                    //}
                                }
                            }
                        }

                    }

                    else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else
            {
                Es.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}