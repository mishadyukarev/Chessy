using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UIsystem : CellReductionSystem, IEcsInitSystem, IEcsRunSystem
{
    private TextMeshProUGUI _textMeshProUGUI;

    private Image _rightUpUnitImage;
    private Image _rightMiddleUnitImage;
    private Image _rightDownUnitImage;

    private Image _leftEconomyImage;

    private EcsComponentRef<EconomyComponent> _economyComponentRef = default;
    private EcsComponentRef<SelectorComponent> _selectorComponetRef = default;


    internal UIsystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _economyComponentRef = eCSmanager.EntitiesGeneralManager.EconomyComponentRef;
        _selectorComponetRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
    }


    public void Init()
    {
        _textMeshProUGUI = GameObject.Find("GoldAmmount").GetComponent<TextMeshProUGUI>();

        _rightUpUnitImage = GameObject.Find("RightUpUnitImage").GetComponent<Image>();
        _rightMiddleUnitImage = GameObject.Find("RightMiddleUnitImage").GetComponent<Image>();
        _rightDownUnitImage = GameObject.Find("RightDownUnitImage").GetComponent<Image>();
        _rightMiddleUnitImage.gameObject.SetActive(false);

        _leftEconomyImage = GameObject.Find("LeftEconomy").GetComponent<Image>();
    }

    public void Run()
    {
        _textMeshProUGUI.text = _economyComponentRef.Unref().Gold.ToString();

        var xySelectedCell = _selectorComponetRef.Unref().XYselectedCell;

        if (CellUnitComponent(xySelectedCell).IsMine)
        {
            switch (CellUnitComponent(xySelectedCell).UnitType)
            {
                case UnitTypes.None:
                    _rightDownUnitImage.gameObject.SetActive(false);
                    break;

                case UnitTypes.King:
                    break;

                case UnitTypes.Pawn:
                    _rightDownUnitImage.gameObject.SetActive(true);
                    break;

                default:
                    break;
            }
        }
        else
        {
            _rightDownUnitImage.gameObject.SetActive(false);
        }
        

        switch (CellBuildingComponent(xySelectedCell).BuildingType)
        {
            case BuildingTypes.None:
                _leftEconomyImage.gameObject.SetActive(false);
                break;

            case BuildingTypes.City:
                _leftEconomyImage.gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }
}
