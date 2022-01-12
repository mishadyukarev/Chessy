namespace Game.Game
{
    public struct PlayerC : IUnitCellE, IBuildCell
    {
        public PlayerTypes Player { get; set; }

        public bool Is(params PlayerTypes[] owners)
        {
            foreach (var player in owners) if (player == Player) return true;
            return false;
        }


        internal void Set(PlayerC ownerC) => Player = ownerC.Player;
        internal void Reset() => Player = PlayerTypes.None;
    }
}
