namespace Chessy.Game
{
    sealed class CenterKingUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterKingUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
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
