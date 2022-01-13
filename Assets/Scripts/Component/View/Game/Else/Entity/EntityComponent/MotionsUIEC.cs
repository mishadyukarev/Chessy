using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    public struct MotionsUIEC
    {
        public string Text
        {
            set => Motion<TextMPUGUIC>().Text = value;
        }

        public void SetActiveParent(in bool isActive) => Motion<TextMPUGUIC>().Parent_G.SetActive(isActive);
    }
}
