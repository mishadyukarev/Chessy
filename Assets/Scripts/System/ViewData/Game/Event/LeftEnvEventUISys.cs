namespace Game.Game
{
    sealed class LeftEnvEventUISys : SystemUIAbstract
    {
        internal LeftEnvEventUISys(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftEs.EnvironmentEs.Info<ButtonUIC>().AddListener(EnvironmentInfo);
        }

        void EnvironmentInfo()
        {
            Es.InfoEnvironmentE.IsActiveC.Toggle();
        }
    }
}