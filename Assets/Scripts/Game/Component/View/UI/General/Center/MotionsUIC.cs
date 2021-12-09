namespace Game.Game
{
    public struct MotionsUIC
    {
        public string Text
        {
            set => EntityUIPool.MotionCenter<TextUIC>().Text = value;
        }

        public void SetActiveParent(in bool isActive) => EntityUIPool.MotionCenter<TextUIC>().Parent_G.SetActive(isActive);
    }
}
