namespace Game.Game
{
    sealed class UpgradeUnitMS : SystemAbstract, IEcsRunSystem
    {
        public UpgradeUnitMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = Es.MasterEs.UpgradeUnit<IdxC>().Idx;

            var unit_0 = UnitEs.Main(idx_0).UnitTC;

            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            if (UnitEs.StatEs.Hp(idx_0).HaveMax)
            {
                if (UnitEs.StatEs.Step(idx_0).HaveSteps)
                {
                    if (Es.InventorResourcesEs.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        Es.InventorResourcesEs.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        UnitEs.Main(idx_0).Upgrade();
                        UnitEs.StatEs.Step(idx_0).Steps.Amount--;

                        UnitEs.StatEs.Hp(idx_0).Health.Amount = CellUnitHpValues.MAX_HP;

                        Es.Rpc.SoundToGeneral(sender, ClipTypes.UpgradeMelee);
                    }
                    else
                    {
                        Es.Rpc.MistakeEconomyToGeneral(sender, needRes);
                    }
                }
                else
                {
                    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}