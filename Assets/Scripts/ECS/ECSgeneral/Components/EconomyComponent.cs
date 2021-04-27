internal struct EconomyComponent
{
    private int _gold;
    private int _food;
    private int _wood;
    private int _ore;
    private int _iron;


    internal int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }
    internal int Food
    {
        get { return _food; }
        set { _food = value; }
    }
    internal int Wood
    {
        get { return _wood; }
        set { _wood = value; }
    }
    internal int Ore
    {
        get { return _ore; }
        set { _ore = value; }
    }
    internal int Iron
    {
        get { return _iron; }
        set { _iron = value; }
    }


    internal struct UnitComponent
    {
        private bool _isSettedKing;

        internal bool IsSettedKing
        {
            get { return _isSettedKing; }
            set { _isSettedKing = value; }
        }
    }


    internal struct BuildingComponent
    {
        private bool _isSettedCity;
        private int[] _xySettedCity;


        internal bool IsSettedCity
        {
            get { return _isSettedCity; }
            set { _isSettedCity = value; }
        }
        internal int[] XYsettedCity
        {
            get { return _xySettedCity; }
            set { _xySettedCity = value; }
        }
    }
}
