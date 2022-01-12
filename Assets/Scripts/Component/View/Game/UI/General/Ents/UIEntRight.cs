using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct UIEntRight
    {
        static Entity _zone;

        public static ref C Zone<C>() where C : struct => ref _zone.Get<C>();

        public UIEntRight(in EcsWorld gameW, in GameObject rightZone)
        {
            _zone = gameW.NewEntity()
                .Add(new GameObjectVC(rightZone));
        }
    }
}