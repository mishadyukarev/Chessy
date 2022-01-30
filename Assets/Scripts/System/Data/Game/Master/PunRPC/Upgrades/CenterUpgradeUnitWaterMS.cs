namespace Game.Game
{
    sealed class CenterUpgradeUnitWaterMS : SystemAbstract, IEcsRunSystem
    {
        public CenterUpgradeUnitWaterMS(in Entities ents) : base(ents) { }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    Es.UnitStatUpgradesEs.Upgrade(UnitStatTypes.Water, unit, level, whoseMove, UpgradeTypes.PickCenter).HaveUpgrade.Have = true;
                }
            }
            Es.AvailableCenterUpgradeEs.HaveUpgrade(whoseMove).HaveUpgrade.Have = false;
            Es.AvailableCenterUpgradeEs.HaveWaterUpgrade(whoseMove).HaveUpgrade.Have = false;

            Es.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}