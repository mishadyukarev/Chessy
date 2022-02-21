using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    sealed class DeleteTrailsS : SystemAbstract, IEcsRunSystem
    {
        internal DeleteTrailsS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (!E.AdultForestC(idx_0).HaveAny)
                {
                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        if (E.CellEs(idx_0).TrailHealthC(dirT).IsAlive)
                        {
                            E.CellEs(idx_0).TrailHealthC(dirT).Health = 0;
                        }
                    }
                }
            }
        }
    }
}