using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class RightUniqueZoneUIE : EntityAbtract
    {
        public ref GameObjectVC Zone => ref Ent.Get<GameObjectVC>();

        public RightUniqueZoneUIE(in EcsWorld gameW, in Transform uniqueZone) : base(gameW)
        {
            Ent.Add(new GameObjectVC(uniqueZone.gameObject));
        }
    }
}