using Chessy.Common;

namespace Chessy.Game
{
    sealed class CenterEndGameUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterEndGameUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (E.WinnerC.Player == default)
            {
                UIEs.CenterEs.EndGame.SetActiveParent(false);
            }

            else if (E.WinnerC.Player != E.CurPlayerITC.Player)
            {
                UIEs.CenterEs.EndGame.TextUI.text = LanguageC.GetText(GameLanguageTypes.YouAreLoser);
                UIEs.CenterEs.EndGame.SetActiveParent(true);
            }
            else
            {

                UIEs.CenterEs.EndGame.TextUI.text = LanguageC.GetText(GameLanguageTypes.YouAreWinner);

                UIEs.CenterEs.EndGame.SetActiveParent(true);
            }
        }
    }
}