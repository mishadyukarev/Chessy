using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    struct KingZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            if (EntInventorUnits.Units<AmountC>(UnitTypes.King, LevelTypes.First, EntWhoseMove.CurPlayerI).Have)
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
