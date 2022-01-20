namespace Game.Game
{
    struct CenterUpgradeUnitWaterMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var whoseMove = WhoseMoveE.WhoseMove<PlayerTC>().Player;

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>(UnitStatTypes.Water, unit, level, whoseMove, UpgradeTypes.PickCenter).Have = true;
                }
            }
            AvailableCenterUpgradeEs.HaveUpgrade<HaveUpgradeC>(whoseMove).Have = false;
            AvailableCenterUpgradeEs.HaveWaterUpgrade<HaveUpgradeC>(whoseMove).Have = false;

            EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}