using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    sealed class KingZoneUISys : SystemUIAbstract, IEcsRunSystem
    {
        internal KingZoneUISys(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (Es.InventorUnitsEs.Units(UnitTypes.King, LevelTypes.First, Es.WhoseMoveE.CurPlayerI).HaveUnits)
            {
                Paren.SetActive(true);
            }
            else
            {
                Paren.SetActive(false);
            }
        }
    }
}
