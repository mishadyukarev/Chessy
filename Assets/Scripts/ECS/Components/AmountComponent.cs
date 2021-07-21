internal struct AmountComponent
{
    internal int Amount { get; set; }

    internal bool HaveAmount => Amount > 0;

    internal void StartFill(int amount = default) => Amount = amount;

    internal void AddAmount(int adding = 1) => Amount += adding;
    internal void TakeAmount(int taking = 1) => Amount -= taking;
    internal void ResetAmount() => Amount = default;
}