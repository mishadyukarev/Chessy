using static Game.Game.CellEs;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct EnvCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in Idxs)
            {
                for (var env_0 = EnvironmentTypes.First; env_0 < EnvironmentTypes.End; env_0++)
                {
                    if (Environment<HaveEnvironmentC>(env_0, idx).Have)
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
