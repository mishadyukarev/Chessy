using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellIceWallEs : CellAbstE
    {
        public ref AmountC Hp => ref Ent.Get<AmountC>();
        public ref PlayerTC Owner => ref Ent.Get<PlayerTC>();

        public CellIceWallEs(in EcsWorld gameW, in byte idx) : base(gameW, idx) { }
    }
}