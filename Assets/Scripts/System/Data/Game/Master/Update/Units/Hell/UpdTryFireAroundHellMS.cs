using UnityEngine;

namespace Game.Game
{
    sealed class UpdTryFireAroundHellMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdTryFireAroundHellMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitE(idx_0).Is(UnitTypes.Hell))
                {
                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (Es.EnvironmentEs(idx_1).AdultForest.HaveEnvironment)
                        {
                            Es.EffectEs(idx_1).FireE.TryFireHell();
                        }
                    }
                }
            }
        }
    }
}