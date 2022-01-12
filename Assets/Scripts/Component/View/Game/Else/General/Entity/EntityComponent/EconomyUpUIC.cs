using UnityEngine;
using static Game.Game.EntityUpUIPool;

namespace Game.Game
{
    public readonly struct EconomyUpUIC
    {
        readonly ResTypes _curRes;

        public Color Color
        {
            set => Economy<TextUIC>(_curRes).Color = value;
        }
        public string Text
        {
            set => Economy<TextUIC>(_curRes).Text = value;
        }

        internal EconomyUpUIC(in ResTypes res) => _curRes = res;
    }
}