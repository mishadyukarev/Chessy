
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
        internal int AmountKingMaster;
        internal int AmountKingOther;

        internal bool IsSettedKingMaster;
        internal bool IsSettedKingOther;

        internal int AmountUnitPawnMaster;
        internal int AmountUnitPawnOther;

        internal int AmountRookMaster;
        internal int AmountRookOther;

        internal int AmountBishopMaster;
        internal int AmountBishopOther;


        internal UnitsMasterComponent(StartValuesGameConfig startValues)
        {
            AmountKingMaster = startValues.AMOUNT_KING_MASTER;
            AmountKingOther = startValues.AMOUNT_KING_OTHER;

            AmountUnitPawnMaster = startValues.AMOUNT_PAWN_MASTER;
            AmountUnitPawnOther = startValues.AMOUNT_PAWN_OTHER;

            AmountRookMaster = startValues.AMOUNT_ROOK_MASTER;
            AmountRookOther = startValues.AMOUNT_ROOK_OTHER;

            AmountBishopMaster = startValues.AMOUNT_BISHOP_MASTER;
            AmountBishopOther = startValues.AMOUNT_BISHOP_OTHER;

            IsSettedKingMaster = false;
            IsSettedKingOther = false;
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
        private int _amountMineMaster;
        private int _amountMineOther;

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

            _amountMineMaster = 0;
            _amountMineOther = 0;
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
            get => _amountWoodcutterOther;
            set => _amountWoodcutterOther = value;
        }

        internal int AmountMineMaster
        {
            get { return _amountMineMaster; }
            set { _amountMineMaster = value; }
        }
        internal int AmountMineOther
        {
            get => _amountMineOther;
            set => _amountMineOther = value;
        }
    }
}
