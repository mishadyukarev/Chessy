using UnityEngine;

[CreateAssetMenu(menuName ="StartValues", fileName = "StartValues")]
public class StartValuesConfig : ScriptableObject
{
    [Space(12)]
    [SerializeField] internal int CellCountX = 15;
    [SerializeField] internal int CellCountY = 12;
    internal readonly int X = 0;
    internal readonly int Y = 1;
    internal readonly int XY_FOR_ARRAY = 2;


    [Space(12)]
    [Range(0, 100)] [SerializeField] internal int PercentFood = 10;
    [Range(0, 100)] [SerializeField] internal int PercentTree = 20;
    [Range(0, 100)] [SerializeField] internal int PercentHill = 5;
    [Range(0, 100)] [SerializeField] internal int PercentMountain = 2;


    [Space(12)]
    [SerializeField] internal int GoldMaster = 100;
    [SerializeField] internal int GoldOther = 100;


    [Space(12)]
    [SerializeField] internal int GoldForBuyingPawn = 50;


    [Space(12)]
    [SerializeField] internal int AmountPawnMaster = 1;
    [SerializeField] internal int AmountPawnOther = 3;

    internal readonly int AMOUNT_KING_MASTER = 1;
    internal readonly int AMOUNT_KING_OTHER = 1;

    internal readonly int AMOUNT_STEPS_PAWN = 1;
    internal readonly int AMOUNT_STEPS_KING = 2;

    internal readonly int TAKE_AMOUNT_STEPS = 1;
    internal readonly int TAKE_UNIT = 1;

    internal readonly int AMOUNT_FOR_TAKE_UNIT = 1;
    internal readonly int AMOUNT_FOR_DEATH = 0;



    [Space(12)]
    [SerializeField] internal int AmountHealthPawn = 100;
    [SerializeField] internal int PowerDamagePawn = 50;
    [SerializeField] internal int ProtectionPawn = 10;

    [Space(12)]
    [SerializeField] internal int AmountHealthKing = 200;
    [SerializeField] internal int PowerDamageKing = 70;
    [SerializeField] internal int ProtectionKing = 10;

    [Space(12)]
    [SerializeField] internal int ProtectionHill = 10;
    [SerializeField] internal int ProtectionTree = 20;
    [SerializeField] internal int ProtectionCity = 25;


    internal readonly int NUMBER_PHOTON_VIEW = 1001;
}
