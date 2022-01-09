namespace Game.Game
{
    sealed class KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (InvUnitsC.Have(UnitTypes.King, LevelTypes.First, WhoseMoveC.CurPlayerI))
            {
                KingZoneUIC.EnableZone();
            }
            else
            {
                KingZoneUIC.DisableZone();
            }
        }
    }
}
