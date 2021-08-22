namespace Assets.Scripts.ECS.Game.General.Components
{
    internal struct OwnerBotComponent
    {
        internal bool IsBot { get; set; }

        internal void Reset() => IsBot = default;
    }
}
