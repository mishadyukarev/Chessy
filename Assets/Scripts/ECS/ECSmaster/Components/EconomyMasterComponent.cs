
internal struct EconomyMasterComponent
{
    private int _goldMaster;
    private int _goldOther;

    internal EconomyMasterComponent(StartValuesConfig startValues)
    {
        _goldMaster = startValues.GoldMaster;
        _goldOther = startValues.GoldOther;
    }


    internal int GoldMaster
    {
        get { return _goldMaster; }
        set { _goldMaster = value; }
    }
    internal int GoldOther
    {
        get { return _goldOther; }
        set { _goldOther = value; }
    }


    internal struct UnitMasterComponent
    {
        private int _amountKingMaster;
        private int _amountKingOther;
        private int _amountUnitPawnMaster;
        private int _amountUnitPawnOther;
        private bool _isSettedKingMaster;
        private bool _isSettedKingOther;

        internal UnitMasterComponent(StartValuesConfig startValues)
        {
            _amountKingMaster = startValues.AMOUNT_KING_MASTER;
            _amountKingOther = startValues.AMOUNT_KING_OTHER;
            _amountUnitPawnMaster = startValues.AmountPawnMaster;
            _amountUnitPawnOther = startValues.AmountPawnOther;
            _isSettedKingMaster = default;
            _isSettedKingOther = default;
        }


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
            set { _amountUnitPawnOther = value; }
        }
        internal bool IsSettedKingMaster
        {
            get { return _isSettedKingMaster; }
            set { _isSettedKingMaster = value; }
        }
        internal bool IsSettedKingOther
        {
            get { return _isSettedKingOther; }
            set { _isSettedKingOther = value; }
        }
    }



    internal struct BuildingsMasterComponent
    {
        private bool _isSettedCityMaster;
        private bool _isSettedCityOther;
        private int[] _xySettedCityMaster;
        private int[] _xySettedCityOther;

        internal BuildingsMasterComponent(StartValuesConfig nameValueManager)
        {
            _isSettedCityMaster = default;
            _isSettedCityOther = default;
            _xySettedCityMaster = new int[nameValueManager.XY_FOR_ARRAY];
            _xySettedCityOther = new int[nameValueManager.XY_FOR_ARRAY];
        }


        internal bool IsBuildedCityMaster
        {
            get { return _isSettedCityMaster; }
            set { _isSettedCityMaster = value; }
        }
        internal bool IsBuildedCityOther
        {
            get { return _isSettedCityOther; }
            set { _isSettedCityOther = value; }
        }
        internal int[] XYsettedCityMaster
        {
            get { return _xySettedCityMaster; }
            set { _xySettedCityMaster = value; }
        }
        internal int[] XYsettedCityOther
        {
            get { return _xySettedCityOther; }
            set { _xySettedCityOther = value; }
        }
    }
}
