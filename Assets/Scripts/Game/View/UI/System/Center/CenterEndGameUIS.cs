using Chessy.Common;
using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    sealed class CenterEndGameUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly EntitiesViewUIGame eUI;

        internal CenterEndGameUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
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