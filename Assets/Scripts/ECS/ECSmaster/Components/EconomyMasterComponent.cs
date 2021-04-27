
internal struct EconomyMasterComponent
{
    private int _goldMaster;
    private int _goldOther;
    private int _foodMaster;
    private int _foodOther;
    private int _woodMaster;
    private int _woodOther;
    private int _oreMaster;
    private int _oreOther;
    private int _ironMaster;
    private int _ironOther;

    internal EconomyMasterComponent(StartValuesGameConfig startValuesGameConfig)
    {
        _goldMaster = startValuesGameConfig.AMOUNT_GOLD_MASTER;
        _goldOther = startValuesGameConfig.AMOUNT_GOLD_OTHER;

        _foodMaster = startValuesGameConfig.AMOUNT_FOOD_MASTER;
        _foodOther = startValuesGameConfig.AMOUNT_FOOD_OTHER;

        _woodMaster = startValuesGameConfig.AMOUNT_WOOD_MASTER;
        _woodOther = startValuesGameConfig.AMOUNT_WOOD_OTHER;

        _oreMaster = startValuesGameConfig.AMOUNT_ORE_MASTER;
        _oreOther = startValuesGameConfig.AMOUNT_ORE_OTHER;

        _ironMaster = startValuesGameConfig.AMOUNT_IRON_MASTER;
        _ironOther = startValuesGameConfig.AMOUNT_IRON_OTHER;
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

    internal int FoodMaster
    {
        get { return _foodMaster; }
        set { _foodMaster = value; }
    }
    internal int FoodOther
    {
        get { return _foodOther; }
        set { _foodOther = value; }
    }

    internal int WoodMaster
    {
        get { return _woodMaster; }
        set { _woodMaster = value; }
    }
    internal int WoodOther
    {
        get { return _woodOther; }
        set { _woodOther = value; }
    }

    internal int OreMaster
    {
        get { return _oreMaster; }
        set { _oreMaster = value; }
    }
    internal int OreOther
    {
        get { return _oreOther; }
        set { _oreOther = value; }
    }

    internal int IronMaster
    {
        get { return _ironMaster; }
        set { _ironMaster = value; }
    }
    internal int IronOther
    {
        get { return _ironOther; }
        set { _ironOther = value; }
    }



    internal struct UnitsMasterComponent
    {
        private int _amountKingMaster;
        private int _amountKingOther;
        private int _amountUnitPawnMaster;
        private int _amountUnitPawnOther;
        private bool _isSettedKingMaster;
        private bool _isSettedKingOther;

        internal UnitsMasterComponent(StartValuesGameConfig startValues)
        {
            _amountKingMaster = startValues.AMOUNT_KING_MASTER;
            _amountKingOther = startValues.AMOUNT_KING_OTHER;
            _amountUnitPawnMaster = startValues.AMOUNT_PAWN_MASTER;
            _amountUnitPawnOther = startValues.AMOUNT_PAWN_OTHER;
            _isSettedKingMaster = false;
            _isSettedKingOther = false;
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
        private int _amountFarmsMaster;
        private int _amountFarmsOther;
        private int _amountWoodcutterMaster;
        private int _amountWoodcutterOther;

        internal BuildingsMasterComponent(StartValuesGameConfig nameValueManager)
        {
            _isSettedCityMaster = default;
            _isSettedCityOther = default;
            _xySettedCityMaster = new int[nameValueManager.XY_FOR_ARRAY];
            _xySettedCityOther = new int[nameValueManager.XY_FOR_ARRAY];

            _amountFarmsMaster = 0; // !!!!!!!!!!!!!!
            _amountFarmsOther = 0; // !!!!!!!!!!!!!

            _amountWoodcutterMaster = 0;
            _amountWoodcutterOther = 0;
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

        internal int AmountFarmMaster
        {
            get { return _amountFarmsMaster; }
            set { _amountFarmsMaster = value; }
        }
        internal int AmountFarmOther
        {
            get { return _amountFarmsOther; }
            set { _amountFarmsOther = value; }
        }

        internal int AmountWoodcutterMaster
        {
            get { return _amountWoodcutterMaster; }
            set { _amountWoodcutterMaster = value; }
        }
        internal int AmountWoodcutterOther
        {
            get { return _amountWoodcutterOther; }
            set { _amountWoodcutterOther = value; }
        }
    }
}
