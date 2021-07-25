namespace Assets.Scripts.ECS.Components
{
    internal struct HaveFireComponent
    {
        internal bool HaveFire { get; set; }

        internal void StartFill(bool haveFire = default) => HaveFire = haveFire;
    }
}
