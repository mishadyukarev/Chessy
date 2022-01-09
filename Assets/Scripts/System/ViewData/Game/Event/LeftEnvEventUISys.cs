namespace Game.Game
{
    sealed class LeftEnvEventUISys
    {
        internal LeftEnvEventUISys()
        {
            EnvirUIC.AddListenerToEnvInfo(EnvironmentInfo);
        }

        private void EnvironmentInfo()
        {
            EnvInfoC.IsActivatedInfo = !EnvInfoC.IsActivatedInfo;
        }
    }
}