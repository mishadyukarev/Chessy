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

            ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
            ref var levUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
            ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;


            ref var hpUnit_0 = ref Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health;


            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            if (Es.CellEs.UnitEs.StatEs.Hp(idx_0).HaveMax)
            {
                if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Have)
                {
                    if (Es.InventorResourcesEs.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        Es.InventorResourcesEs.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        Es.CellEs.UnitEs.Main(idx_0).LevelC.Level = LevelTypes.Second;
                        Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();

                        Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health.Amount = CellUnitHpValues.MAX_HP;

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