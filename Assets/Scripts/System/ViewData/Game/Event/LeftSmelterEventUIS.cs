namespace Game.Game
{
    internal sealed class LeftSmelterEventUIS : SystemUIAbstract
    {
        internal LeftSmelterEventUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftSmelterEs.TogglerE.ButtonUIC.AddListener(Toggle);
        }

        void Toggle()
        {
            Es.BuildingE(Es.SelectedIdxE.Idx).ToggleSmelter();
        }
    }
}