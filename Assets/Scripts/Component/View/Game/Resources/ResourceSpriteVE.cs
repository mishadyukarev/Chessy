using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class ResourceSpriteVE : EntityAbstract
    {
        public ref SpriteVC SpriteC => ref Ent.Get<SpriteVC>();

        public ResourceSpriteVE(in EcsWorld gameW, in string name) : base(gameW)
        {
            Ent.Add(new SpriteVC(Resources.Load<Sprite>(name)));
        }
    }
}