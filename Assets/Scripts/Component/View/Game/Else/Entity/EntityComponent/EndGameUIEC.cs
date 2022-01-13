using Game.Common;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    public struct EndGameUIEC
    {
        public void SetActiveZone(in bool isActive) => EndGame<TextMPUGUIC>().SetActiveParent(isActive);

        public void SetText(in bool imWinner)
        {
            if (imWinner) EndGame<TextMPUGUIC>().Text = LanguageC.GetText(GameLanguageTypes.YouAreWinner);
            else EndGame<TextMPUGUIC>().Text = LanguageC.GetText(GameLanguageTypes.YouAreLoser);
        }
    }
}
