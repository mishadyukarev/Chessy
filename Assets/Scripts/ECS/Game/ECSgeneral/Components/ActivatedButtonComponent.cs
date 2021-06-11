using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

internal struct ActivatedButtonComponent : IDisposable
{
    private Dictionary<bool, bool> _isActivatedDictionary;
    private Button _button;
    private TextMeshProUGUI _text;
    private Action _mistake;

    internal ActivatedButtonComponent(List<IDisposable> disposables)
    {
        _isActivatedDictionary = new Dictionary<bool, bool>();
        _isActivatedDictionary.Add(true, default);
        _isActivatedDictionary.Add(false, default);

        _button = default;
        _text = default;
        _mistake = default;

        disposables.Add(this);
    }

    internal void Fill(Button button, TextMeshProUGUI text)
    {
        _button = button;
        _text = text;
    }

    internal void SetMistake(Action action) => _mistake = action;
    internal void InvokeMistake() => _mistake();

    public void Dispose()
    {
        _isActivatedDictionary[true] = default;
        _isActivatedDictionary[false] = default;
        _button = default;
        _text = default;
        _mistake = default;
    }
}
