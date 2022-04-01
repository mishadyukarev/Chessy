using System;

namespace Chessy.Game
{
    public struct EnvironmetTC
    {
        public EnvironmentTypes Environment { get; internal set; }

        public bool Is(params EnvironmentTypes[] envs)
        {
            if (envs == default) throw new Exception();

            foreach (var env in envs) if (env == Environment) return true;
            return false;
        }
    }
}