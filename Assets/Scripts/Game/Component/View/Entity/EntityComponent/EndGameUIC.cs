using Game.Common;

namespace Game.Game
{
    public struct EndGameUIC
    {
        public void SetActiveZone(in bool isActive) => EntityUIPool.EndGameCenter<TextUIC>().SetActiveParent(isActive);

        public void SetText(in bool imWinner)
        {
            if (imWinner) EntityUIPool.EndGameCenter<TextUIC>().Text = LanguageC.GetText(GameLanguageTypes.YouAreWinner);
            else EntityUIPool.EndGameCenter<TextUIC>().Text = LanguageC.GetText(GameLanguageTypes.YouAreLoser);
        }
    }
}
