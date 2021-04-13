using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal struct EconomyMasterComponent
{
    private int _goldMaster;
    private int _goldOther;

    internal int GoldMaster => _goldMaster;
    internal int GoldOther => _goldOther;


    internal EconomyMasterComponent(StartValuesConfig startValues)
    {
        _goldMaster = startValues.GoldMaster;
        _goldOther = startValues.GoldOther;
    }


    internal int AddGoldMaster(int addGold) => _goldMaster += addGold;
    internal int AddGoldOther(int addGold) => _goldOther += addGold;

    internal int TakeGoldMaster(int takeGold) => _goldMaster -= takeGold;
    internal int TakeGoldOther(int takeGold) => _goldOther -= takeGold;


    internal struct UnitsMasterComponent
    {
        private int _amountKingMaster;
        private int _amountKingOther;
        private int _amountUnitPawnMaster;
        private int _amountUnitPawnOther;

        internal int AmountKingMaster
        {
            get { return _amountKingMaster; }
            set { _amountKingMaster = value; }
        }
        internal int AmountKingOther
        {
            get { return _amountKingOther; }
            set { _amountKingOther = value; }
        }
        internal int AmountUnitPawnMaster
        {
            get { return _amountUnitPawnMaster; }
            set { _amountUnitPawnMaster = value; }
        }
        internal int AmountUnitPawnOther
        { 
            get { return _amountUnitPawnOther; }
            set { _amountUnitPawnOther= value; }
        }
                


        internal UnitsMasterComponent(StartValuesConfig startValues)
        {
            _amountKingMaster = startValues.AmountKingMaster;
            _amountKingOther = startValues.AmountKingOther;
            _amountUnitPawnMaster = startValues.AmountPawnMaster;
            _amountUnitPawnOther = startValues.AmountPawnOther;
        }
    }



    internal struct BuildingsMasterComponent
    {
        private bool _isSettedCityMaster;
        private bool _isSettedCityOther;
        private int[] _xySettedCityMaster;
        private int[] _xySettedCityOther;

        internal bool IsBuildedCityMaster
        { get { return _isSettedCityMaster; } set { _isSettedCityMaster = value; } }
        internal bool IsBuildedCityOther
        { get { return _isSettedCityOther; } set { _isSettedCityOther = value; } }
        internal int[] XYsettedCityMaster
        { get { return _xySettedCityMaster; } set { _xySettedCityMaster = value; } }
        internal int[] XYsettedCityOther
        { get { return _xySettedCityOther; } set { _xySettedCityOther = value; } }

        internal BuildingsMasterComponent(StartValuesConfig nameValueManager)
        {
            _isSettedCityMaster = default;
            _isSettedCityOther = default;
            _xySettedCityMaster = new int[nameValueManager.XY_FOR_ARRAY];
            _xySettedCityOther = new int[nameValueManager.XY_FOR_ARRAY];
        }
    }
}
