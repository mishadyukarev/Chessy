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
            E.BuildSmelterTC(E.SelectedIdxC.Idx) = !E.BuildSmelterTC(E.SelectedIdxC.Idx);
        }
    }
}