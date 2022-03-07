namespace Chessy.Game
{
    sealed class CenterKingUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterKingUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            if (E.UnitInfoE(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.King).HaveInInventor)
            {
                UIE.CenterEs.KingE.Paren.SetActive(true);
            }
            else
            {
                UIE.CenterEs.KingE.Paren.SetActive(false);
            }
        }
    }
}
