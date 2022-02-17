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
                if (Es.FertilizeC(idx_0).HaveAny)
                {
                    Es.FertilizeC(idx_0).Resources -= 0.1f;
                }
            }
        }
    }
}