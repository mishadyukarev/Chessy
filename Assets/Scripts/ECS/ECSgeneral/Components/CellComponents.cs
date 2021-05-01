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

        if (InstanceGame.IS_TEST)
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

        _cellGO = InstanceGame.StartSpawnGameManager.CellsGO[x, y];
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
        private List<EnvironmentTypes> _listEnvironmentTypes;

        internal EnvironmentComponent(int x, int y)
        {
            _xy = new int[] { x, y };

            _haveFood = default;
            _haveMountain = default;
            _haveTree = default;
            _haveHill = default;

            _foodGO = InstanceGame.StartSpawnGameManager.FoodsGO[x, y];
            _mountainGO = InstanceGame.StartSpawnGameManager.MountainsGO[x, y];
            _treeGO = InstanceGame.StartSpawnGameManager.TreesGO[x, y];
            _hillGO = InstanceGame.StartSpawnGameManager.HillsGO[x, y];

            _listEnvironmentTypes = new List<EnvironmentTypes>();
            _listEnvironmentTypes.Add(default);
            _listEnvironmentTypes.Add(default);
            _listEnvironmentTypes.Add(default);
            _listEnvironmentTypes.Add(default);
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
        private bool _isActiveUnitMaster;
        private bool _isActiveUnitOther;
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

        internal UnitComponent(int x, int y)
        {
            _xy = new int[] { x, y };

            _isActiveUnitMaster = default;
            _isActiveUnitOther = default;
            _unitType = default;
            _amountSteps = default;
            _amountHealth = default;
            _powerDamage = default;
            _powerProtection = default;
            _isProtected = default;
            _isRelaxed = default;
            _player = default;

            _unitPawnGO = InstanceGame.StartSpawnGameManager.UnitPawnsGO[x, y];
            _unitKingGO = InstanceGame.StartSpawnGameManager.UnitKingsGO[x, y];
            _unitPawnSpriteRender = InstanceGame.StartSpawnGameManager.UnitPawnsGOsr[x, y];
            _unitKingSpriteRender = InstanceGame.StartSpawnGameManager.UnitKingsGOsr[x, y];
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

        internal int PowerDamage
        {
            get { return _powerDamage; }
            set { _powerDamage = value; }
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
                        return _amountSteps == InstanceGame.StartValuesGameConfig.MAX_AMOUNT_STEPS_KING;
                    case UnitTypes.Pawn:
                        return _amountSteps == InstanceGame.StartValuesGameConfig.MAX_AMOUNT_STEPS_PAWN;
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


        #region Methods

        private void SetColorUnit(in SpriteRenderer unitSpriteRender, in Player player)
        {
            if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
            else unitSpriteRender.color = Color.red;
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
        internal void ActiveVisionCell(bool isActive, UnitTypes unitType, Player player = default)
        {
            switch (unitType)
            {
                case UnitTypes.King:
                    _unitKingGO.SetActive(isActive);
                    if (player != default) SetColorUnit(_unitKingSpriteRender, player);
                    break;

                case UnitTypes.Pawn:
                    _unitPawnGO.SetActive(isActive);
                    if (player != default) SetColorUnit(_unitPawnSpriteRender, player);
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
        private Player _player;

        internal BuildingComponent(int x, int y)
        {
            _xy = new int[] { x, y };

            _buildingType = default;
            _cityGO = InstanceGame.StartSpawnGameManager.CampsGO[x, y];
            _farmGO = InstanceGame.StartSpawnGameManager.FarmsGO[x, y];
            _woodcutterGO = InstanceGame.StartSpawnGameManager.WoodcuttersGO[x, y];

            _player = default;
        }

        #region Properties

        private StartValuesGameConfig StartValuesGameConfig => InstanceGame.StartValuesGameConfig;

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
        private Player _player;
        private GameObject _selectorVisionGO;
        private GameObject _spawnVisionGO;
        private GameObject _wayUnitVisionGO;
        private GameObject _enemyVisionGO;
        private GameObject _zoneVisionGO;

        internal SupportVisionComponent(int x, int y)
        {
            _player = default;

            _selectorVisionGO = InstanceGame.StartSpawnGameManager.SelectorVisionsGO[x, y];
            _spawnVisionGO = InstanceGame.StartSpawnGameManager.SpawnVisionsGO[x, y];
            _wayUnitVisionGO = InstanceGame.StartSpawnGameManager.WayUnitVisionsGO[x, y];
            _enemyVisionGO = InstanceGame.StartSpawnGameManager.EnemyVisionsGO[x, y];
            _zoneVisionGO = InstanceGame.StartSpawnGameManager.ZoneVisionGO[x, y];
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

                case SupportVisionTypes.Zone:
                    _zoneVisionGO.SetActive(isActive);
                    SetColorVision(_zoneVisionGO.GetComponent<SpriteRenderer>(), _player);
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}
