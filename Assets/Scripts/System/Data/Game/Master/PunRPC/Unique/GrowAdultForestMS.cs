using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct GrowAdultForestMS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_0 = Entities.MasterEs.GrowAdultForest<IdxC>().Idx;
            var uniq_cur = Entities.MasterEs.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;


            if (!Entities.CellEs.UnitEs.CooldownUnique(uniq_cur, idx_0).Cooldown.Have)
            {
                if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
                {
                    if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).Resources.Have)
                    {
                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.YoungForest, idx_0).Remove();

                        Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).SetNew();

                        Entities.CellEs.UnitEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));

                        Entities.CellEs.UnitEs.CooldownUnique(uniq_cur, idx_0).Cooldown.Amount = 5;

                        Entities.Rpc.SoundToGeneral(sender, uniq_cur);

                        //if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
                        //{
                        //    CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
                        //}
                        var around = CellSpaceSupport.GetXyAround(Entities.CellEs.CellE(idx_0).XyC.Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = Entities.CellEs.IdxCell(xy_1);

                            ref var unit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).UnitC;
                            ref var ownUnit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).OwnerC;

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

                    else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else
            {
                Entities.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}