using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class GameObjectPool
{
    #region Variables

    internal GameObject ParentScriptsGO;

    internal GameObject AudioSourceGO;


    #region Cell

    internal GameObject[,] CellsGO;

    internal GameObject[,] CellUnitPawnGOs;
    internal GameObject[,] CellUnitKingGOs;
    internal GameObject[,] CellUnitRookGOs;
    internal GameObject[,] CellUnitBishopGOs;

    internal GameObject[,] CellEnvironmentFoodGOs;
    internal GameObject[,] CellEnvironmentMountainGOs;
    internal GameObject[,] CellEnvironmentTreeGOs;
    internal GameObject[,] CellEnvironmentHillGOs;

    internal GameObject[,] CellSupportVisionSelectorGOs;
    internal GameObject[,] CellSupportVisionSpawnGOs;
    internal GameObject[,] CellSupportVisionWayUnitGOs;
    internal GameObject[,] CellSupportVisionEnemyGOs;
    internal GameObject[,] CellSupportVisionUniqueAttackGOs;
    internal GameObject[,] CellSupportVisionZoneGOs;

    internal GameObject[,] CellBuildingCityGOs;
    internal GameObject[,] CellBuildingFarmGOs;
    internal GameObject[,] CellBuildingWoodcutterGOs;
    internal GameObject[,] CellBuildingMineGOs;

    #endregion


    #region Canvas


    #region Left

    internal Image LeftImage;

    internal Button LeftImproveCityButton;
    internal Button LeftMeltButton;

    internal Button GameLeftBuyPawnButton;
    internal Button GameLeftBuyRookButton;
    internal Button GameLeftBuyBishopButton;

    #endregion


    #region Right

    internal Image RightUpImage;
    internal Image RightMiddleImage;
    internal Image RightDownImage;

    #region Down

    internal TextMeshProUGUI HpCurrentUnitText;
    internal TextMeshProUGUI DamageCurrentUnitText;
    internal TextMeshProUGUI ProtectionCurrentUnitText;
    internal TextMeshProUGUI StepsCurrentUnitText;


    internal Button BuildingAbilityButton0;

    internal Button BuildingAbilityButton1;
    internal Button BuildingAbilityButton2;
    internal Button BuildingAbilityButton3;

    internal Button BuildingAbilityButton4;


    internal Button UniqueAbilityButton1;
    internal Button UniqueAbilityButton2;
    internal Button UniqueAbilityButton3;

    internal Button StandartAbilityButton1;
    internal Button StandartAbilityButton2;

    #endregion

    #endregion


    #region Down

    internal Button GameDownTakeUnit0Button;
    internal Button GameDownTakeUnit1Button;
    internal Button GameDownTakeUnit2Button;
    internal Button GameDownTakeUnit3Button;

    internal Button DoneButton;

    internal RawImage DonerRawImage;

    #endregion


    #region Up

    internal TextMeshProUGUI GoldAmmountText;
    internal TextMeshProUGUI FoodAmmountText;
    internal TextMeshProUGUI WoodAmmountText;
    internal TextMeshProUGUI OreAmmountText;
    internal TextMeshProUGUI MetalAmmountText;

    internal Button ButtonLeave;

    #endregion


    #region Ready Zone

    internal RectTransform ParentReadyZone;

    internal Button ReadyButton;

    #endregion


    #region TheEndGame Zone

    internal RectTransform ParentTheEndGameZone;
    internal TextMeshProUGUI TheEndGameText;

    #endregion


    #endregion

    #endregion
}
