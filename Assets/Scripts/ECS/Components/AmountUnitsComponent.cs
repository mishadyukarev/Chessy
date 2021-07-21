namespace Assets.Scripts.ECS.Components
{
    internal class AmountUnitsComponent
    {
        internal int AmountUnits { get; set; }

        internal void StartFill(int amountUnits = default) => AmountUnits = amountUnits;


    }
}
