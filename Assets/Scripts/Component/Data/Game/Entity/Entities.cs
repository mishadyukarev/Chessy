using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public struct Entities
    {
        static Dictionary<string, ScoutHeroCooldownE> _scoutHeroCooldownEs;
        static Dictionary<PlayerTypes, ReadyE> _ready;
        static Dictionary<ClipTypes, SoundE> _sounds0;
        static Dictionary<AbilityTypes, SoundE> _sounds1;

        public static SelectedIdxE SelectedIdxE { get; private set; }
        public static CurrentIdxE CurrentIdxE { get; private set; }
        public static PreviousVisionIdxE PreviousVisionIdxE { get; private set; }
        public static WindE WindE { get; private set; }
        public static WinnerE WinnerE { get; private set; }
        public static WhoseMoveE WhoseMove { get; private set; }
        public static InputE InputE { get; private set; }
        public static FriendZoneE FriendZoneE { get; private set; }
        public static RpcE Rpc { get; private set; }
        public static MotionE Motion { get; private set; }
        public static GameInfoE GameInfo { get; private set; }
        public static InfoEnvironmentE InfoEnvironment { get; private set; }
        public static ClickerObjectE ClickerObject { get; private set; }
        public static SunSidesE SunSidesE { get; private set; }
        public static SelectedUnitE SelectedUnitE { get; private set; }
        public static SelectedUniqueAbilityE SelectedUniqueAbilityE { get; private set; }

        public static ScoutHeroCooldownE ScoutHeroCooldownE(in UnitTypes unit, in PlayerTypes player) => _scoutHeroCooldownEs[unit.ToString() + player];
        public static ReadyE Ready(in PlayerTypes player) => _ready[player];
        public static SoundE Sound(in ClipTypes clip) => _sounds0[clip];
        public static SoundE Sound(in AbilityTypes unique) => _sounds1[unique];


        public Entities(in EcsWorld gameW, in List<object> forData, in List<string> namesMethods, out int i)
        {
            i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, System.Action>)forData[i++];
            var sounds1 = (Dictionary<AbilityTypes, System.Action>)forData[i++];


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

            _scoutHeroCooldownEs = new Dictionary<string, ScoutHeroCooldownE>();
            _ready = new Dictionary<PlayerTypes, ReadyE>();
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _ready.Add(player, new ReadyE(gameW));

                for (var unit = UnitTypes.Scout; unit <= UnitTypes.Snowy; unit++)
                {
                    _scoutHeroCooldownEs.Add(unit.ToString() + player, new ScoutHeroCooldownE(gameW));
                }
            }

            _sounds0 = new Dictionary<ClipTypes, SoundE>();
            _sounds1 = new Dictionary<AbilityTypes, SoundE>();
            foreach (var item in sounds0) _sounds0.Add(item.Key, new SoundE(item.Value, gameW));
            foreach (var item in sounds1) _sounds1.Add(item.Key, new SoundE(item.Value, gameW));


            new AvailableCenterHeroEs(gameW);
            new UnitStatUpgradesEs(gameW);
            new BuildingUpgradesEs(gameW);


            new EntWhereEnviroments(gameW);
            new WhereUnitsE(gameW);
            new WhereBuildsE(gameW);

            new InventorUnitsE(gameW);
            new InventorResourcesE(gameW);
            new InventorToolWeaponE(gameW);

            new CellsForSetUnitsEs(gameW);
            new CellsForShiftUnitsEs(gameW);
            new CellsForAttackUnitsEs(gameW);
            new CellsForArsonArcherEs(gameW);

            new SelectedToolWeaponE(gameW);
            new MistakeE(gameW);
            new EntHint(gameW);
           
            new StatUnitsUpgradesE(gameW);
            new GetterUnitsEs(gameW);    
        }
    }
}