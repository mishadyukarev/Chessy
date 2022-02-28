namespace Chessy.Game
{
    sealed class CenterKingUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterKingUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (E.UnitInfo(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.King).HaveInInventor)
            {
                UIEs.CenterEs.KingE.Paren.SetActive(true);
            }
            else
            {
                UIEs.CenterEs.KingE.Paren.SetActive(false);
            }
        }
    }
}
