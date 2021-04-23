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

        internal EnvironmentComponent(int x, int y, StartSpawnGameManager startSpawnManager)
        {
            _haveFood = default;
            _haveMountain = default;
            _haveTree = default;
            _haveHill = default;

            _foodGO = startSpawnManager.FoodsGO[x, y];
            _mountainGO = startSpawnManager.MountainsGO[x, y];
            _treeGO = startSpawnManager.TreesGO[x, y];
            _hillGO = startSpawnManager.HillsGO[x, y];
        }


        internal bool HaveFood => _haveFood;
        internal bool HaveMountain => _haveMountain;
        internal bool HaveTree => _haveTree;
        internal bool HaveHill => _haveHill;

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

    }



    public struct UnitComponent
    {
        private UnitTypes _unitType;
        private int _amountSteps;
        private int _amountHealth;
        private int _powerDamage;
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
            _isProtected = default;
            _isRelaxed = default;
            _player = default;
            _startValues = startValues;

            _unitPawnGO = startSpawnManager.UnitPawnsGO[x, y];
            _unitKingGO = startSpawnManager.UnitKingsGO[x, y];
            _unitPawnSpriteRender = startSpawnManager.UnitPawnsGOsr[x, y];
            _unitKingSpriteRender = startSpawnManager.UnitKingsGOsr[x, y];

        }


        internal UnitTypes UnitType => _unitType;
        internal int PowerDamage => _powerDamage;
        internal bool HaveAmountSteps => _amountSteps >= _startValues.AMOUNT_FOR_TAKE_UNIT;
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
        internal bool HaveUnit => UnitType != UnitTypes.None;
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
        internal int AmountSteps
        {
            get { return _amountSteps; }
            set { _amountSteps = value; }
        }
        internal int AmountHealth
        {
            get { return _amountHealth; }
            set { _amountHealth = value; }
        }

        private void SetColorUnit(in SpriteRenderer unitSpriteRender, in Player player)
        {
            if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
            else unitSpriteRender.color = Color.yellow;
        }
        internal void RefreshAmountSteps(int amountSteps = -1)
        {
            if (amountSteps == -1)
            {
                switch (_unitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        _amountSteps = _startValues.AMOUNT_STEPS_KING;
                        break;

                    case UnitTypes.Pawn:
                        _amountSteps = _startValues.AMOUNT_STEPS_PAWN;
                        break;

                    default:
                        break;
                }
            }

            else
            {
                switch (_unitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        _amountSteps = amountSteps;
                        break;

                    case UnitTypes.Pawn:
                        _amountSteps = amountSteps;
                        break;

                    default:
                        break;
                }
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



            switch (_unitType)
            {
                case UnitTypes.None:
                    _unitPawnGO.SetActive(false);
                    _unitKingGO.SetActive(false);
                    break;

                case UnitTypes.King:
                    _unitKingGO.SetActive(true);
                    SetColorUnit(_unitKingSpriteRender, _player);
                    break;

                case UnitTypes.Pawn:
                    _unitPawnGO.SetActive(true);
                    SetColorUnit(_unitPawnSpriteRender, _player);
                    break;

                default:
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
    }



    public struct BuildingComponent
    {
        private BuildingTypes _buildingType;
        private GameObject _campGO;

        internal BuildingComponent(int x, int y, StartSpawnGameManager startSpawnManager)
        {
            _buildingType = default;
            _campGO = startSpawnManager.CampsGO[x, y];
        }


        internal BuildingTypes BuildingType => _buildingType;
        internal bool HaveBuilding => _buildingType != BuildingTypes.None;

        internal void SetBuilding(in BuildingTypes buildingType)
        {
            _buildingType = buildingType;

            switch (buildingType)
            {
                case BuildingTypes.None:
                    _campGO.SetActive(false);
                    break;

                case BuildingTypes.City:
                    _campGO.SetActive(true);
                    break;

                default:
                    break;
            }
        }
        internal void ResetBuilding()
        {
            var buildingType = BuildingTypes.None;
            SetBuilding(buildingType);
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

    }
}
