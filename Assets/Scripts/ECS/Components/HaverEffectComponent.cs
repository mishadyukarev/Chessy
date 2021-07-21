namespace Assets.Scripts.ECS.Components
{
    internal struct HaverEffectComponent
    {
        internal bool HaveEffect { get; set; }

        internal void StartFill(bool haveEffect = default) => HaveEffect = haveEffect;
    }
}
