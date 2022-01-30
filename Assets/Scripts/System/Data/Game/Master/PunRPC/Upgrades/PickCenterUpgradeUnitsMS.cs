namespace Game.Game
{
    sealed class PickCenterUpgradeUnitsMS : SystemAbstract, IEcsRunSystem
    {
        public PickCenterUpgradeUnitsMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = Es.MasterEs.UpgradeCenterUnit<UnitTC>().Unit;
            var whoseMove = Es.WhoseMove.WhoseMove.Player;


            if (unit == UnitTypes.Scout)
            {
                Es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Steps, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
                Es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Steps, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
            }
            else
            {
                Es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
                Es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Damage, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
            }

            Es.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            Es.AvailableCenterUpgradeEs.HaveUnitUpgrade(unit, whoseMove).HaveUpgrade.Have = false;

            Es.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}