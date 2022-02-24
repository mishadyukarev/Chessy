namespace Game.Game
{
    internal sealed class LeftSmelterEventUIS : SystemUIAbstract
    {
        internal LeftSmelterEventUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftSmelterEs.TogglerE.ButtonUIC.AddListener(Toggle);
        }

        void Toggle()
        {
            E.IsActiveSmelter(E.SelectedIdxC.Idx) = !E.IsActiveSmelter(E.SelectedIdxC.Idx);
        }
    }
}