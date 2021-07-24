internal struct AmountComponent
{
    internal int Amount { get; set; }

    internal void StartFill(int amount = default) => Amount = amount;
}