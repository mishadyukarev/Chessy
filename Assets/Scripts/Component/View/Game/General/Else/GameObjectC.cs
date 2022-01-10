using UnityEngine;

namespace Game.Game
{
    public struct GameObjectC : IBackgroundE, IGeneralZoneE
    {
        readonly GameObject _gO;

        public string Name => _gO.name;
        internal Transform Transform => _gO.transform;

        internal GameObjectC(in GameObject gO)
        {
            _gO = gO;
        }
    }
}