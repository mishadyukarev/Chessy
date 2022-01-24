using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct GrowAdultForestMS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_0 = EntityMPool.GrowAdultForest<IdxC>().Idx;
            var uniq_cur = EntityMPool.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var ownUnit_0 = ref EntitiesPool.UnitElse.Owner(idx_0);


            if (!CellUnitAbilityUniqueEs.Cooldown(uniq_cur, idx_0).Have)
            {
                if (EntitiesPool.UnitStep.Have(idx_0, uniq_cur))
                {
                    if (Resources(EnvironmentTypes.YoungForest, idx_0).Have)
                    {
                        Remove(EnvironmentTypes.YoungForest, idx_0);

                        SetNew(EnvironmentTypes.AdultForest, idx_0);

                        EntitiesPool.UnitStep.Take(idx_0, uniq_cur);

                        CellUnitAbilityUniqueEs.Cooldown(uniq_cur, idx_0).Amount = 5;

                        EntityPool.Rpc.SoundToGeneral(sender, uniq_cur);

                        if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have)
                        {
                            CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_0).Have = true;
                        }
                        var around = CellSpaceSupport.GetXyAround(Cell<XyC>(idx_0).Xy);
                        foreach (var xy_1 in around)
                        {
                            var idx_1 = IdxCell(xy_1);

                            ref var unit_1 = ref Unit(idx_1);
                            ref var ownUnit_1 = ref EntitiesPool.UnitElse.Owner(idx_1);

                            if (unit_1.Have)
                            {
                                if (ownUnit_1.Is(ownUnit_0.Player))
                                {
                                    if (!CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have)
                                    {
                                        CellUnitEffectsEs.HaveEffect<HaveEffectC>(UnitStatTypes.Steps, idx_1).Have = true;
                                    }
                                }
                            }
                        }

                    }

                    else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedOtherPlace, sender);
                }
                else EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else
            {
                EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.Mistake);
            }
        }
    }
}