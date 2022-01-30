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

        public ScoutHeroCooldownE ScoutHeroCooldownE(in UnitTypes unit, in PlayerTypes player) => _scoutHeroCooldownEs[unit.ToString() + player];
        public ScoutHeroCooldownE ScoutHeroCooldownE(in CellUnitMainE unitMainE) => _scoutHeroCooldownEs[unitMainE.UnitC.Unit.ToString() + unitMainE.OwnerC.Player];
        public ReadyE Ready(in PlayerTypes player) => _ready[player];
        public SoundE Sound(in ClipTypes clip) => _sounds0[clip];
        public SoundE Sound(in AbilityTypes unique) => _sounds1[unique];
        public AvailableCenterHeroE AvailableCenterHero(in PlayerTypes player) => _availHero[player];


        public readonly CurrentIdxE CurrentIdxE;
        public readonly SelectedIdxE SelectedIdxE;
        public readonly SunSidesE SunSidesE;
        public readonly MotionE Motion;
        public readonly InputE InputE;
        public readonly PreviousVisionIdxE PreviousVisionIdxE;
        public readonly WinnerE WinnerE;
        public readonly WindE WindE;
        public readonly FriendZoneE FriendZoneE;
        public readonly GameInfoE GameInfo;
        public readonly InfoEnvironmentE InfoEnvironment;
        public readonly SelectedUnitE SelectedUnitE;
        public readonly ClickerObjectE ClickerObject;
        public readonly WhoseMoveE WhoseMove;
        public readonly SelectedUniqueAbilityE SelectedUniqueAbilityE;
        public readonly RpcE Rpc;
        public readonly SelectedToolWeaponE SelectedToolWeaponE;


        #region Pools

        public readonly WhereBuildingEs WhereBuildingEs;
        public readonly WhereEnviromentEs WhereEnviromentEs;
        public readonly WhereUnitsEs WhereUnitsEs;

        public readonly InventorUnitsEs InventorUnitsEs;
        public readonly InventorResourcesEs InventorResourcesEs;
        public readonly InventorToolWeaponEs InventorToolWeaponEs;

        public readonly UnitStatUpgradesEs UnitStatUpgradesEs;
        public readonly AvailableCenterUpgradeEs AvailableCenterUpgradeEs;
        public readonly MasterEs MasterEs;
        public readonly BuildingUpgradeEs BuildingUpgradeEs;
        public readonly CellEs CellEs;

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
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _ready.Add(player, new ReadyE(gameW));
                _availHero.Add(player, new AvailableCenterHeroE(true, gameW));

                for (var unit = UnitTypes.Scout; unit <= UnitTypes.Snowy; unit++)
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

            WindE = new WindE(gameW);
            WinnerE = new WinnerE(gameW);
            WhoseMove = new WhoseMoveE(PlayerTypes.First, gameW);
            InputE = new InputE(gameW);
            FriendZoneE = new FriendZoneE(GameModeC.IsGameMode(GameModes.WithFriendOff), gameW);
            Rpc = new RpcE(actions, namesMethods);
            Motion = new MotionE(gameW);
            GameInfo = new GameInfoE(gameW);
            InfoEnvironment = new InfoEnvironmentE(gameW);
            ClickerObject = new ClickerObjectE(CellClickTypes.SimpleClick, gameW);
            SunSidesE = new SunSidesE(SunSideTypes.Dawn, gameW);
            SelectedUnitE = new SelectedUnitE(gameW);
            SelectedUniqueAbilityE = new SelectedUniqueAbilityE(gameW);
            SelectedToolWeaponE = new SelectedToolWeaponE(gameW);

            WhereBuildingEs = new WhereBuildingEs(gameW);
            WhereEnviromentEs = new WhereEnviromentEs(gameW);
            WhereUnitsEs = new WhereUnitsEs(gameW);

            InventorUnitsEs = new InventorUnitsEs(gameW);
            InventorResourcesEs = new InventorResourcesEs(gameW);
            InventorToolWeaponEs = new InventorToolWeaponEs(gameW);

            UnitStatUpgradesEs = new UnitStatUpgradesEs(gameW);
            CellEs = new CellEs(gameW, isActiveParenCells, idCells);
            AvailableCenterUpgradeEs = new AvailableCenterUpgradeEs(gameW);
            BuildingUpgradeEs = new BuildingUpgradeEs(gameW);
            MasterEs = new MasterEs(gameW);


            new CellsForSetUnitsEs(gameW);
            new CellsForShiftUnitsEs(gameW);
            new CellsForAttackUnitsEs(gameW);
            new CellsForArsonArcherEs(gameW);

            new MistakeE(gameW);
            new EntHint(gameW); 
            new GetterUnitsEs(gameW);
        }
    }
}