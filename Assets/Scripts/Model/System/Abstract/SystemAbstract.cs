using Chessy.Model.Component;
using Chessy.Model.Entity;
using Chessy.Model.Values;

namespace Chessy.Model
{
    public abstract class SystemAbstract
    {
        protected readonly EntitiesModel _e;

        protected readonly CellC[] _cellCs = new CellC[IndexCellsValues.CELLS];
        protected readonly PositionC[] _possitionCellCs = new PositionC[IndexCellsValues.CELLS];

        protected readonly UnitOnCellC[] _unitCs = new UnitOnCellC[IndexCellsValues.CELLS];
        protected readonly EffectsUnitC[] _effectsUnitCs = new EffectsUnitC[IndexCellsValues.CELLS];
        protected readonly ShiftingObjectC[] _shiftingUnitCs = new ShiftingObjectC[IndexCellsValues.CELLS];
        protected readonly HowManyDistanceNeedForShiftingUnitC[] _howManyDistanceNeedForShiftingUnitCs = new HowManyDistanceNeedForShiftingUnitC[IndexCellsValues.CELLS];
        protected readonly WhereUnitCanAttackToEnemyC[] _whereSimpleAttackCs = new WhereUnitCanAttackToEnemyC[IndexCellsValues.CELLS];
        protected readonly WhereUnitCanAttackToEnemyC[] _whereUniqueAttackCs = new WhereUnitCanAttackToEnemyC[IndexCellsValues.CELLS];
        protected readonly bool[][] _whereSimple = new bool[IndexCellsValues.CELLS][];
        protected readonly bool[][] _whereUnque = new bool[IndexCellsValues.CELLS][];
        protected readonly VisibleToOtherPlayerOrNotC[] _unitVisibleCs = new VisibleToOtherPlayerOrNotC[IndexCellsValues.CELLS];
        protected readonly WhereViewIdxCellC[] _unitWhereViewDataCs = new WhereViewIdxCellC[IndexCellsValues.CELLS];

        protected readonly CloudC[] _cloudCs = new CloudC[IndexCellsValues.CELLS];
        protected readonly WhereViewIdxCellC[] _cloudWhereViewDataCs = new WhereViewIdxCellC[IndexCellsValues.CELLS];


        #region Else

        protected readonly CommonInfoAboutGameC _aboutGameC;
        protected readonly InputC _inputC;
        protected readonly BookC _bookC;
        protected readonly SelectedBuildingsInTownC _selectedBuildingsInTownC;
        protected readonly SelectedToolWeaponC _selectedToolWeaponC;
        protected readonly SettingsC _settingsC;
        protected readonly ShopC _shopC;


        protected readonly WeatherE _weatherE;
        protected readonly SunC _sunC;
        protected readonly WindC _windC;



        #endregion

        protected SystemAbstract(in EntitiesModel eM)
        {
            _e = eM;

            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                var cellEs = _e.CellEs(cellIdx_0);

                var cellE = cellEs.CellE;
                var unitE = cellEs.UnitE;
                var cloudE = cellEs.CloudE;


                _cellCs[cellIdx_0] = cellE.CellC;
                _possitionCellCs[cellIdx_0] = cellE.PositionC;

                _unitCs[cellIdx_0] = unitE.MainC;
                _effectsUnitCs[cellIdx_0] = unitE.EffectsC;
                _shiftingUnitCs[cellIdx_0] = unitE.ShiftingInfoForUnitC;
                _howManyDistanceNeedForShiftingUnitCs[cellIdx_0] = unitE.HowManyDistanceNeedForShiftingUnitC;
                _whereSimpleAttackCs[cellIdx_0] = unitE.WhereCanAttackSimpleAttackToEnemyC;
                _whereUniqueAttackCs[cellIdx_0] = unitE.WhereCanAttackUniqueAttackToEnemyC;
                _whereSimple[cellIdx_0] = _whereSimpleAttackCs[cellIdx_0].WhereUnitCanAttack;
                _whereUnque[cellIdx_0] = _whereUniqueAttackCs[cellIdx_0].WhereUnitCanAttack;
                _unitVisibleCs[cellIdx_0] = unitE.VisibleToOtherPlayerOrNotC;
                _unitWhereViewDataCs[cellIdx_0] = unitE.WhereViewDataUnitC;

                _cloudCs[cellIdx_0] = cloudE.CloudC;
                _cloudWhereViewDataCs[cellIdx_0] = cloudE.WhereSkinAndWhereDataInfoC;
            }

            var commonInfoE = _e.CommonGameE;

            _aboutGameC = commonInfoE.CommonInfoAboutGameC;
            _inputC = commonInfoE.InputC;
            _bookC = commonInfoE.BookC;
            _selectedBuildingsInTownC = commonInfoE.SelectedBuildingsC;
            _selectedToolWeaponC = commonInfoE.SelectedToolWeaponC;
            _settingsC = commonInfoE.SettingsC;
            _shopC = commonInfoE.ShopC;

            _weatherE = _e.WeatherE;
            _sunC = _weatherE.SunC;
            _windC = _weatherE.WindC;


        }
    }
}