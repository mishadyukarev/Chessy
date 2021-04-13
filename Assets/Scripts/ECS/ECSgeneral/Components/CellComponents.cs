using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static Main;

public struct CellComponent
{
    private bool _isStartMaster;
    private bool _isStartOther;
    private bool _isSelected;
    private GameObject _cellGO;


    public CellComponent(bool isStartMaster, bool isStartOther,  GameObject cellGO)
    {
        _isStartMaster = isStartMaster;
        _isStartOther = isStartOther;
        _isSelected = default;

        _cellGO = cellGO;
    }


    public bool IsStartMaster => _isStartMaster;
    public bool IsStartOther => _isStartOther;
    public bool IsSelected => _isSelected;
    public int InstanceIDcell => _cellGO.GetInstanceID();

    public void SetIsSelected(bool isActive) => _isSelected = isActive;



    public struct EnvironmentComponent
    {
        private bool _haveMountain;
        private bool _haveTree;
        private bool _haveHill;
        private GameObject _mountainGO;
        private GameObject _treeGO;
        private GameObject _hillGO;

        public EnvironmentComponent(GameObject mountainGO, GameObject treeGO, GameObject hillGO)
        {
            _haveMountain = default;
            _haveTree = default;
            _haveHill = default;

            _mountainGO = mountainGO;
            _treeGO = treeGO;
            _hillGO = hillGO;
        }


        public bool HaveMountain => _haveMountain;
        public bool HaveTree => _haveTree;
        public bool HaveHill => _haveHill;

        public void SetResetEnvironment(bool isActive, EnvironmentTypes environmentType)
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
        private SpriteRenderer _unitSpriteRender;
        private GameObject _unitKingGO;
        private StartValuesConfig _startValues;

        public UnitComponent(StartValuesConfig startValues, Player player, GameObject unitPawnGO)
        {
            _unitType = default;
            _amountSteps = default;
            _amountHealth = default;
            _powerDamage = default;
            _isProtected = default;
            _isRelaxed = default;
            _player = player;
            _startValues = startValues;
            _unitPawnGO = unitPawnGO;
            _unitSpriteRender = _unitPawnGO.GetComponent<SpriteRenderer>();
            _unitKingGO = default;
        }


        public UnitTypes UnitType => _unitType;
        public int AmountSteps => _amountSteps;
        public int AmountHealth => _amountHealth;
        public int PowerDamage => _powerDamage;
        public bool HaveAmountSteps
        {
            get
            {
                if (_amountSteps >= _startValues.TakeAmountSteps) return true;
                else return false;
            }
        }
        public int ActorNumber => _player.ActorNumber;
        public bool IsMine
        {
            get
            {
                if (_player.ActorNumber == Instance.LocalPlayer.ActorNumber)
                {
                    return true;
                }
                return false;
            }
        }
        public bool HaveUnit
        {
            get
            {
                if (UnitType == UnitTypes.None)
                {
                    return false;
                }
                return true;
            }
        }
        public bool IsProtected { get { return _isProtected; } set { _isProtected = value; } }
        public bool IsRelaxed { get { return _isRelaxed; } set { _isRelaxed = value; } }


        private void SetColorUnit(in SpriteRenderer unitSpriteRender, in Player player)
        {
            if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
            else unitSpriteRender.color = Color.yellow;
        }

        public void TakeAmountSteps(int takeAmountSteps) => _amountSteps -= takeAmountSteps;

        public void TakeHealth(in int powerDamage) => _amountHealth -= powerDamage;

        public void RefreshAmountSteps(int amountSteps = -1)
        {
            if (amountSteps == -1)
            {
                switch (_unitType)
                {
                    case UnitTypes.None:
                        break;

                    case UnitTypes.King:
                        break;

                    case UnitTypes.Pawn:
                        _amountSteps = _startValues.AmountStepsPawn;
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
                        break;

                    case UnitTypes.Pawn:
                        _amountSteps = amountSteps;
                        break;

                    default:
                        break;
                }
            }

        }

        public void ResetUnit()
        {
            UnitTypes unitType = default;
            int amountHealth = default;
            int powerDamage = default;
            int amountSteps = default;
            bool isProtected = default;
            bool isRelaxed = default;
            Player player = Instance.MasterClient;

            SetResetUnit(unitType, amountHealth, powerDamage, amountSteps, isProtected, isRelaxed,  player);
        }

        public void SetResetUnit(in UnitComponent cellUnitComponent)
        {
            var unitType = cellUnitComponent._unitType;
            var amountHealth = cellUnitComponent._amountHealth;
            var powerDamage = cellUnitComponent._powerDamage;
            var amountSteps = cellUnitComponent._amountSteps;
            var isProtected = cellUnitComponent._isProtected;
            var isRelaxed = cellUnitComponent._isRelaxed;
            var player = cellUnitComponent._player;

            SetResetUnit(unitType, amountHealth, powerDamage, amountSteps, isProtected, isRelaxed, player);
        }

        public void SetResetUnit(in UnitTypes unitType, in int amountHealth, in int powerDamage, in int amountSteps, in bool isProtected, in bool isRelaxed, in Player player)
        {
            _unitType = unitType;
            _amountSteps = amountSteps;
            _amountHealth = amountHealth;
            _powerDamage = powerDamage;
            _isProtected = isProtected;
            _isRelaxed = isRelaxed;
            _player = player;

            SetColorUnit(_unitSpriteRender, _player);

            switch (_unitType)
            {
                case UnitTypes.None:
                    _unitPawnGO.SetActive(false);
                    break;

                case UnitTypes.King:
                    _unitKingGO.SetActive(true);
                    break;

                case UnitTypes.Pawn:
                    _unitPawnGO.SetActive(true);
                    break;

                default:
                    break;
            }
        }

        public void EnableVisionCell(bool isActive, UnitTypes unitType, Player player)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    break;

                case UnitTypes.Pawn:
                    _unitPawnGO.SetActive(isActive);
                    SetColorUnit(_unitSpriteRender, player);
                    break;

                default:
                    break;
            }
        }

        public bool IsHim(Player player) => _player.ActorNumber == player.ActorNumber;
    }


    public struct BuildingComponent
    {
        private BuildingTypes _buildingType;
        private GameObject _campGO;

        internal BuildingTypes BuildingType => _buildingType;
        internal bool HaveBuilding => _buildingType != BuildingTypes.None;


        public BuildingComponent(GameObject campGO)
        {
            _buildingType = default;
            _campGO = campGO;
        }


        public void SetResetBuilding(in BuildingTypes buildingType)
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
    }


    public struct SupportVisionComponent
    {
        private GameObject _selectorVisionGO;
        private GameObject _spawnVisionGO;
        private GameObject _wayUnitVisionGO;
        private GameObject _enemyVisionGO;

        public SupportVisionComponent(GameObject selectorVisionGO, GameObject spawnVisionGO, GameObject wayUnitVisionGO, GameObject enemyVisionGO)
        {
            _selectorVisionGO = selectorVisionGO;
            _spawnVisionGO = spawnVisionGO;
            _wayUnitVisionGO = wayUnitVisionGO;
            _enemyVisionGO = enemyVisionGO;
        }


        public void EnableVision(bool isActive, SupportVisionTypes supportVisionType)
        {
            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    break;

                case SupportVisionTypes.SelectorVision:
                    _selectorVisionGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.SpawnVision:
                    _spawnVisionGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.WayOfUnitVision:
                    _wayUnitVisionGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.EnemyVision:
                    _enemyVisionGO.SetActive(isActive);
                    break;

                default:
                    break;
            }
        }

    }
}
