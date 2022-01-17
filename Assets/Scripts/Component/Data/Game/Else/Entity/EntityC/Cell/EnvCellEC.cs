using System;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;
using static Game.Game.CellTrailEs;

namespace Game.Game
{
    public readonly struct EnvCellEC : IEnvCell
    {
        readonly byte _idx;
        readonly EnvTypes _env;

        readonly EnvironmentValues _values;



        internal EnvCellEC(in byte idx, in EnvTypes env)
        {
            _idx = idx;
            _env = env;
            _values = new EnvironmentValues();
        }
    }
}