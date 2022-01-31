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
            var uniq_cur = Es.MasterEs.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);

            var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;


            if (!UnitEs.CooldownAbility(uniq_cur, idx_0).Cooldown.Have)
            {
                if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    if (CellEs.EnvironmentEs.YoungForest( idx_0).HaveEnvironment)
                    {
                        CellEs.EnvironmentEs.YoungForest( idx_0).Destroy(Es.WhereEnviromentEs);

                        CellEs.EnvironmentEs.AdultForest( idx_0).SetNew(Es.WhereEnviromentEs);

                        UnitEs.StatEs.Step(idx_0).Steps.Amount -= CellUnitStepValues.NeedSteps(uniq_cur);

                        UnitEs.CooldownAbility(uniq_cur, idx_0).SetAfterAbility();

                        Es.Rpc.SoundToGeneral(sender, uniq_cur);

                        //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
                        //{
                        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
                        //}
                        var around = CellEs.GetXyAround(CellEs.CellE(idx_0).XyC.Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = CellEs.GetIdxCell(xy_1);

                            var unit_1 = UnitEs.Main(idx_1).UnitTC;
                            var ownUnit_1 = UnitEs.Main(idx_1).OwnerC;

                            if (unit_1.Have)
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