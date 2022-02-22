using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    sealed class KingZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal KingZoneUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (E.UnitInfo(E.CurPlayerI.Player, LevelTypes.First, UnitTypes.King).HaveInInventor)
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
