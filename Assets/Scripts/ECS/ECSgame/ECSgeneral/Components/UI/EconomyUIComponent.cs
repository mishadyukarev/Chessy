using System;
using TMPro;
using UnityEngine;

internal struct EconomyUIComponent
{
    private TextMeshProUGUI _foodText;
    private TextMeshProUGUI _woodText;
    private TextMeshProUGUI _oreText;
    private TextMeshProUGUI _ironText;
    private TextMeshProUGUI _goldText;


    internal void Fill()
    {
        _foodText = GameObject.Find("FoodAmount").GetComponent<TextMeshProUGUI>();
        _woodText = GameObject.Find("WoodAmount").GetComponent<TextMeshProUGUI>();
        _oreText = GameObject.Find("OreAmount").GetComponent<TextMeshProUGUI>();
        _ironText = GameObject.Find("MetalAmount").GetComponent<TextMeshProUGUI>();
        _goldText = GameObject.Find("GoldAmount").GetComponent<TextMeshProUGUI>();
    }

    internal void SetText(EconomyTypes economyTypes, string text)
    {
        switch (economyTypes)
        {
            case EconomyTypes.None:
                throw new Exception();

            case EconomyTypes.Food:
                _foodText.text = text;
                break;

            case EconomyTypes.Wood:
                _woodText.text = text;
                break;

            case EconomyTypes.Ore:
                _oreText.text = text;
                break;

            case EconomyTypes.Iron:
                _ironText.text = text;
                break;

            case EconomyTypes.Gold:
                _goldText.text = text;
                break;

            default:
                break;
        }
    }

    internal void SetColor(EconomyTypes economyTypes, Color color)
    {
        switch (economyTypes)
        {
            case EconomyTypes.None:
                throw new Exception();

            case EconomyTypes.Food:
                _foodText.color = color;
                break;

            case EconomyTypes.Wood:
                _woodText.color = color;
                break;

            case EconomyTypes.Ore:
                _oreText.color = color;
                break;

            case EconomyTypes.Iron:
                _ironText.color = color;
                break;

            case EconomyTypes.Gold:
                _goldText.color = color;
                break;

            default:
                break;
        }
    }
}