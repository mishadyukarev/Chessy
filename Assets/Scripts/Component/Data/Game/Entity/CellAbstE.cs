using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public abstract class CellAbstE : EntityAbstract
    {
        protected readonly byte Idx;

        public CellAbstE(in EcsWorld gameW, in byte idx) : base(gameW)
        {
            Idx = idx;
        }
    }
}