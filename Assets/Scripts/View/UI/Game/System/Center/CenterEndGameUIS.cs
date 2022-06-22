using Chessy.Common;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class CenterEndGameUIS : SystemUIAbstract
    {
        readonly EntitiesViewUIGame eUI;

        internal CenterEndGameUIS( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }

        internal override void Sync()
        {
            if (_e.WinnerPlayerT == default)
            {
                eUI.CenterEs.EndGame.SetActiveParent(false);
            }

            else if (_e.WinnerPlayerT != _e.CurPlayerIT)
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