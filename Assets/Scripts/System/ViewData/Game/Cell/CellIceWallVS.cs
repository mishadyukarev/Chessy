using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct CellIceWallVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                if (CellIceWallEs.Hp(idx_0).Have)
                {

                }

                CellIceWallVEs.SR(idx_0).Enabled = CellIceWallEs.Hp(idx_0).Have;
            }
        }
    }
}