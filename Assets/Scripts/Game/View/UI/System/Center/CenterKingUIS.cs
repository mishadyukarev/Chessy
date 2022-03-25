namespace Chessy.Game
{
    sealed class CenterKingUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterKingUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            if (eMGame.PlayerInfoE(eMGame.CurPlayerITC.Player).HaveKingInInventor)
            {
                eUI.CenterEs.KingE.Paren.SetActive(true);
            }
            else
            {
                eUI.CenterEs.KingE.Paren.SetActive(false);
            }
        }
    }
}
