using static Game.Game.CellUnitE;

namespace Game.Game
{
    struct UpgUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);
            UniqueAbilityMC.Get(out var unit_cur);

            ref var unit_0 = ref Unit<UnitTC>(idx_0);
            ref var levUnit_0 = ref Unit<LevelTC>(idx_0);
            ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);


            ref var unitE_0 = ref Unit<UnitCellEC>(idx_0);
            ref var hpUnit_0 = ref Unit<HpC>(idx_0);
            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);


            //var whoseMove = WhoseMoveC.WhoseMove;

            if (unitE_0.HaveMax)
            {
                if (stepUnit_0.Have(unit_cur))
                {
                    //if (InvResC.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    //{
                    //    InvResC.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                    //    Unit<UnitCellEC>(idx_0).Upgrade();

                    //    stepUnit_0.Take(unit_cur);

                    //    Unit<UnitCellEC>(idx_0).SetMaxHp();

                    //    EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.UpgradeMelee);
                    //}
                    //else
                    //{
                    //    EntityPool.Rpc<RpcC>().MistakeEconomyToGeneral(sender, needRes);
                    //}
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