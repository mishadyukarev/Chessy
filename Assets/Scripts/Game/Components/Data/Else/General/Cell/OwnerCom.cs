using System;

namespace Scripts.Game
{
    public struct OwnerCom
    {
        private PlayerTypes _ownerType;
        public PlayerTypes Owner => _ownerType;
        public bool IsMine => Is(WhoseMoveC.CurPlayer);

        public bool Is(PlayerTypes playerType) => _ownerType == playerType;
        public void SetOwner(PlayerTypes playerType) => _ownerType = playerType;
        public void NoneOwner() => SetOwner(PlayerTypes.None);
        public void SyncOwner(PlayerTypes playerType) => _ownerType = playerType;
    }
}
