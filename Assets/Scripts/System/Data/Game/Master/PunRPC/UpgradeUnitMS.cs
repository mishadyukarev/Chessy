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

            var unit_0 = UnitEs(idx_0).MainE.UnitTC;

            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            var ability = Es.MasterEs.AbilityC.Ability;

            if (UnitStatEs(idx_0).Hp.HaveMax)
            {
                if (UnitStatEs(idx_0).StepE.Have(ability))
                {
                    if (Es.InventorResourcesEs.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        Es.InventorResourcesEs.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        UnitEs(idx_0).MainE.Upgrade();
                        UnitStatEs(idx_0).StepE.Take(ability);

                        UnitStatEs(idx_0).Hp.SetMax();

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