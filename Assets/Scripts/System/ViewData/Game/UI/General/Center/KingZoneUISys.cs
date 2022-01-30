using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    sealed class KingZoneUISys : SystemViewAbstract, IEcsRunSystem
    {
        public KingZoneUISys(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            if (Es.InventorUnitsEs.Units(UnitTypes.King, LevelTypes.First, Es.WhoseMove.CurPlayerI).Units.Have)
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
