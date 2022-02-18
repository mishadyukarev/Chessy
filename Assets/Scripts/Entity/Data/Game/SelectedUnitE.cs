namespace Game.Game
{
    public struct SelectedUnitE
    {
        public UnitTC UnitTC;
        public LevelTC LevelTC;

        public SelectedUnitE(in UnitTypes unitT, LevelTypes levT)
        {
            UnitTC.Unit = unitT;
            LevelTC.Level = levT;
        }
    }
}