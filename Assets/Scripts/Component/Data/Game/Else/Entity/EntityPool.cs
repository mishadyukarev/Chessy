using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        readonly static Dictionary<string, Entity> _ents;
        static Entity _background;
        static Dictionary<bool, Entity> _whoseMove;
        static Entity _winner;
        static Dictionary<PlayerTypes, Entity> _ready;
        static Entity _gameInfo;
        static Entity _motionZone;
        static Entity _friendZone;
        static Entity _infoEnvironment;

        public static ref C SelIdx<C>() where C : struct, ISelectedIdx => ref _ents[nameof(ISelectedIdx)].Get<C>();
        public static ref C CurIdx<C>() where C : struct, ICurrectIdx => ref _ents[nameof(ICurrectIdx)].Get<C>();
        public static ref C PreVisIdx<C>() where C : struct, IPreVisionIdx => ref _ents[nameof(IPreVisionIdx)].Get<C>();
        public static ref C Input<C>() where C : struct, IInputE => ref _ents[nameof(IInputE)].Get<C>();
        public static ref C ClickerObject<C>() where C : struct, IClickerObjectE => ref _ents[nameof(IClickerObjectE)].Get<C>();
        public static ref C Rpc<C>() where C : struct, IRpc => ref _ents[nameof(Rpc)].Get<C>();
        public static ref C Background<C>() where C : struct => ref _background.Get<C>();
        public static ref C WhoseMove<C>(in bool isOffline) where C : struct => ref _whoseMove[isOffline].Get<C>();
        public static ref C Winner<C>() where C : struct => ref _winner.Get<C>();
        public static ref C Ready<C>(in PlayerTypes player) where C : struct => ref _ready[player].Get<C>();
        public static ref C GameInfo<C>() where C : struct => ref _gameInfo.Get<C>();
        public static ref C MotionZone<C>() where C : struct => ref _motionZone.Get<C>();
        public static ref C FriendZone<C>() where C : struct => ref _friendZone.Get<C>();
        public static ref C EnvironmentInfo<C>() where C : struct => ref _infoEnvironment.Get<C>();

        static EntityPool()
        {
            _ents = new Dictionary<string, Entity>();
            _whoseMove = new Dictionary<bool, Entity>();
            _ready = new Dictionary<PlayerTypes, Entity>();

            _whoseMove.Add(true, default);
            _whoseMove.Add(false, default);

            for (var player = PlayerTypes.Start; player <= PlayerTypes.End; player++)
                _ready.Add(player, default);
        }

        public EntityPool(in EcsWorld gameW, in string nameBackground, in List<object> actions, in List<string> namesMethods)
        {
            _ents[nameof(ISelectedIdx)] = gameW.NewEntity()
                .Add(new SelIdxEC())
                .Add(new IdxC(0));

            _ents[nameof(ICurrectIdx)] = gameW.NewEntity()
                .Add(new CurIdxC())
                .Add(new IdxC(0));

            _ents[nameof(IPreVisionIdx)] = gameW.NewEntity()
                .Add(new PreVisIdxC())
                .Add(new IdxC(0));

            _ents[nameof(IInputE)] = gameW.NewEntity()
                .Add(new ClickC());

            _ents[nameof(IClickerObjectE)] = gameW.NewEntity()
                .Add(new CellClickC(CellClickTypes.SimpleClick))
                .Add(new RayCastC());

            _ents[nameof(Rpc)] = gameW.NewEntity()
                .Add(new RpcC(actions, namesMethods));

            _background = gameW.NewEntity()
                .Add(new NameC(nameBackground));

            _whoseMove[true] = gameW.NewEntity()
                .Add(new PlayerC());

            _whoseMove[false] = gameW.NewEntity()
                .Add(new PlayerC());

            _winner = gameW.NewEntity()
                .Add(new PlayerC());

            _ready[PlayerTypes.First] = gameW.NewEntity()
                .Add(new IsStartedGameC())
                .Add(new IsReadyC());

            _ready[PlayerTypes.Second] = gameW.NewEntity()
                .Add(new IsReadyC());

            _gameInfo = gameW.NewEntity()
                .Add(new IsStartedGameC())
                .Add(new AmountMotionsC(0));

            _motionZone = gameW.NewEntity()
                .Add(new IsActivatedC(false));

            _friendZone = gameW.NewEntity()
                .Add(new IsActivatedC(GameModeC.IsGameMode(GameModes.WithFriendOff)));

            _infoEnvironment = gameW.NewEntity()
                .Add(new IsActivatedC(false));

            new WhoseMoveC(true);


            new WindC(DirectTypes.Right);


            BuildsUpgC.Start();
            UnitUpgC.StartGame();
            new UnitAvailPickUpgC(true);
            new BuildAvailPickUpgC(new Dictionary<string, bool>());
            new WaterAvailPickUpgC(new Dictionary<PlayerTypes, bool>());


            new SetUnitCellsC(true);
            new AttackCellsC(true);
            new CellsGiveTWC(true);

            new WhereEnvC(true);
            new WhereUnitsC(true);
            new WhereBuildsC(true);

            new InvUnitsC(true);
            new InvResC(true);
            new InvTWC(true);


            
            new ScoutHeroCooldownC(true);

            new MistakeC(new Dictionary<ResTypes, int>());

            new HintC(new Dictionary<VideoClipTypes, bool>());
            new PickUpgC(new Dictionary<PlayerTypes, bool>());
            new GetterUnitsC(new Dictionary<UnitTypes, bool>());
            new BuildAbilC(true);
        }
    }

    public interface IRpc { }
}