using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class Entities : EntityAbstract
    {
        readonly Dictionary<string, UnitsInInventorE> _units;
        readonly Dictionary<string, ScoutHeroCooldownE> _scoutHeroCooldownEs;
        readonly Dictionary<PlayerTypes, ReadyE> _ready;
        readonly Dictionary<ClipTypes, SoundE> _sounds0;
        readonly Dictionary<AbilityTypes, SoundE> _sounds1;
        readonly Dictionary<PlayerTypes, AvailableCenterHeroE> _availHero;
        readonly Dictionary<PlayerTypes, MaxAvailablePawnsE> _maxPawnsEs;
        readonly Dictionary<PlayerTypes, PeopleInCityE> _peopleEs;
        readonly Dictionary<ResourceTypes, MistakeEconomyE> _mistakeEconomyEs;

        string KeyInventorUnits(UnitTypes unit, LevelTypes level, PlayerTypes player) => unit.ToString() + level + player;
        public UnitsInInventorE Units(in UnitTypes unit, in LevelTypes level, in PlayerTypes player) => _units[KeyInventorUnits(unit, level, player)];
        public UnitsInInventorE Units(in CellUnitE unitE) => _units[KeyInventorUnits(unitE.UnitTC.Unit, unitE.LevelTC.Level, unitE.PlayerTC.Player)];
        public UnitsInInventorE Units(in string key) => _units[key];
        public ScoutHeroCooldownE ScoutHeroCooldownE(in UnitTypes unit, in PlayerTypes player) => _scoutHeroCooldownEs[unit.ToString() + player];
        public ScoutHeroCooldownE ScoutHeroCooldownE(in CellUnitE unitE) => _scoutHeroCooldownEs[unitE.UnitTC.Unit.ToString() + unitE.PlayerTC.Player];
        public ReadyE ReadyE(in PlayerTypes player) => _ready[player];
        public SoundE Sound(in ClipTypes clip) => _sounds0[clip];
        public SoundE Sound(in AbilityTypes unique) => _sounds1[unique];
        public AvailableCenterHeroE AvailableCenterHero(in PlayerTypes player) => _availHero[player];
        public MaxAvailablePawnsE MaxAvailablePawnsE(in PlayerTypes player) => _maxPawnsEs[player];
        public PeopleInCityE PeopleInCityE(in PlayerTypes player) => _peopleEs[player];
        public MistakeEconomyE MistakeEconomyE(in ResourceTypes resT) => _mistakeEconomyEs[resT];

        public readonly RpcE RpcE;

        public CurrentIdxC CurrentIdxC => Ent.Get<CurrentIdxC>();
        public SelectedIdxC SelectedIdxC => Ent.Get<SelectedIdxC>();
        public IdxC PreviousVisionIdxC => Ent.Get<IdxC>();
        public SunSideTC SunSideTC => Ent.Get<SunSideTC>();
        public CenterCloudIdxC CenterCloudIdxC => Ent.Get<CenterCloudIdxC>();
        public IsClickedC IsClickedC => Ent.Get<IsClickedC>();
        public MotionsC MotionsC => Ent.Get<MotionsC>();     
        public WinnerPlayerTC WinnerC => Ent.Get<WinnerPlayerTC>();
        public WindDirectTC DirectWind => Ent.Get<WindDirectTC>();
        public IsStartedGameC IsStartedGameC => Ent.Get<IsStartedGameC>();
        public ref BuildingTC SelectedBuildingTC => ref Ent.Get<BuildingTC>();
        public WhoseMovePlayerTC WhoseMovePlayerTC => Ent.Get<WhoseMovePlayerTC>();

        public ref CellClickC CellClickTC => ref Ent.Get<CellClickC>();
        public ref RayCastTC RayCastTC => ref Ent.Get<RayCastTC>();

        public IsActiveC MotionIsActiveC => Ent.Get<IsActiveC>();
        public EnvironmentZoneIsActiveC EnvIsActiveC => Ent.Get<EnvironmentZoneIsActiveC>();
        public FriendZoneIsActiveC FriendIsActiveC => Ent.Get<FriendZoneIsActiveC>();

        public ToolWeaponTC SelectedTWTC => Ent.Get<ToolWeaponTC>();
        public LevelTC SelectedTWLevelTC => Ent.Get<LevelTC>();

        public ref MistakeTC MistakeC => ref Ent.Get<MistakeTC>();
        public TimerC TimerC => Ent.Get<TimerC>();

        public SelectedUnitTC SelUnitTC => Ent.Get<SelectedUnitTC>();
        public SelectedUnitLevelTC SelUnitLevelTC => Ent.Get<SelectedUnitLevelTC>();

        public ref AbilityTC SelAbilityTC => ref Ent.Get<AbilityTC>();

        public StartTeleportIdxC StartTeleportIdxC => Ent.Get<StartTeleportIdxC>();
        public EndTeleportIdxC EndTeleportIdxC => Ent.Get<EndTeleportIdxC>();



        public bool HaveHeroInInventor(in PlayerTypes owner, out UnitTypes hero)
        {
            for (var unit = UnitTypes.Elfemale; unit < UnitTypes.Camel; unit++)
            {
                for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                {
                    if (Units(KeyInventorUnits(unit, level, owner)).HaveUnits)
                    {
                        hero = unit;
                        return true;
                    }
                }
            }

            hero = default;
            return false;
        }


        #region Pools

        public readonly InventorResourcesEs InventorResourcesEs;
        public readonly InventorToolWeaponEs InventorToolWeaponEs;

        public readonly UnitStatUpgradesEs UnitStatUpgradesEs;
        public readonly AvailableCenterUpgradeEs AvailableCenterUpgradeEs;
        public readonly BuildingUpgradeEs BuildingUpgradeEs;


        #region Cells

        readonly CellEs[] _cellEs;
        public CellEs[] Cells => (CellEs[])_cellEs.Clone();
        public byte LengthCells => (byte)_cellEs.Length;


        public CellEs CellEs(in byte idx) => _cellEs[idx];

        #region Unit

        public CellUnitEs UnitEs(in byte idx) => CellEs(idx).UnitEs;

        public CellUnitE UnitE(in byte idx) => UnitEs(idx).UnitE;
        public UnitTC UnitTC(in byte idx) => UnitE(idx).UnitTC;
        public PlayerTC UnitPlayerTC(in byte idx) => UnitE(idx).PlayerTC;
        public LevelTC UnitLevelTC(in byte idx) => UnitE(idx).LevelTC;
        public ref ConditionUnitTC UnitConditionTC(in byte idx) => ref UnitE(idx).ConditionTC;
        public ref IsRightArcherC UnitIsRightArcherC(in byte idx) => ref UnitE(idx).IsRightArcherC;
        public HealthC UnitHpC(in byte idx) => UnitE(idx).HealthC;
        public StepsC UnitStepC(in byte idx) => UnitE(idx).StepC;
        public WaterC UnitWaterC(in byte idx) => UnitE(idx).WaterC;

        public CellUnitMainToolWeaponE MainTWE(in byte idx) => UnitEs(idx).MainToolWeaponE;
        public ToolWeaponTC UnitMainTWTC(in byte idx) => MainTWE(idx).ToolWeaponTC;
        public LevelTC UnitMainTWLevelTC(in byte idx) => MainTWE(idx).LevelTC;

        public CellUnitExtraToolWeaponE ExtraTWE(in byte idx) => UnitEs(idx).ExtraToolWeaponE;


        #region Effects

        public ref StunC UnitStunC(in byte idx) => ref UnitE(idx).StunC;
        public ShieldEffectC UnitEffectShield(in byte idx) => UnitE(idx).ShieldEffectC;
        public ref FrozenArrawC UnitFrozenArrawC(in byte idx) => ref UnitE(idx).FrozenArrawC;

        #endregion

        #endregion

        public CellBuildingEs BuildEs(in byte idx) => CellEs(idx).BuildEs;
        public CellBuildingE BuildingE(in byte idx) => BuildEs(idx).BuildingE;

        public CellEnvironmentEs EnvironmentEs(in byte idx) => CellEs(idx).EnvironmentEs;
        public CellEnvYoungForestE YoungForestE(in byte idx) => EnvironmentEs(idx).YoungForest;
        public CellEnvAdultForestE AdultForestE(in byte idx) => EnvironmentEs(idx).AdultForest;
        public CellEnvMountainE MountainE(in byte idx) => EnvironmentEs(idx).Mountain;
        public CellEnvHillE HillE(in byte idx) => EnvironmentEs(idx).Hill;
        public CellEnvFertilizerE FertilizeE(in byte idx) => EnvironmentEs(idx).Fertilizer;

        public CellRiverEs RiverEs(in byte idx) => CellEs(idx).RiverEs;
        public CellTrailEs TrailEs(in byte idx) => CellEs(idx).TrailEs;
        public CellEffectEs EffectEs(in byte idx) => CellEs(idx).EffectEs;




        public readonly CellSpaceWorker CellSpaceWorker;
        public readonly WhereWorker WhereWorker;

        #endregion

        #endregion


        public Entities(in EcsWorld gameW, in List<object> forData, in List<string> namesMethods) : base(gameW)
        {
            Ent.Add(new SunSideTC(SunSideTypes.Dawn))
               .Add(new CenterCloudIdxC(60))
               .Add(new WindDirectTC(DirectTypes.Right))
               .Add(new FriendZoneIsActiveC(GameModeC.IsGameMode(GameModes.WithFriendOff)))
               .Add(new ToolWeaponTC(ToolWeaponTypes.Pick))
               .Add(new LevelTC(LevelTypes.Second))
               .Add(new WhoseMovePlayerTC(PlayerTypes.First))
               .Add(new CellClickC(CellClickTypes.SimpleClick));
    

            var i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, System.Action>)forData[i++];
            var sounds1 = (Dictionary<AbilityTypes, System.Action>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];


            _units = new Dictionary<string, UnitsInInventorE>();
            _scoutHeroCooldownEs = new Dictionary<string, ScoutHeroCooldownE>();
            _ready = new Dictionary<PlayerTypes, ReadyE>();
            _availHero = new Dictionary<PlayerTypes, AvailableCenterHeroE>();
            _maxPawnsEs = new Dictionary<PlayerTypes, MaxAvailablePawnsE>();
            _peopleEs = new Dictionary<PlayerTypes, PeopleInCityE>();
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _ready.Add(player, new ReadyE(gameW));
                _availHero.Add(player, new AvailableCenterHeroE(true, gameW));
                _maxPawnsEs.Add(player, new MaxAvailablePawnsE(player, gameW));
                _peopleEs.Add(player, new PeopleInCityE(player, gameW));

                for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
                {
                    _scoutHeroCooldownEs.Add(unit.ToString() + player, new ScoutHeroCooldownE(gameW));
                }

                for (var unit = UnitTypes.None + 1; unit < UnitTypes.Camel; unit++)
                {
                    for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
                    {
                        _units.Add(KeyInventorUnits(unit, level, player), new UnitsInInventorE(unit, level, gameW));
                    }
                }
            }
            _mistakeEconomyEs = new Dictionary<ResourceTypes, MistakeEconomyE>();
            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                _mistakeEconomyEs.Add(resT, new Game.MistakeEconomyE(resT, gameW));
            }


            _sounds0 = new Dictionary<ClipTypes, SoundE>();
            _sounds1 = new Dictionary<AbilityTypes, SoundE>();
            foreach (var item in sounds0) _sounds0.Add(item.Key, new SoundE(item.Value, gameW));
            foreach (var item in sounds1) _sounds1.Add(item.Key, new SoundE(item.Value, gameW));

            RpcE = new RpcE(actions, namesMethods);

            InventorResourcesEs = new InventorResourcesEs(gameW);
            InventorToolWeaponEs = new InventorToolWeaponEs(gameW);

            UnitStatUpgradesEs = new UnitStatUpgradesEs(gameW);

            AvailableCenterUpgradeEs = new AvailableCenterUpgradeEs(gameW);
            BuildingUpgradeEs = new BuildingUpgradeEs(gameW);


            _cellEs = new CellEs[StartValues.ALL_CELLS_AMOUNT];
            byte idx = 0;
            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
                {
                    _cellEs[idx] = new CellEs(isActiveParenCells[idx], idCells[idx], new byte[] { x, y }, idx, gameW);
                    ++idx;
                }
            CellSpaceWorker = new CellSpaceWorker(_cellEs);
            WhereWorker = new WhereWorker(_cellEs);


            new CellsForSetUnitsEs(gameW);
            new CellsForShiftUnitsEs(gameW);
            new CellsForAttackUnitsEs(gameW);
            new CellsForArsonArcherEs(gameW);
            new EntHint(gameW);
        }
    }
}