using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct UpgradeUnitMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = Entities.MasterEs.UpgradeUnit<IdxC>().Idx;

            ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
            ref var levUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
            ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;


            ref var hpUnit_0 = ref Entities.CellEs.UnitEs.Hp(idx_0).AmountC;


            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            if (Entities.CellEs.UnitEs.Hp(idx_0).HaveMax)
            {
                if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Have)
                {
                    if (InventorResourcesE.CanUpgradeUnit(whoseMove, unit_0.Unit, out var needRes))
                    {
                        InventorResourcesE.BuyUpgradeUnit(whoseMove, unit_0.Unit);

                        Entities.CellEs.UnitEs.Else(idx_0).LevelC.Level = LevelTypes.Second;
                        Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();

                        Entities.CellEs.UnitEs.Hp(idx_0).AmountC.Amount = UnitHpValues.MAX_HP;

                        Entities.Rpc.SoundToGeneral(sender, ClipTypes.UpgradeMelee);
                    }
                    else
                    {
                        Entities.Rpc.MistakeEconomyToGeneral(sender, needRes);
                    }
                }
                else
                {
                    Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}