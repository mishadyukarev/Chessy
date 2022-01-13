namespace Game.Game
{
    struct PickUpgUnitsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);

            //var whoseMove = WhoseMoveC.WhoseMove;

            //if (unit == UnitTypes.Scout)
            //{
            //    EntUnitUpgrades.Upgrade<HaveUpgradeC>(UnitStatTypes.Steps, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).Have = true;
            //    EntUnitUpgrades.Upgrade<HaveUpgradeC>(UnitStatTypes.Steps, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).Have = true;
            //}
            //else
            //{
            //    EntUnitUpgrades.Upgrade<HaveUpgradeC>(UnitStatTypes.Damage, unit, LevelTypes.First, whoseMove, UpgradeTypes.PickCenter).Have = true;
            //    EntUnitUpgrades.Upgrade<HaveUpgradeC>(UnitStatTypes.Damage, unit, LevelTypes.Second, whoseMove, UpgradeTypes.PickCenter).Have = true;
            //}


            ////UnitAvailPickUpgC.Set(unit, whoseMove, false);
            ////PickUpgC.SetHaveUpgrade(whoseMove, false);
            //EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}