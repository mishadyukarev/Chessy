internal struct AmountComponent
{
    private int _amount;

    internal int Amount => _amount;

    internal void StartFill() => _amount = default;

    internal void SetAmount(int amount) => _amount = amount;
    internal void AddAmount(int adding = 1) => _amount += adding;
    internal void TakeAmount(int taking = 1) => _amount -= taking;
}