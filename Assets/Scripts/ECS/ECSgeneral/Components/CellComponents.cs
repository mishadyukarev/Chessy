using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using static MainGame;

public struct CellComponent
{
    private int[] _xy;
    private bool _isStartMaster;
    private bool _isStartOther;
    private bool _isSelected;
    private GameObject _cellGO;

    internal CellComponent(int x, int y)
    {
        _xy = new int[] { x, y };

        if (InstanceGame.StartValuesGameConfig.IS_TEST)
        {
            _isStartMaster = true;
            _isStartOther = true;
        }
        else
        {
            _isStartMaster = y < 3 && x > 2 && x < 12;
            _isStartOther = y > 8 && x > 2 && x < 12;
        }

        _isSelected = default;

        _cellGO = InstanceGame.GameObjectPool.CellsGO[x, y];
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
        private int[] _xy;
        private bool _haveFood;
        private bool _haveMountain;
        private bool _haveTree;
        private bool _haveHill;
        private GameObject _foodGO;
        private GameObject _mountainGO;
        private GameObject _treeGO;
        private GameObject _hillGO;

        internal EnvironmentComponent(int x, int y)
        {
            _xy = new int[] { x, y };

            _haveFood = default;
            _haveMountain = default;
            _haveTree = default;
            _haveHill = default;

            _foodGO = InstanceGame.GameObjectPool.CellEnvironmentFoodGOs[x, y];
            _mountainGO = InstanceGame.GameObjectPool.CellEnvironmentMountainGOs[x, y];
            _treeGO = InstanceGame.GameObjectPool.CellEnvironmentTreeGOs[x, y];
            _hillGO = InstanceGame.GameObjectPool.CellEnvironmentHillGOs[x, y];
        }

        internal bool HaveFood => _haveFood;
        internal bool HaveMountain => _haveMountain;
        internal bool HaveTree => _haveTree;
        internal bool HaveHill => _haveHill;

        internal List<EnvironmentTypes> ListEnvironmentTypes
        {
            get
            {
                List<EnvironmentTypes> listEnvironmentTypes = new List<EnvironmentTypes>();

                if (_haveFood) listEnvironmentTypes.Add(EnvironmentTypes.Food);
                if (_haveTree) listEnvironmentTypes.Add(EnvironmentTypes.Tree);
                if (_haveHill) listEnvironmentTypes.Add(EnvironmentTypes.Hill);

                return listEnvironmentTypes;
            }
        }

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
        private int[] _xy;
        private bool _canShift;
        private bool _canAttack;
        private bool _isActiveUnitMaster;
        private bool _isActiveUnitOther;
        private UnitTypes _unitType;
        private int _amountSteps;
        private int _amountHealth;
        private bool _isProtected;
        private bool _isRelaxed;
        private Player _player;
        private GameObject _pawnGO;
        private GameObject _kingGO;
        private GameObject _rookGO;
        private GameObject _bishopGO;
        private SpriteRenderer _unitPawnSpriteRender;
        private SpriteRenderer _unitKingSpriteRender;

        internal UnitComponent(int x, int y)
        {
            _xy = new int[] { x, y };

            _canShift = default;
            _canAttack = default;

            _isActiveUnitMaster = default;
            _isActiveUnitOther = default;
            _unitType = default;
            _amountSteps = default;
            _amountHealth = default;
            _isProtected = default;
            _isRelaxed = default;
            _player = default;

            _pawnGO = InstanceGame.GameObjectPool.CellUnitPawnGOs[x, y];
            _kingGO = InstanceGame.GameObjectPool.CellUnitKingGOs[x, y];
            _rookGO = InstanceGame.GameObjectPool.CellUnitRookGOs[x, y];
            _bishopGO = InstanceGame.GameObjectPool.CellUnitBishopGOs[x, y];

            _unitPawnSpriteRender = InstanceGame.GameObjectPool.CellUnitPawnGOs[x, y].GetComponent<SpriteRenderer>();
            _unitKingSpriteRender = InstanceGame.GameObjectPool.CellUnitKingGOs[x, y].GetComponent<SpriteRenderer>();
        }


        #region Properties

        #region Base

        internal UnitTypes UnitType => _unitType;
        internal bool HaveUnit => UnitType != UnitTypes.None;

        #endregion


        #region Activity

        internal bool IsActiveUnitMaster
        {
            get => _isActiveUnitMaster;
            set => _isActiveUnitMaster = value;
        }
        internal bool IsActiveUnitOther
        {
            get => _isActiveUnitOther;
            set => _isActiveUnitOther = value;
        }

        #endregion


        #region Player

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


        #region Health

        internal int AmountHealth
        {
            get { return _amountHealth; }
            set { _amountHealth = value; }
        }

        #endregion


        #region Damage

        internal int SimplePowerDamage
        {
            get
            {
                switch (_unitType)
                {
                    case UnitTypes.None:
                        return default;

                    case UnitTypes.King:
                        return InstanceGame.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_KING;

                    case UnitTypes.Pawn:
                        return InstanceGame.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_PAWN;

                    case UnitTypes.Rook:
                        return InstanceGame.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_ROOK;

                    case UnitTypes.Bishop :
                        return InstanceGame.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_BISHOP;

                    default:
                        return default;
                }
            }
        }

        internal int UniquePowerDamage
        {
            get
            {
                switch (_unitType)
                {
                    case UnitTypes.None:
                        return default;

                    case UnitTypes.King:
                        return InstanceGame.StartValuesGameConfig.UNIQIE_POWER_DAMAGE_KING;

                    case UnitTypes.Pawn:
                        return InstanceGame.StartValuesGameConfig.UNIQIE_POWER_DAMAGE_PAWN;

                    case UnitTypes.Rook:
                        return InstanceGame.StartValuesGameConfig.UNIQIE_POWER_DAMAGE_ROOK;

                    case UnitTypes.Bishop:
                        return InstanceGame.StartValuesGameConfig.UNIQIE_POWER_DAMAGE_BISHOP;

                    default:
                        return default;
                }
            }
        }

        #endregion


        #region Protection

        internal int PowerProtection(List<EnvironmentTypes> listEnvironmentTypes, BuildingTypes buildingType)
        {
            int powerProtection = 0;

            if (_isProtected)
            {
                switch (_unitType)
                {
                    case UnitTypes.King:
                        powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_KING;
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_PAWN;
                        break;

                    case UnitTypes.Rook:
                        powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_ROOK;
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_BISHOP;
                        break;
                }
            }

            else if (_isRelaxed)
            {
                switch (_unitType)
                {
                    case UnitTypes.King:
                        powerProtection -= InstanceGame.StartValuesGameConfig.PROTECTION_KING;
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= InstanceGame.StartValuesGameConfig.PROTECTION_PAWN;
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= InstanceGame.StartValuesGameConfig.PROTECTION_ROOK;
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= InstanceGame.StartValuesGameConfig.PROTECTION_BISHOP;
                        break;
                }
            }

            foreach (var item in listEnvironmentTypes)
            {
                switch (_unitType)
                {
                    case UnitTypes.King:

                        switch (item)
                        {
                            case EnvironmentTypes.Food:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_FOOD_FOR_KING;
                                break;

                            case EnvironmentTypes.Tree:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_TREE_FOR_KING;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_TREE_FOR_KING;
                                break;
                        }

                        break;


                    case UnitTypes.Pawn:

                        switch (item)
                        {
                            case EnvironmentTypes.Food:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_FOOD_FOR_PAWN;
                                break;

                            case EnvironmentTypes.Tree:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_TREE_FOR_PAWN;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_HILL_FOR_PAWN;
                                break;
                        }

                        break;


                    case UnitTypes.Rook:

                        switch (item)
                        {
                            case EnvironmentTypes.Food:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_FOOD_FOR_ROOK;
                                break;

                            case EnvironmentTypes.Tree:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_TREE_FOR_ROOK;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_HILL_FOR_ROOK;
                                break;
                        }

                        break;


                    case UnitTypes.Bishop:

                        switch (item)
                        {
                            case EnvironmentTypes.Food:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_FOOD_FOR_BISHOP;
                                break;

                            case EnvironmentTypes.Tree:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_TREE_FOR_BISHOP;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_HILL_FOR_BISHOP;
                                break;
                        }

                        break;
                }

            }

            switch (buildingType)
            {
                case BuildingTypes.City:

                    switch (_unitType)
                    {
                        case UnitTypes.King:
                            powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_CITY_KING;
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_CITY_PAWN;
                            break;

                        case UnitTypes.Rook:
                            powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_CITY_ROOK;
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += InstanceGame.StartValuesGameConfig.PROTECTION_CITY_BISHOP;
                            break;
                    }

                    break;

                case BuildingTypes.Farm:
                    break;

                case BuildingTypes.Woodcutter:
                    break;

                case BuildingTypes.Mine:
                    break;
            }

            return powerProtection;
        }

        #endregion


        #region Steps

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
                        return _amountSteps == InstanceGame.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                    case UnitTypes.Pawn:
                        return _amountSteps == InstanceGame.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                    case UnitTypes.Rook:
                        return _amountSteps == InstanceGame.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                    case UnitTypes.Bishop:
                        return _amountSteps == InstanceGame.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
                }
                return false;
            }
        }
        internal bool MinAmountSteps => _amountSteps >= InstanceGame.StartValuesGameConfig.MIN_AMOUNT_STEPS_FOR_UNIT;

        internal int NeedAmountSteps(List<EnvironmentTypes> listEnvironmentTypes)
        {
            int amountSteps = 1;

            foreach (var item in listEnvironmentTypes)
            {
                switch (item)
                {
                    case EnvironmentTypes.Food:
                        amountSteps += InstanceGame.StartValuesGameConfig.NEED_AMOUNT_STEPS_FOOD;
                        break;

                    case EnvironmentTypes.Tree:
                        amountSteps += InstanceGame.StartValuesGameConfig.NEED_AMOUNT_STEPS_TREE;
                        break;

                    case EnvironmentTypes.Hill:
                        amountSteps += InstanceGame.StartValuesGameConfig.NEED_AMOUNT_STEPS_HILL;
                        break;
                }
            }

            return amountSteps;
        }

        internal void RefreshAmountSteps()
        {
            switch (_unitType)
            {
                case UnitTypes.King:
                    _amountSteps = InstanceGame.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                    break;

                case UnitTypes.Pawn:
                    _amountSteps = InstanceGame.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                    break;

                case UnitTypes.Rook:
                    _amountSteps = InstanceGame.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                    break;

                case UnitTypes.Bishop:
                    _amountSteps = InstanceGame.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
                    break;

                default:
                    break;
            }
        }

        #endregion


        #region Condition

        internal bool IsRelaxed
        {
            get { return _isRelaxed; }
            set { _isRelaxed = value; }
        }
        internal bool IsProtected
        {
            get { return _isProtected; }
            set { _isProtected = value; }
        }

        #endregion

        #endregion


        #region Else

        private void SetColorUnit(in SpriteRenderer unitSpriteRender, in Player player)
        {
            if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
            else unitSpriteRender.color = Color.red;
        }


        internal void ResetUnit()
        {
            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            bool isProtected = default;
            bool isRelaxed = default;
            Player player = default;

            SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
        }
        internal void SetUnit(in UnitComponent cellUnitComponent)
        {
            var unitType = cellUnitComponent._unitType;
            var amountHealth = cellUnitComponent._amountHealth;
            var amountSteps = cellUnitComponent._amountSteps;
            var isProtected = cellUnitComponent._isProtected;
            var isRelaxed = cellUnitComponent._isRelaxed;
            var player = cellUnitComponent._player;

            SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
        }
        internal void SetUnit(in UnitTypes unitType, in int amountHealth, in int amountSteps, in bool isProtected, in bool isRelaxed, in Player player)
        {
            _unitType = unitType;
            _amountSteps = amountSteps;
            _amountHealth = amountHealth;
            _isProtected = isProtected;
            _isRelaxed = isRelaxed;
            _player = player;



            _pawnGO.SetActive(false);
            _kingGO.SetActive(false);
            _rookGO.SetActive(false);
            _bishopGO.SetActive(false);

            switch (_unitType)
            {
                case UnitTypes.King:
                    _kingGO.SetActive(true);
                    SetColorUnit(_unitKingSpriteRender, _player);
                    break;

                case UnitTypes.Pawn:
                    _pawnGO.SetActive(true);
                    SetColorUnit(_unitPawnSpriteRender, _player);
                    break;

                case UnitTypes.Rook:
                    _rookGO.SetActive(true);
                    SetColorUnit(_rookGO.GetComponent<SpriteRenderer>(), _player);
                    break;

                case UnitTypes.Bishop:
                    _bishopGO.SetActive(true);
                    SetColorUnit(_bishopGO.GetComponent<SpriteRenderer>(), _player);
                    break;
            }
        }
        internal void ActiveVisionCell(bool isActive, UnitTypes unitType, Player player = default)
        {
            switch (unitType)
            {
                case UnitTypes.King:
                    _kingGO.SetActive(isActive);
                    if (player != default) SetColorUnit(_unitKingSpriteRender, player);
                    break;

                case UnitTypes.Pawn:
                    _pawnGO.SetActive(isActive);
                    if (player != default) SetColorUnit(_unitPawnSpriteRender, player);
                    break;

                case UnitTypes.Rook:
                    _rookGO.SetActive(isActive);
                    if (player != default) SetColorUnit(_rookGO.GetComponent<SpriteRenderer>(), player);
                    break;

                case UnitTypes.Bishop:
                    _bishopGO.SetActive(isActive);
                    if (player != default) SetColorUnit(_bishopGO.GetComponent<SpriteRenderer>(), player);
                    break;

                default:
                    break;
            }
        }
        internal bool IsHisUnit(Player player)
        {
            if (_player == default) return false;
            return _player.ActorNumber == player.ActorNumber;
        }

        #endregion
    }



    public struct BuildingComponent
    {
        private int[] _xy;
        private BuildingTypes _buildingType;
        private GameObject _cityGO;
        private GameObject _farmGO;
        private GameObject _woodcutterGO;
        private GameObject _mineGO;
        private Player _player;

        internal BuildingComponent(int x, int y)
        {
            _xy = new int[] { x, y };

            _buildingType = default;
            _cityGO = InstanceGame.GameObjectPool.CellBuildingCityGOs[x, y];
            _farmGO = InstanceGame.GameObjectPool.CellBuildingFarmGOs[x, y];
            _woodcutterGO = InstanceGame.GameObjectPool.CellBuildingWoodcutterGOs[x, y];
            _mineGO = InstanceGame.GameObjectPool.CellBuildingMineGOs[x, y];

            _player = default;
        }

        #region Properties

        internal BuildingTypes BuildingType => _buildingType;
        internal bool HaveBuilding => _buildingType != BuildingTypes.None;

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
            _mineGO.SetActive(false);

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

                case BuildingTypes.Mine:
                    _mineGO.SetActive(true);
                    SetColorBuilding(_mineGO.GetComponent<SpriteRenderer>(), _player);
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
        private Player _player;
        private GameObject _selectorGO;
        private GameObject _spawnGO;
        private GameObject _wayUnitGO;
        private GameObject _enemyGO;
        private GameObject _uniqueAttackGO;
        private GameObject _zoneGO;

        internal SupportVisionComponent(int x, int y)
        {
            _player = default;

            _selectorGO = InstanceGame.GameObjectPool.CellSupportVisionSelectorGOs[x, y];
            _spawnGO = InstanceGame.GameObjectPool.CellSupportVisionSpawnGOs[x, y];
            _wayUnitGO = InstanceGame.GameObjectPool.CellSupportVisionWayUnitGOs[x, y];
            _enemyGO = InstanceGame.GameObjectPool.CellSupportVisionEnemyGOs[x, y];
            _uniqueAttackGO = InstanceGame.GameObjectPool.CellSupportVisionUniqueAttackGOs[x, y];
            _zoneGO = InstanceGame.GameObjectPool.CellSupportVisionZoneGOs[x, y];
        }


        #region Methods

        private void SetColorVision(in SpriteRenderer unitSpriteRender, in Player player)
        {
            if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
            else unitSpriteRender.color = Color.red;

            unitSpriteRender.color = new Color(0, 0, 1, 0.15f);
        }

        internal void ActiveVision(bool isActive, SupportVisionTypes supportVisionType, Player player = default)
        {
            _player = player;

            switch (supportVisionType)
            {
                case SupportVisionTypes.None:
                    break;

                case SupportVisionTypes.Selector:
                    _selectorGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.Spawn:
                    _spawnGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.WayOfUnit:
                    _wayUnitGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.SimpleAttack:
                    _enemyGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.UniqueAttack:
                    _uniqueAttackGO.SetActive(isActive);
                    break;

                case SupportVisionTypes.Zone:
                    _zoneGO.SetActive(isActive);
                    SetColorVision(_zoneGO.GetComponent<SpriteRenderer>(), _player);
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}
