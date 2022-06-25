using Chessy.Common;
using Chessy.Model;

namespace Chessy.Model
{
    sealed class CenterEndGameUIS : SystemUIAbstract
    {
        readonly EntitiesViewUI eUI;

        internal CenterEndGameUIS(in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
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