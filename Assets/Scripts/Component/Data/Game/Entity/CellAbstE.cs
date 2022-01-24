using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public abstract class CellAbstE : EntityAbtract
    {
        protected readonly byte Idx;

        public CellAbstE(in EcsWorld gameW, in byte idx) : base(gameW)
        {
            Idx = idx;
        }
    }
}