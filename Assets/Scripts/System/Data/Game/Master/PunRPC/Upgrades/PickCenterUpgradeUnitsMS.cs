namespace Game.Game
{
    struct PickCenterUpgradeUnitsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var unit = EntitiesMaster.UpgradeCenterUnit<UnitTC>().Unit;
            var whoseMove = Entities.WhoseMove.WhoseMove.Player;


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

            AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            AvailableCenterUpgradeEs.HaveUnitUpgrade(unit, whoseMove).HaveUpgrade.Have = false;

            Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}