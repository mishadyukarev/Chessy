namespace Game.Game
{
    struct WaterUpgMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            //var whoseMove = WhoseMoveC.WhoseMove;


            //for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            //{
            //    for (var level = LevelTypes.First; level < LevelTypes.End; level++)
            //    {
            //        //EntUnitUpgrades.Upgrade<HaveUpgradeC>(UpgradeTypes.PickCenter, UnitStatTypes.Water, unit, level, whoseMove).Have = true;
            //    }
            //}

            ////WaterAvailPickUpgC.Set(whoseMove, false);
            ////PickUpgC.SetHaveUpgrade(whoseMove, false);
            //EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}