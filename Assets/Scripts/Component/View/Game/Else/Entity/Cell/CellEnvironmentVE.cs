using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellEnvironmentVE : CellEntityAbstract
    {
        readonly EnvironmentTypes EnvT;
        public ref SpriteRendererVC SR => ref Ent.Get<SpriteRendererVC>();

        internal CellEnvironmentVE(in SpriteRenderer sr, in EnvironmentTypes envT, in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
            EnvT = envT;
            Ent.Add(new SpriteRendererVC(sr));
        }
    }
}