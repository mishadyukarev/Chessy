namespace Assets.Scripts.ECS.Components
{
    internal struct HaveFireComponent
    {
        internal bool HaveFire { get; set; }

        internal void Enable() => HaveFire = true;
        internal void Disable() => HaveFire = false;
    }
}
