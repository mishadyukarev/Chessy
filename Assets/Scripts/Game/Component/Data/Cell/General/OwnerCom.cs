using System;

namespace Scripts.Game
{
    public struct OwnerCom
    {
        private PlayerTypes _owner;
        public PlayerTypes Owner => _owner;

        public bool Is(PlayerTypes playerType) => _owner == playerType;
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
