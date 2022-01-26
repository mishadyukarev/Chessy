namespace Game.Game
{
    struct PickCenterUpgradeUnitsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = EntityMPool.UpgradeCenterUnit<UnitTC>().Unit;
            var whoseMove = Entities.WhoseMoveE.WhoseMove.Player;


            if (unit == UnitTypes.Scout)
            {
                UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>(UnitStatTypes.Steps, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).Have = true;
                UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>(UnitStatTypes.Steps, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).Have = true;
            }
            else
            {
                UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>(UnitStatTypes.Damage, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).Have = true;
                UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>(UnitStatTypes.Damage, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).Have = true;
            }

            AvailableCenterUpgradeEs.HaveUpgrade<HaveUpgradeC>(whoseMove).Have = false;
            AvailableCenterUpgradeEs.HaveUnitUpgrade<HaveUpgradeC>(unit, whoseMove).Have = false;

            EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}