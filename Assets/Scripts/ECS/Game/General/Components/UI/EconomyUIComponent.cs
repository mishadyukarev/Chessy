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

    internal void SetText(ResourceTypes economyTypes, string text)
    {
        switch (economyTypes)
        {
            case ResourceTypes.None:
                throw new Exception();

            case ResourceTypes.Food:
                _foodText.text = text;
                break;

            case ResourceTypes.Wood:
                _woodText.text = text;
                break;

            case ResourceTypes.Ore:
                _oreText.text = text;
                break;

            case ResourceTypes.Iron:
                _ironText.text = text;
                break;

            case ResourceTypes.Gold:
                _goldText.text = text;
                break;

            default:
                break;
        }
    }

    internal void SetColor(ResourceTypes economyTypes, Color color)
    {
        switch (economyTypes)
        {
            case ResourceTypes.None:
                throw new Exception();

            case ResourceTypes.Food:
                _foodText.color = color;
                break;

            case ResourceTypes.Wood:
                _woodText.color = color;
                break;

            case ResourceTypes.Ore:
                _oreText.color = color;
                break;

            case ResourceTypes.Iron:
                _ironText.color = color;
                break;

            case ResourceTypes.Gold:
                _goldText.color = color;
                break;

            default:
                break;
        }
    }
}