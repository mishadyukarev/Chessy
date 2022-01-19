using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct UpgradeUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = EntityMPool.UpgradeUnit<IdxC>().Idx;

            ref var unit_0 = ref Unit<UnitTC>(idx_0);
            ref var levUnit_0 = ref Unit<LevelTC>(idx_0);
            ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);


            ref var hpUnit_0 = ref CellUnitHpEs.Hp<AmountC>(idx_0);


            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            if (CellUnitHpEs.HaveMax(idx_0))
            {
                if (CellUnitStepEs.Steps<AmountC>(idx_0).Have)
                {
                    if (InventorResourcesE.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        InventorResourcesE.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        Unit<LevelTC>(idx_0).Level = LevelTypes.Second;
                        CellUnitStepEs.Steps<AmountC>(idx_0).Take();

                        CellUnitHpEs.SetMaxHp(idx_0);

                        EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.UpgradeMelee);
                    }
                    else
                    {
                        EntityPool.Rpc<RpcC>().MistakeEconomyToGeneral(sender, needRes);
                    }
                }
                else
                {
                    EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}