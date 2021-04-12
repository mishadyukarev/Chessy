using UnityEngine;

[CreateAssetMenu(menuName ="StartValues", fileName = "StartValues")]
public class StartValuesConfig : ScriptableObject
{
    [Space(12)]
    [SerializeField] internal int CellCountX = 15;
    [SerializeField] internal int CellCountY = 12;
    internal int X = 0;
    internal int Y = 1;
    internal int XYforArray = 2;


    [Space(12)]
    [Range(0, 100)] [SerializeField] internal int PercentTree = 30;
    [Range(0, 100)] [SerializeField] internal int PercentHill = 10;
    [Range(0, 100)] [SerializeField] internal int PercentMountain = 2;


    [Space(12)]
    [SerializeField] internal int GoldMaster = 100;
    [SerializeField] internal int GoldOther = 100;


    [Space(12)]
    [SerializeField] internal int GoldForBuyingPawn = 50;


    [Space(12)]
    [SerializeField] internal int AmountPawnMaster = 1;
    [SerializeField] internal int AmountPawnOther = 3;


    [Space(12)]
    [SerializeField] internal int AmountStepsPawn = 1;
    [SerializeField] internal int AmountHealthPawn = 100;
    [SerializeField] internal int PawerDamagePawn = 50;
    [SerializeField] internal int ProtectionPawn = 10;


    [Space(12)]
    [SerializeField] internal int ProtectionHill = 10;
    [SerializeField] internal int ProtectionTree = 20;
    [SerializeField] internal int ProtectionCity = 25;

    internal int TakeAmountSteps = 1;
    internal int TakeUnit = 1;

    internal int AmountForTakeUnit = 1;
    internal int AmountForDeath = 0;

    internal int NumberPhotonView = 1001;
}
