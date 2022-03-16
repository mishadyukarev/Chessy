namespace Chessy.Game
{
    sealed class CenterKingUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterKingUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            if (E.PlayerInfoE(E.CurPlayerITC.Player).HaveKingInInventor)
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
