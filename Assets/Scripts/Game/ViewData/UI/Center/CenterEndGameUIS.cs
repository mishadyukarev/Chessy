using Chessy.Common;

namespace Chessy.Game
{
    sealed class CenterEndGameUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterEndGameUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            if (E.WinnerC.Player == default)
            {
                UIE.CenterEs.EndGame.SetActiveParent(false);
            }

            else if (E.WinnerC.Player != E.CurPlayerITC.Player)
            {
                UIE.CenterEs.EndGame.TextUI.text = LanguageC.GetText(GameLanguageTypes.YouAreLoser);
                UIE.CenterEs.EndGame.SetActiveParent(true);
            }
            else
            {

                UIE.CenterEs.EndGame.TextUI.text = LanguageC.GetText(GameLanguageTypes.YouAreWinner);

                UIE.CenterEs.EndGame.SetActiveParent(true);
            }
        }
    }
}