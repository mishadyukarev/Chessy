using ECS;
using UnityEngine.UI;

namespace Game.Game
{
    public sealed class LeftMarketButtonUIE : EntityAbstract
    {
        public ButtonUIC ButtonUIC => Ent.Get<ButtonUIC>();

        internal LeftMarketButtonUIE(in Button button, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new ButtonUIC(button));
        }
    }
}