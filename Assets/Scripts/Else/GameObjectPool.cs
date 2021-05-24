using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class GameObjectPool
{
    #region Variables

    internal GameObject ParentScriptsGO;

    internal GameObject AudioSourceGO;
    internal AudioSource AttackAudioSource;


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

    internal GameObject[,] CellEffectFireGOs;

    #endregion


    #region Canvas


    #region Left

    internal Image LeftImage;

    internal Button LeftMeltButton;

    internal Button InGameLeftUpgadePawnButton;
    internal Button InGameLeftUpgadeRookButton;
    internal Button InGameLeftUpgadeBishopButton;

    internal Button InGameLeftBuyPawnButton;
    internal Button InGameLeftBuyRookButton;
    internal Button InGameLeftBuyBishopButton;

    internal Button LeftUpgradeFarmButton;
    internal Button LeftUpgradeWoodcutterButton;
    internal Button LeftUpgradeMineButton;

    #endregion


    #region Right

    internal Image RightImage;


    internal TextMeshProUGUI HpCurrentUnitText;
    internal TextMeshProUGUI DamageCurrentUnitText;
    internal TextMeshProUGUI ProtectionCurrentUnitText;
    internal TextMeshProUGUI StepsCurrentUnitText;


    internal Button BuildingAbilityButton0;

    internal Button BuildingAbilityButton1;
    internal Button BuildingAbilityButton2;
    internal Button BuildingAbilityButton3;

    internal Button BuildingAbilityButton4;


    internal Button UniqueFirstAbilityButton;
    internal TextMeshProUGUI UniqueFirstAbilityText;

    internal Button UniqueSecondAbilityButton;
    internal TextMeshProUGUI UniqueSecondAbilityText;

    internal Button UniqueThirdAbilityButton;
    internal TextMeshProUGUI UniqueThirdAbilityText;

    internal Button StandartAbilityButton1;
    internal Button StandartAbilityButton2;

    #endregion


    #region Down

    internal Button GameDownTakerKingButton;
    internal Button GameDownTakerPawnButton;
    internal Button GameDownTakerRookButton;
    internal Button GameDownTakerBishopButton;

    internal Button DoneButton;

    internal Button TruceButton;

    #endregion


    #region Up

    internal TextMeshProUGUI GoldAmmountText;
    internal TextMeshProUGUI FoodAmmountText;
    internal TextMeshProUGUI WoodAmmountText;
    internal TextMeshProUGUI OreAmmountText;
    internal TextMeshProUGUI IronAmmountText;

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


    #region RefreshZone

    internal GameObject InGameRefreshZoneGO;

    internal TextMeshProUGUI InGameRefreshZoneRefreshText;
    internal Image InGameRefreshZoneRefreshImage;

    #endregion


    #endregion

    #endregion
}
