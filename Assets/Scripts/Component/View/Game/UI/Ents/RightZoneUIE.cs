using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class RightZoneUIE : EntityAbstract
    {
        public ref GameObjectVC Zone => ref Ent.Get<GameObjectVC>();

        public RightZoneUIE(in EcsWorld gameW, in GameObject rightZone) : base(gameW)
        {
            Ent
                .Add(new GameObjectVC(rightZone));
        }
    }
}