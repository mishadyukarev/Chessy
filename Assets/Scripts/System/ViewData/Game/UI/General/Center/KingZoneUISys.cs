using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    struct KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (InventorUnitsE.Units(UnitTypes.King, LevelTypes.First, Entities.WhoseMoveE.CurPlayerI).Have)
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
