using System.Collections.Generic;

public struct SupportVisionComponent
{
    private StartValuesConfig _nameValueManager;
    private CellManager _cellManager;
    private SystemsGeneralManager _systemsGeneralManager;


    private bool _isActiveVisionIN;
    private SupportVisionTypes _supportVisioTypeIN;
    private List<int[]> _xyAvailableCellsIN;
    public List<int[]> _xyAvailableCellsWithEnemyIN;
    private int[] _xyPreviousCellIN;
    private int[] _xySelectedCellIN;



    public SupportVisionComponent(SystemsGeneralManager systemsGeneralManager, StartValuesConfig nameValueManager, CellManager cellManager)
    {
        _isActiveVisionIN = default;
        _supportVisioTypeIN = default;

        _xyAvailableCellsIN = new List<int[]>();
        _xyAvailableCellsWithEnemyIN = new List<int[]>();
        _nameValueManager = nameValueManager;
        _cellManager = cellManager;
        _systemsGeneralManager = systemsGeneralManager;
        _xyPreviousCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _xySelectedCellIN = new int[nameValueManager.XY_FOR_ARRAY];
    }



    public void Unpack(out SupportVisionTypes supportVisionType, out bool isActive)
    {
        supportVisionType = _supportVisioTypeIN;
        isActive = _isActiveVisionIN;
    }


    #region

    public void ActiveSelectorVisionInvoke(bool isActive, int[] xyPreviousCell, int[] xySelectedCell)
    {
        _supportVisioTypeIN = SupportVisionTypes.Selector;

        _isActiveVisionIN = isActive;
        _cellManager.CopyXYinTo(xyPreviousCell, _xyPreviousCellIN);
        _cellManager.CopyXYinTo(xySelectedCell, _xySelectedCellIN);


        InvokeRunSystem();
    }

    public void UnpackSelector(out int[] xyPreviousCell, out int[] xySelectedCell)
    {
        xyPreviousCell = _xyPreviousCellIN;
        xySelectedCell = _xySelectedCellIN;
    }

    #endregion


    #region

    public void ActiveSpawnVisionInvoke(bool isActive)
    {
        _supportVisioTypeIN = SupportVisionTypes.Spawn;

        _isActiveVisionIN = isActive;

        InvokeRunSystem();
    }

    #endregion


    #region

    public void ActiveWayUnitVisionInvoke(bool isActive, List<int[]> xyAvailableCellsForShift)
    {
        _supportVisioTypeIN = SupportVisionTypes.WayOfUnit;

        _isActiveVisionIN = isActive;
        _cellManager.CopyListXYinTo(xyAvailableCellsForShift, _xyAvailableCellsIN);

        InvokeRunSystem();
    }

    public void UnpackWayUnitVision(out List<int[]> xyAvailableCellsForShift)
    {
        xyAvailableCellsForShift = _xyAvailableCellsIN;
    }

    #endregion


    #region

    public void ActiveEnemyVision(bool isActive, List<int[]> xyAvailableCellsWithEnemyIN)
    {
        _supportVisioTypeIN = SupportVisionTypes.Enemy;

        _cellManager.CopyListXYinTo(xyAvailableCellsWithEnemyIN, _xyAvailableCellsWithEnemyIN);

        InvokeRunSystem();
    }
    public void UnpackEnemyVision(out List<int[]> xyAvailableCellsWithEnemyIN)
    {
        xyAvailableCellsWithEnemyIN = _xyAvailableCellsWithEnemyIN;
    }

    #endregion


    private void InvokeRunSystem()
    {
        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Else, nameof(SupportVision2System));
    }
}
