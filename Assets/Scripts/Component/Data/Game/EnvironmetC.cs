namespace Game.Game
{
    public struct EnvironmetC
    {
        public EnvironmentTypes Environment;

        public bool Is(params EnvironmentTypes[] envs)
        {
            if (envs == default) throw new System.Exception();

            foreach (var env in envs) if (env == Environment) return true;
            return false;
        }

        public EnvironmetC(in EnvironmentTypes env) => Environment = env;
    }
}