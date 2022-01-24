using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct UpgradeUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = EntityMPool.UpgradeUnit<IdxC>().Idx;

            ref var unit_0 = ref Unit(idx_0);
            ref var levUnit_0 = ref EntitiesPool.UnitElse.Level(idx_0);
            ref var ownUnit_0 = ref EntitiesPool.UnitElse.Owner(idx_0);


            ref var hpUnit_0 = ref EntitiesPool.UnitHps[idx_0].Hp;


            var whoseMove = WhoseMoveE.WhoseMove.Player;

            if (EntitiesPool.UnitHps[idx_0].HaveMax)
            {
                if (EntitiesPool.UnitStep.Steps(idx_0).Have)
                {
                    if (InventorResourcesE.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        InventorResourcesE.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        EntitiesPool.UnitElse.Level(idx_0).Level = LevelTypes.Second;
                        EntitiesPool.UnitStep.Steps(idx_0).Take();

                        EntitiesPool.UnitHps[idx_0].Hp.Amount = UnitHpValues.MAX_HP;

                        EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.UpgradeMelee);
                    }
                    else
                    {
                        EntityPool.Rpc.MistakeEconomyToGeneral(sender, needRes);
                    }
                }
                else
                {
                    EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}