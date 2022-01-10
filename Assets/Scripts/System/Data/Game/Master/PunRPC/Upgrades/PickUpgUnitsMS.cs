namespace Game.Game
{
    public sealed class PickUpgUnitsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);

            var whoseMove = WhoseMoveC.WhoseMove;

            if (unit == UnitTypes.Scout)
            {
                UnitUpgC.AddUpg(UpgTypes.PickCenter, UnitStatTypes.Steps, unit, LevelTypes.First, whoseMove);
                UnitUpgC.AddUpg(UpgTypes.PickCenter, UnitStatTypes.Steps, unit, LevelTypes.Second, whoseMove);
            }
            else
            {
                UnitUpgC.AddUpg(UpgTypes.PickCenter, UnitStatTypes.Damage, unit, LevelTypes.First, whoseMove);
                UnitUpgC.AddUpg(UpgTypes.PickCenter, UnitStatTypes.Damage, unit, LevelTypes.Second, whoseMove);
            }


            UnitAvailPickUpgC.Set(unit, whoseMove, false);
            PickUpgC.SetHaveUpgrade(whoseMove, false);
            EntityPool.Rpc<RpcC>().SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}