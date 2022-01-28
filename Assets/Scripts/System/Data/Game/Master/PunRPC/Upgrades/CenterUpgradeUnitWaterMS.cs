namespace Game.Game
{
    struct CenterUpgradeUnitWaterMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    Entities.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Water, unit, level, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
                }
            }
            Entities.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            Entities.AvailableCenterUpgradeEs.HaveWaterUpgrade(whoseMove).HaveUpgrade.Have = false;

            Entities.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}