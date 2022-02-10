using ECS;
using UnityEngine;

namespace Game.Game
{
    public sealed class LeftRightZoneUIE : EntityAbstract
    {
        GameObjectVC Parent => Ent.Get<GameObjectVC>();

        internal LeftRightZoneUIE(in GameObject go, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new GameObjectVC(go));
        }

        public void SetActive(in bool needActive) => Parent.SetActive(needActive);
    }
}