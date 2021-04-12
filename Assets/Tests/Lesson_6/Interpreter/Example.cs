using System;
using TMPro;
using UnityEngine;
namespace Asteroids.Interpreter
{
    internal sealed class Example : MonoBehaviour
    {
#pragma warning disable CS0649 // Полю "Example._text" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        [SerializeField] private TMP_Text _text;
#pragma warning restore CS0649 // Полю "Example._text" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
#pragma warning disable CS0649 // Полю "Example._inputField" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        [SerializeField] private TMP_InputField _inputField;
#pragma warning restore CS0649 // Полю "Example._inputField" нигде не присваивается значение, поэтому оно всегда будет иметь значение по умолчанию null.
        private void Start()
        {
            _inputField.onValueChanged.AddListener(Interpret);
        }
        private void Interpret(string value)
        {
            if (Int64.TryParse(value, out var number))
            {
                _text.text = ToRoman(number);
            }
        }
        private string ToRoman(long number)
        {
            if ((number < 0) || (number > 3999)) throw new
           ArgumentOutOfRangeException(nameof(number),
            "insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException(nameof(number));
        }
    }
}
