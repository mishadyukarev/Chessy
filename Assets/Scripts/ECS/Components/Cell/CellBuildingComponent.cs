internal struct CellBuildingComponent
{
    internal int TimeStepsMine { get; set; }

    internal void StartFill()
    {
        TimeStepsMine = default;
    }
}
