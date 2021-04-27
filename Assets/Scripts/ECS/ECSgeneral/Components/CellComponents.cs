using Photon.Realtime;
using UnityEngine;

public struct CellComponent
{
    private bool _isStartMaster;
    private bool _isStartOther;
    private bool _isSelected;
    private GameObject _cellGO;

    internal CellComponent(bool isStartMaster, bool isStartOther, GameObject cellGO)
    {
        _isStartMaster = isStartMaster;
        _isStartOther = isStartOther;
        _isSelected = default;

        _cellGO = cellGO;
    }


    internal bool IsStartMaster => _isStartMaster;
    internal bool IsStartOther => _isStartOther;
    internal int InstanceIDcell => _cellGO.GetInstanceID();
    internal bool IsSelected
    {
        get { return _isSelected; }
        set { _isSelected = value; }
    }
    internal Transform TransformCell => _cellGO.transform;



    public struct EnvironmentComponent
    {
        private bool _haveFood;
        private bool _haveMountain;
        private bool _haveTree;
        private bool _haveHill;
        private GameObject _foodGO;
        private GameObject _mountainGO;
        private GameObject _treeGO;
        private GameObject _hillGO;

        private StartValuesGameConfig _startValuesGameConfig;

        internal EnvironmentComponent(int x, int y, StartSpawnGameManager startSpawnManager, StartValuesGameConfig startValuesGameConfig)
        {
            _haveFood = default;
            _haveMountain = default;
            _haveTree = default;
            _haveHill = default;

            _foodGO = startSpawnManager.FoodsGO[x, y];
            _mountainGO = startSpawnManager.MountainsGO[x, y];
            _treeGO = startSpawnManager.TreesGO[x, y];
            _hillGO = startSpawnManager.HillsGO[x, y];

            _startValuesGameConfig = startValuesGameConfig;
        }


        #region Properties

        internal int PowerProtection
        {
            get
            {
                int powerProtection = 0;

                if (_haveTree) powerProtection += _startValuesGameConfig.PROTECTION_TREE;
                if (_haveHill) powerProtection += _startValuesGameConfig.PROTECTION_HILL;

                return powerProtection;
            }
        }

        internal int NeedAmountSteps
        {
            get
            {
                int amountSteps = 0;

                if (_haveTree) amountSteps += _startValuesGameConfig.NEED_AMOUNT_STEPS_TREE;
                if (_haveHill) amountSteps += _startValuesGameConfig.NEED_AMOUNT_STEPS_HILL;

                return amountSteps;
            }
        }

        internal bool HaveFood => _haveFood;
        internal bool HaveMountain => _haveMountain;
        internal bool HaveTree => _haveTree;
        internal bool HaveHill => _haveHill;

        #endregion


        #region Methods

        internal void SetResetEnvironment(bool isActive, EnvironmentTypes environmentType)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.Mountain:
                    _haveMountain = isActive;
                    _mountainGO.SetActive(isActive);
                    break;

                case EnvironmentTypes.Tree:
                    _haveTree = isActive;
                    _treeGO.SetActive(isActive);
                    break;

                case EnvironmentTypes.Hill:
                    _haveHill = isActive;
                    _hillGO.SetActive(isActive);
                    break;

                case EnvironmentTypes.Food:
                    _haveFood = isActive;
                    _foodGO.SetActive(isActive);
                    break;

                default:
                    break;
            }
        }

        #endregion
    }



    public struct UnitComponent
    {
        private UnitTypes _unitType;
        private int _amountSteps;
        private int _amountHealth;
        private int _powerDamage;
        private int _powerProtection;
        private bool _isProtected;
        private bool _isRelaxed;
        private Player _player;
        private GameObject _unitPawnGO;
        private GameObject _unitKingGO;
        private SpriteRenderer _unitPawnSpriteRender;
        private SpriteRenderer _unitKingSpriteRender;
        private StartValuesGameConfig _startValues;

        internal UnitComponent(int x, int y, StartSpawnGameManager startSpawnManager, StartValuesGameConfig startValues)
        {
            _unitType = default;
            _amountSteps = default;
            _amountHealth = default;
            _powerDamage = default;
            _powerProtection = default;
            _isProtected = default;
            _isRelaxed = default;
            _player = default;
            _startValues = startValues;

            _unitPawnGO = startSpawnManager.UnitPawnsGO[x, y];
            _unitKingGO = startSpawnManager.UnitKingsGO[x, y];
            _unitPawnSpriteRender = startSpawnManager.UnitPawnsGOsr[x, y];
            _unitKingSpriteRender = startSpawnManager.UnitKingsGOsr[x, y];

        }

        #region Properties

        internal UnitTypes UnitType => _unitType;
        internal bool HaveUnit => UnitType != UnitTypes.None;

        internal GameObject CurrentUnitGO
        {
            get
            {
                switch (_unitType)
                {
                    case UnitTypes.King:
                        return _unitKingGO;

                    case UnitTypes.Pawn:
                        return _unitPawnGO;

                    default:
                        return default;
                }
            }
        }

        internal int PowerDamage
        {
            get { return _powerDamage; }
            set { _powerDamage = value; }
        }
        internal int PowerProtection
        {
            get
            {
                if (_isProtected)
                {
                    switch (_unitType)
                    {
                        case UnitTypes.King:
                            return _startValues.PROTECTION_KING;

                        case UnitTypes.Pawn:
                            return _startValues.PROTECTION_PAWN;

                        default:
                            return default;
                    }
                }
                else return default;

            }
        }

        internal int AmountSteps
        {
            get { return _amountSteps; }
            set { _amountSteps = value; }
        }
        internal bool HaveMaxSteps
        {
            get
            {
                switch (_unitType)
                {
                    case UnitTypes.King:
                        return _amountSteps == _startValues.MAX_AMOUNT_STEPS_KING;
                    case UnitTypes.Pawn:
                        return _amountSteps == _startValues.MAX_AMOUNT_STEPS_PAWN;
                }
                return false;
            }
        }
        internal int MaxAmountSteps
        {
            get
            {
                switch (_unitType)
                {
                    case UnitTypes.King:
                        return _startValues.MAX_AMOUNT_STEPS_KING;
                    case UnitTypes.Pawn:
                        return _startValues.MAX_AMOUNT_STEPS_PAWN;
                }
                return default;
            }
        }
        internal bool MinAmountSteps => _amountSteps >= _startValues.MIN_AMOUNT_STEPS_FOR_UNIT;

        internal int AmountHealth
        {
            get { return _amountHealth; }
            set { _amountHealth = value; }
        }

        internal bool IsProtected
        {
            get { return _isProtected; }
            set { _isProtected = value; }
        }
        internal bool IsRelaxed
        {
            get { return _isRelaxed; }
            set { _isRelaxed = value; }
        }

        internal int ActorNumber
        {
            get
            {
                if (_player != default)
                    return _player.ActorNumber;
                else return -1;
            }
        }
        internal bool IsMine
        {
            get
            {
                if (_player != default)
                    return _player.IsLocal;
                else return false;
            }
        }

        #endregion


        #region Methods

        private void SetColorUnit(in SpriteRenderer unitSpriteRender, in Player player)
        {
            if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
            else unitSpriteRender.color = Color.red;
        }

        internal void RefreshAmountSteps()
        {
            switch (_unitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    _amountSteps = _startValues.MAX_AMOUNT_STEPS_KING;
                    break;

                case UnitTypes.Pawn:
                    _amountSteps = _startValues.MAX_AMOUNT_STEPS_PAWN;
                    break;

                default:
                    break;
            }
        }

        internal void ResetUnit()
        {
            UnitTypes unitType = default;
            int amountHealth = default;
            int powerDamage = default;
            int amountSteps = default;
            bool isProtected = default;
            bool isRelaxed = default;
            Player player = default;

            SetUnit(unitType, amountHealth, powerDamage, amountSteps, isProtected, isRelaxed, player);
        }
        internal void SetUnit(in UnitComponent cellUnitComponent)
        {
            var unitType = cellUnitComponent._unitType;
            var amountHealth = cellUnitComponent._amountHealth;
            var powerDamage = cellUnitComponent._powerDamage;
            var amountSteps = cellUnitComponent._amountSteps;
            var isProtected = cellUnitComponent._isProtected;
            var isRelaxed = cellUnitComponent._isRelaxed;
            var player = cellUnitComponent._player;

            SetUnit(unitType, amountHealth, powerDamage, amountSteps, isProtected, isRelaxed, player);
        }
        internal void SetUnit(in UnitTypes unitType, in int amountHealth, in int powerDamage, in int amountSteps, in bool isProtected, in bool isRelaxed, in Player player)
        {
            _unitType = unitType;
            _amountSteps = amountSteps;
            _amountHealth = amountHealth;
            _powerDamage = powerDamage;
            _isProtected = isProtected;
            _isRelaxed = isRelaxed;
            _player = player;



            _unitPawnGO.SetActive(false);
            _unitKingGO.SetActive(false);

            switch (_unitType)
            {
                case UnitTypes.King:
                    _unitKingGO.SetActive(true);
                    SetColorUnit(_unitKingSpriteRender, _player);
                    break;

                case UnitTypes.Pawn:
                    _unitPawnGO.SetActive(true);
                    SetColorUnit(_unitPawnSpriteRender, _player);
                    break;
            }
        }

        internal void ActiveVisionCell(bool isActive, UnitTypes unitType, Player player)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    _unitKingGO.SetActive(isActive);
                    SetColorUnit(_unitKingSpriteRender, player);
                    break;

                case UnitTypes.Pawn:
                    _unitPawnGO.SetActive(isActive);
                    SetColorUnit(_unitPawnSpriteRender, player);
                    break;

                default:
                    break;
            }
        }

        internal bool IsHim(Player player) => _player.ActorNumber == player.ActorNumber;

        #endregion
    }



    public struct BuildingComponent
    {
        private BuildingTypes _buildingType;
        private GameObject _cityGO;
        private GameObject _farmGO;
        private GameObject _woodcutterGO;
        private Player _player;

        private StartValuesGameConfig _startValuesGameConfig;

        internal BuildingComponent(int x, int y, StartSpawnGameManager startSpawnManager, StartValuesGameConfig startValuesGameConfig)
        {
            _buildingType = default;
            _cityGO = startSpawnManager.CampsGO[x, y];
            _farmGO = startSpawnManager.FarmsGO[x, y];
            _woodcutterGO = startSpawnManager.WoodcuttersGO[x, y];

            _startValuesGameConfig = startValuesGameConfig;

            _player = default;
        }

        #region Properties

        internal BuildingTypes BuildingType => _buildingType;
        internal bool HaveBuilding => _buildingType != BuildingTypes.None;
        internal int PowerProtection
        {
            get
            {
                switch (_buildingType)
                {
                    case BuildingTypes.City:
                        return _startValuesGameConfig.PROTECTION_CITY;

                    default:
                        return default;
                }
            }
        }

        internal int ActorNumber
        {
            get
            {
                if (_player != default)
                    return _player.ActorNumber;
                else return -1;
            }
        }
        internal bool IsMine
        {
            get
            {
                if (_player != default)
                    return _player.IsLocal;
                else return false;
            }
        }

        #endregion


        private void SetColorBuilding(in SpriteRenderer unitSpriteRender, in Player player)
        {
            if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
            else unitSpriteRender.color = Color.red;
        }

        internal void SetBuilding(in BuildingTypes buildingType, Player player)
        {
            _buildingType = buildingType;
            _player = player;


            _cityGO.SetActive(false);
            _farmGO.SetActive(false);
            _woodcutterGO.SetActive(false);

            switch (buildingType)
            {
                case BuildingTypes.City:
                    _cityGO.SetActive(true);
                    SetColorBuilding(_cityGO.GetComponent<SpriteRenderer>(), _player);
                    break;

                case BuildingTypes.Farm:
                    _farmGO.SetActive(true);
                    SetColorBuilding(_farmGO.GetComponent<SpriteRenderer>(), _player);
                    break;
                case BuildingTypes.Woodcutter:
                    _woodcutterGO.SetActive(true);
                    SetColorBuilding(_woodcutterGO.GetComponent<SpriteRenderer>(), _player);
                    break;
            }
        }
        internal void ResetBuilding()
        {
            var buildingType = BuildingTypes.None;
            Player player = default;
            SetBuilding(buildingType, player);
        }
        internal bool IsHim(Player player)
        {
            if (_player == default) return false;
            return _player.ActorNumber == player.ActorNumber;
        }
    }



    public struct SupportVisionComponent
    {
        private GameObject _selectorVisionGO;
        private GameObject _spawnVisionGO;
        private GameObject _wayUnitVisionGO;
        private GameObject _enemyVisionGO;

        internal SupportVisionComponent(int x, int y, StartSpawnGameManager startSpawnManager)
        {
            _selectorVisionGO = startSpawnManager.SelectorVisionsGO[x, y];
            _spawnVisionGO = startSpawnManager.SpawnVisionsGO[x, y];
            _wayUnitVisionGO = startSpawnManager.WayUnitVisionsGO[x, y];
            _enemyVisionGO = startSpawnManager.EnemyVisionsGO[x, y];
        }


        #region Methods

        internal void ActiveVision(bool isActive, SupportVisionTypes supportVisionType)
        {
            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    break;

                case SupportVisionTypes.Selector:
                    _selectorVisionGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.Spawn:
                    _spawnVisionGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.WayOfUnit:
                    _wayUnitVisionGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.Enemy:
                    _enemyVisionGO.SetActive(isActive);
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}
