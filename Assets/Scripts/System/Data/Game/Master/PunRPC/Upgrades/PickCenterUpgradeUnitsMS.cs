namespace Game.Game
{
    struct PickCenterUpgradeUnitsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = Entities.MasterEs.UpgradeCenterUnit<UnitTC>().Unit;
            var whoseMove = Entities.WhoseMove.WhoseMove.Player;


            if (unit == UnitTypes.Scout)
            {
                Entities.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Steps, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
                Entities.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Steps, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
            }
            else
            {
                Entities.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
                Entities.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
            }

            Entities.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            Entities.AvailableCenterUpgradeEs.HaveUnitUpgrade(unit, whoseMove).HaveUpgrade.Have = false;

            Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}