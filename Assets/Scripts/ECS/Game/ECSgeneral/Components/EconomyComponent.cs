using System;
using System.Collections.Generic;

internal struct EconomyComponent
{
    private Dictionary<bool, int> _foodAmount;
    private Dictionary<bool, int> _woodAmount;
    private Dictionary<bool, int> _oreAmount;
    private Dictionary<bool, int> _ironAmount;
    private Dictionary<bool, int> _goldAmount;

    internal void CreateDict()
    {
        _foodAmount = new Dictionary<bool, int>();
        _woodAmount = new Dictionary<bool, int>();
        _oreAmount = new Dictionary<bool, int>();
        _ironAmount = new Dictionary<bool, int>();
        _goldAmount = new Dictionary<bool, int>();

        _foodAmount.Add(true, default);
        _foodAmount.Add(false, default);

        _woodAmount.Add(true, default);
        _woodAmount.Add(false, default);

        _oreAmount.Add(true, default);
        _oreAmount.Add(false, default);

        _ironAmount.Add(true, default);
        _ironAmount.Add(false, default);

        _goldAmount.Add(true, default);
        _goldAmount.Add(false, default);
    }

    internal int AmountResources(EconomyTypes economyType, bool key)
    {
        switch (economyType)
        {
            case EconomyTypes.None:
                throw new Exception();

            case EconomyTypes.Food:
                return _foodAmount[key];

            case EconomyTypes.Wood:
                return _woodAmount[key];

            case EconomyTypes.Ore:
                return _oreAmount[key];

            case EconomyTypes.Iron:
                return _ironAmount[key];

            case EconomyTypes.Gold:
                return _goldAmount[key];

            default:
                throw new Exception();
        }
    }

    internal void SetFood(bool key, int value) => _foodAmount[key] = value;
    internal void AddFood(bool key, int value) => _foodAmount[key] += value;
    internal void TakeFood(bool key, int value) => _foodAmount[key] -= value;

    internal void SetWood(bool key, int value) => _woodAmount[key] = value;
    internal void AddWood(bool key, int value) => _woodAmount[key] += value;
    internal void TakeWood(bool key, int value) => _woodAmount[key] -= value;

    internal int Ore(bool key) => _oreAmount[key];
    internal void SetOre(bool key, int value) => _oreAmount[key] = value;
    internal void AddOre(bool key, int value) => _oreAmount[key] += value;
    internal void TakeOre(bool key, int value) => _oreAmount[key] -= value;

    internal int Iron(bool key) => _ironAmount[key];
    internal void SetIron(bool key, int value) => _ironAmount[key] = value;
    internal void AddIron(bool key, int value) => _ironAmount[key] += value;
    internal void TakeIron(bool key, int value) => _ironAmount[key] -= value;

    internal int Gold(bool key) => _goldAmount[key];
    internal void SetGold(bool key, int value) => _goldAmount[key] = value;
    internal void AddGold(bool key, int value) => _goldAmount[key] += value;
    internal void TakeGold(bool key, int value) => _goldAmount[key] -= value;
}
