using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct Entities
    {
        readonly Dictionary<string, ScoutHeroCooldownE> _scoutHeroCooldownEs;
        readonly Dictionary<PlayerTypes, ReadyE> _ready;
        readonly Dictionary<ClipTypes, SoundE> _sounds0;
        readonly Dictionary<AbilityTypes, SoundE> _sounds1;
        readonly Dictionary<PlayerTypes, AvailableCenterHeroE> _availHero;
        readonly Dictionary<PlayerTypes, MaxAvailablePawnsE> _maxPawnsEs;
        readonly Dictionary<PlayerTypes, PeopleInCityE> _peopleEs;

        public ScoutHeroCooldownE ScoutHeroCooldownE(in UnitTypes unit, in PlayerTypes player) => _scoutHeroCooldownEs[unit.ToString() + player];
        public ScoutHeroCooldownE ScoutHeroCooldownE(in CellUnitEs unitEs) => _scoutHeroCooldownEs[unitEs.TypeE.UnitTC.Unit.ToString() + unitEs.OwnerE.OwnerC.Player];
        public ReadyE Ready(in PlayerTypes player) => _ready[player];
        public SoundE Sound(in ClipTypes clip) => _sounds0[clip];
        public SoundE Sound(in AbilityTypes unique) => _sounds1[unique];
        public AvailableCenterHeroE AvailableCenterHero(in PlayerTypes player) => _availHero[player];
        public MaxAvailablePawnsE MaxAvailablePawnsE(in PlayerTypes player) => _maxPawnsEs[player];
        public PeopleInCityE PeopleInCityE(in PlayerTypes player) => _peopleEs[player];


        public readonly CurrentIdxE CurrentIdxE;
        public readonly SelectedIdxE SelectedIdxE;
        public readonly SunSidesE SunSidesE;
        public readonly MotionE Motion;
        public readonly InputE InputE;
        public readonly PreviousVisionIdxE PreviousVisionIdxE;
        public readonly WinnerE WinnerE;
        public readonly WindCloudE WindCloudE;
        public readonly FriendZoneE FriendZoneE;
        public readonly GameInfoE GameInfoE;
        public readonly InfoEnvironmentE InfoEnvironmentE;
        public readonly SelectedUnitE SelectedUnitE;
        public readonly ClickerObjectE ClickerObjectE;
        public readonly WhoseMoveE WhoseMoveE;
        public readonly SelectedAbilityE SelectedAbilityE;
        public readonly SelectedBuildingE SelectedBuildingE;
        public readonly RpcE RpcE;
        public readonly SelectedToolWeaponE SelectedToolWeaponE;
        public readonly StartTeleportE StartTeleportE;
        public readonly EndTeleportE EndTeleportE;


        #region Pools

        public readonly InventorUnitsEs InventorUnitsEs;
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

        public CellUnitEs UnitEs(in byte idx) => CellEs(idx).UnitEs;
        public CellUnitE UnitTypeE(in byte idx) => UnitEs(idx).TypeE;
        public CellUnitLevelE UnitLevelE(in byte idx) => UnitEs(idx).LevelE;
        public CellUnitConditonE UnitConditionE(in byte idx) => UnitEs(idx).ConditionE;
        public CellUnitExtraToolWeaponE UnitExtraTWE(in byte idx) => UnitEs(idx).ExtraToolWeaponE;
        public CellUnitStatEs UnitStatEs(in byte idx) => UnitEs(idx).StatEs;
        public CellUnitStatHpE UnitStatHpE(in byte idx) => UnitStatEs(idx).Hp;
        public CellUnitStatStepE UnitStatStepE(in byte idx) => UnitStatEs(idx).StepE;
        public CellUnitStatWaterE UnitStatWaterE(in byte idx) => UnitStatEs(idx).WaterE;
        public CellUnitEffectEs UnitEffectEs(in byte idx) => UnitEs(idx).EffectEs;

        public CellBuildEs BuildEs(in byte idx) => CellEs(idx).BuildEs;
        public CellBuildingE BuildE(in byte idx) => BuildEs(idx).BuildingE;

        public CellEnvironmentEs EnvironmentEs(in byte idx) => CellEs(idx).EnvironmentEs;
        public CellEnvFertilizerE EnvFertilizerE(in byte idx) => EnvironmentEs(idx).Fertilizer;
        public CellEnvYoungForestE EnvYoungForestE(in byte idx) => EnvironmentEs(idx).YoungForest;
        public CellEnvAdultForestE EnvAdultForestE(in byte idx) => EnvironmentEs(idx).AdultForest;
        public CellEnvMountainE EnvMountainE(in byte idx) => EnvironmentEs(idx).Mountain;
        public CellEnvHillE EnvHillE(in byte idx) => EnvironmentEs(idx).Hill;
        public CellEnvFertilizerE EnvFertilizeE(in byte idx) => EnvironmentEs(idx).Fertilizer;

        public CellRiverEs RiverEs(in byte idx) => CellEs(idx).RiverEs;
        public CellTrailEs TrailEs(in byte idx) => CellEs(idx).TrailEs;
        public CellEffectEs EffectEs(in byte idx) => CellEs(idx).EffectEs;




        public readonly CellSpaceWorker CellSpaceWorker;
        public readonly WhereWorker WhereWorker;

        #endregion

        #endregion


        public Entities(in EcsWorld gameW, in List<object> forData, in List<string> namesMethods)
        {
            var i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, System.Action>)forData[i++];
            var sounds1 = (Dictionary<AbilityTypes, System.Action>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];


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

                for (var unit = UnitTypes.Scout; unit < UnitTypes.Camel; unit++)
                {
                    _scoutHeroCooldownEs.Add(unit.ToString() + player, new ScoutHeroCooldownE(gameW));
                }
            }

            _sounds0 = new Dictionary<ClipTypes, SoundE>();
            _sounds1 = new Dictionary<AbilityTypes, SoundE>();
            foreach (var item in sounds0) _sounds0.Add(item.Key, new SoundE(item.Value, gameW));
            foreach (var item in sounds1) _sounds1.Add(item.Key, new SoundE(item.Value, gameW));


            SelectedIdxE = new SelectedIdxE(gameW);
            CurrentIdxE = new CurrentIdxE(gameW);
            PreviousVisionIdxE = new PreviousVisionIdxE(gameW);

            WindCloudE = new WindCloudE(gameW);
            WinnerE = new WinnerE(gameW);
            WhoseMoveE = new WhoseMoveE(PlayerTypes.First, gameW);
            InputE = new InputE(gameW);
            FriendZoneE = new FriendZoneE(GameModeC.IsGameMode(GameModes.WithFriendOff), gameW);
            RpcE = new RpcE(actions, namesMethods);
            Motion = new MotionE(gameW);
            GameInfoE = new GameInfoE(gameW);
            InfoEnvironmentE = new InfoEnvironmentE(gameW);
            ClickerObjectE = new ClickerObjectE(CellClickTypes.SimpleClick, gameW);
            SunSidesE = new SunSidesE(SunSideTypes.Dawn, gameW);
            SelectedUnitE = new SelectedUnitE(gameW);
            SelectedAbilityE = new SelectedAbilityE(gameW);
            SelectedBuildingE = new SelectedBuildingE(gameW);
            SelectedToolWeaponE = new SelectedToolWeaponE(gameW);
            StartTeleportE = new StartTeleportE(gameW);
            EndTeleportE = new EndTeleportE(gameW);

            InventorUnitsEs = new InventorUnitsEs(gameW);
            InventorResourcesEs = new InventorResourcesEs(gameW);
            InventorToolWeaponEs = new InventorToolWeaponEs(gameW);

            UnitStatUpgradesEs = new UnitStatUpgradesEs(gameW);

            AvailableCenterUpgradeEs = new AvailableCenterUpgradeEs(gameW);
            BuildingUpgradeEs = new BuildingUpgradeEs(gameW);


            _cellEs = new CellEs[CellStartValues.ALL_CELLS_AMOUNT];
            byte idx = 0;
            for (byte x = 0; x < CellStartValues.X_AMOUNT; x++)
                for (byte y = 0; y < CellStartValues.Y_AMOUNT; y++)
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

            new MistakeE(gameW);
            new EntHint(gameW);
        }
    }
}