using UnityEngine;

namespace Game.Game
{
    public struct GeneralZoneVEC : IGeneralZoneE
    {
        public void Attach(in Transform transForAttach)
            => transForAttach.transform.SetParent(EntityVPool.GeneralZone<GameObjectVC>().Transform);
    }
}
