using static Game.Game.CenterKingUIE;

namespace Game.Game
{
    sealed class KingZoneUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal KingZoneUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (Es.PlayerE(Es.CurPlayerI.Player).UnitsInfoE(UnitTypes.King).HaveInInventor)
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
