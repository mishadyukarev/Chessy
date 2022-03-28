using Chessy.Common;

namespace Chessy.Game
{
    sealed class CenterEndGameUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterEndGameUIS( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            if (e.WinnerC.Player == default)
            {
                eUI.CenterEs.EndGame.SetActiveParent(false);
            }

            else if (e.WinnerC.Player != e.CurPlayerITC.Player)
            {
                eUI.CenterEs.EndGame.TextUI.text = LanguageC.GetText(GameLanguageTypes.YouAreLoser);
                eUI.CenterEs.EndGame.SetActiveParent(true);
            }
            else
            {

                eUI.CenterEs.EndGame.TextUI.text = LanguageC.GetText(GameLanguageTypes.YouAreWinner);

                eUI.CenterEs.EndGame.SetActiveParent(true);
            }
        }
    }
}