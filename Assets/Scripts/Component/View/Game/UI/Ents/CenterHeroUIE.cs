using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public sealed class CenterHeroUIE : EntityAbstract
    {
        public ref GameObjectVC Parent => ref Ent.Get<GameObjectVC>();
        public ref ButtonUIC ButtonC => ref Ent.Get<ButtonUIC>();

        internal CenterHeroUIE(in Transform heroT, in UnitTypes unit, in EcsWorld gameW) : base(gameW)
        {
            Ent
                .Add(new ButtonUIC(heroT.Find(unit.ToString()).Find("Button").GetComponent<Button>()))
                .Add(new GameObjectVC(heroT.gameObject));
        }
    }
}