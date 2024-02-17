using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.View.UI.Entity;

namespace Chessy.View.UI.System
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
            if (aboutGameC.WinnerPlayerType == default)
            {
                eUI.CenterEs.EndGame.SetActiveParent(false);
            }

            else if (aboutGameC.WinnerPlayerType != aboutGameC.CurrentPlayerIType)
            {
                eUI.CenterEs.EndGame.TextUI.text = "You are loser";
                eUI.CenterEs.EndGame.SetActiveParent(true);
            }
            else
            {

                eUI.CenterEs.EndGame.TextUI.text = "You are winner";

                eUI.CenterEs.EndGame.SetActiveParent(true);
            }
        }
    }
}