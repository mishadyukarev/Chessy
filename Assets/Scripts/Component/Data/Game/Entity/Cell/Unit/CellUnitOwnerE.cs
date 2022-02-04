using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitOwnerE : CellEntityAbstract
    {
        ref PlayerTC OwnerCRef => ref Ent.Get<PlayerTC>();
        public PlayerTC OwnerC => Ent.Get<PlayerTC>();

        internal CellUnitOwnerE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }

        public void Set(in PlayerTypes player) => OwnerCRef.Player = player;
    }
}