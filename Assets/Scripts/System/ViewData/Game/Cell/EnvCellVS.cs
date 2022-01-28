namespace Game.Game
{
    struct EnvCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in Entities.CellEs.Idxs)
            {
                for (var env_0 = EnvironmentTypes.First; env_0 < EnvironmentTypes.End; env_0++)
                {
                    if (Entities.CellEs.EnvironmentEs.Environment(env_0, idx).Resources.Have)
                    {
                        CellEnvVEs.EnvCellVC<SpriteRendererVC>(env_0, idx).Enable();
                    }
                    else
                    {
                        CellEnvVEs.EnvCellVC<SpriteRendererVC>(env_0, idx).Disable();
                    }
                }
            }
        }
    }
}
