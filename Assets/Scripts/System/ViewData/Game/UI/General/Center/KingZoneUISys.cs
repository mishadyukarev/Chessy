using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    struct KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (InventorUnitsE.Units<AmountC>(UnitTypes.King, LevelTypes.First, WhoseMoveE.CurPlayerI).Have)
            {
                Button<ButtonUIC>().SetActiveParent(true);
            }
            else
            {
                Button<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}
