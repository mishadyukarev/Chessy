using ECS;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class DirectWaveSnowyME : EntityAbstract
    {
        public ref IdxFromToC ForDirectWave => ref Ent.Get<IdxFromToC>();

        internal DirectWaveSnowyME(in EcsWorld world) : base(world)
        {
        }
    }
}