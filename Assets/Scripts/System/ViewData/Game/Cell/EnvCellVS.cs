using static Game.Game.CellE;
using static Game.Game.CellEnvironmentE;

namespace Game.Game
{
    struct EnvCellVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx in Idxs)
            {
                for (var env_0 = EnvTypes.First; env_0 < EnvTypes.End; env_0++)
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
