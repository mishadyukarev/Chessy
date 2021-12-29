using UnityEngine;

namespace Game.Game
{
    public struct EconomyUpUIC
    {
        ResTypes _curRes;

        public Color Color
        {
            set => EntityUIPool.EconomyUp<TextUIC>(_curRes).Color = value;
        }
        public string Text
        {
            set => EntityUIPool.EconomyUp<TextUIC>(_curRes).Text = value;
        }

        internal EconomyUpUIC(in ResTypes res) => _curRes = res;
    }
}