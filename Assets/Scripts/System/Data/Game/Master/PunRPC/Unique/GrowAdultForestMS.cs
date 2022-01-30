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

            ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;


            if (!Es.CellEs.UnitEs.Unique(uniq_cur, idx_0).Cooldown.Have)
            {
                if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    if (Es.CellEs.EnvironmentEs.YoungForest( idx_0).HaveEnvironment)
                    {
                        Es.CellEs.EnvironmentEs.YoungForest( idx_0).Destroy(Es.WhereEnviromentEs);

                        Es.CellEs.EnvironmentEs.AdultForest( idx_0).SetNew(Es.WhereEnviromentEs);

                        Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                        Es.CellEs.UnitEs.Unique(uniq_cur, idx_0).Cooldown.Amount = 5;

                        Es.Rpc.SoundToGeneral(sender, uniq_cur);

                        //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
                        //{
                        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
                        //}
                        var around = Es.CellEs.GetXyAround(Es.CellEs.CellE(idx_0).XyC.Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = Es.CellEs.GetIdxCell(xy_1);

                            ref var unit_1 = ref Es.CellEs.UnitEs.Main(idx_1).UnitC;
                            ref var ownUnit_1 = ref Es.CellEs.UnitEs.Main(idx_1).OwnerC;

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