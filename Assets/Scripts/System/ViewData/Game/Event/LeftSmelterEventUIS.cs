namespace Game.Game
{
    internal sealed class LeftSmelterEventUIS : SystemUIAbstract
    {
        internal LeftSmelterEventUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftSmelterEs.TogglerE.ButtonUIC.AddListener(Toggle);
        }

        void Toggle()
        {
            Es.BuildE(Es.SelectedIdxE.Idx).ToggleSmelter();
        }
    }
}