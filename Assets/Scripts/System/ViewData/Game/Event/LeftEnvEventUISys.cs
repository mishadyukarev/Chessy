namespace Game.Game
{
    sealed class LeftEnvEventUISys : SystemUIAbstract
    {
        internal LeftEnvEventUISys(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            UIEs.LeftEs.EnvironmentEs.Info<ButtonUIC>().AddListener(EnvironmentInfo);
        }

        void EnvironmentInfo()
        {
            Es.EnvIsActive = !Es.EnvIsActive;
        }
    }
}