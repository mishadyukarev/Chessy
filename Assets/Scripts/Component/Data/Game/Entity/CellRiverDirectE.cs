using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellRiverDirectE : EntityAbstract
    {
        public ref HaveC HaveRiver => ref Ent.Get<HaveC>();

        public CellRiverDirectE(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}