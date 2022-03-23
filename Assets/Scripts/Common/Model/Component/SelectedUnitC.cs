namespace Chessy.Game
{
    public struct SelectedUnitC
    {
        public UnitTypes UnitTC;
        public LevelTypes LevelTC;

        public SelectedUnitC(in UnitTypes unitT, LevelTypes levT)
        {
            UnitTC = unitT;
            LevelTC = levT;
        }

        public void Set(in UnitTypes unitT, LevelTypes levT)
        {
            UnitTC = unitT;
            LevelTC = levT;
        }
    }
}