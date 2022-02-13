using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    sealed class UpdDryFertilizerMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdDryFertilizerMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.FertilizeE(idx_0).HaveEnvironment)
                {
                    Es.FertilizeE(idx_0).TakeDry();
                }
            }
        }
    }
}