namespace Game.Game
{
    public struct OwnerC : IUnitCell, IBuildCell
    {
        public PlayerTypes Owner { get; internal set; }

        public bool Is(params PlayerTypes[] owners)
        {
            foreach (var player in owners) if (player == Owner) return true;
            return false;
        }


        internal void Set(OwnerC ownerC) => Owner = ownerC.Owner;
        internal void Reset() => Owner = PlayerTypes.None;
    }
}
