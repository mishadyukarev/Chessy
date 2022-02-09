using ECS;
using UnityEngine.UI;

namespace Game.Game
{
    public sealed class LeftCityBuildUIE : EntityAbstract
    {
        public ref ButtonUIC ButtonCRef => ref Ent.Get<ButtonUIC>();

        internal LeftCityBuildUIE(in Button button, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new ButtonUIC(button));
        }
    }
}