using System;

namespace Chessy.Game
{
    public struct OwnerC
    {
        private PlayerTypes _owner;
        public PlayerTypes Owner => _owner;

        public bool Is(params PlayerTypes[] players)
        {
            foreach (var player in players) if (player == _owner) return true;
            return false;
        }


        public void SetOwner(PlayerTypes playerType)
        {
            if (playerType == default) throw new Exception();

            _owner = playerType;
        }
        public void DefOwner()
        {
            if (_owner == default) throw new Exception();

            _owner = default;
        }
        public void Sync(PlayerTypes playerType) => _owner = playerType;
    }
}
