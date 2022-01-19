using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        static Dictionary<string, Entity> _ents;
        static Entity _background;

        static Entity _winner;
        static Dictionary<PlayerTypes, Entity> _ready;
        static Entity _gameInfo;
        static Entity _motionZone;
        static Entity _friendZone;
        static Entity _infoEnvironment;
        static Dictionary<string, Entity> _scoutHeroCooldown;


      
        public static ref C PreVisIdx<C>() where C : struct, IPreVisionIdx => ref _ents[nameof(IPreVisionIdx)].Get<C>();
        public static ref C Input<C>() where C : struct, IInputE => ref _ents[nameof(IInputE)].Get<C>();
        public static ref C ClickerObject<C>() where C : struct, IClickerObjectE => ref _ents[nameof(IClickerObjectE)].Get<C>();
        public static ref C Rpc<C>() where C : struct, IRpc => ref _ents[nameof(Rpc)].Get<C>();
        public static ref C Background<C>() where C : struct => ref _background.Get<C>();
        public static ref C Winner<C>() where C : struct => ref _winner.Get<C>();
        public static ref C Ready<C>(in PlayerTypes player) where C : struct => ref _ready[player].Get<C>();
        public static ref C GameInfo<C>() where C : struct => ref _gameInfo.Get<C>();
        public static ref C MotionZone<C>() where C : struct => ref _motionZone.Get<C>();
        public static ref C FriendZone<C>() where C : struct => ref _friendZone.Get<C>();
        public static ref C EnvironmentInfo<C>() where C : struct => ref _infoEnvironment.Get<C>();
        public static ref C ScoutHeroCooldown<C>(in UnitTypes unit, in PlayerTypes player) where C : struct => ref _scoutHeroCooldown[unit.ToString() + player].Get<C>();
        

        public EntityPool(in EcsWorld gameW, in string nameBackground, in List<object> actions, in List<string> namesMethods)
        {
            _ents = new Dictionary<string, Entity>();
            _ready = new Dictionary<PlayerTypes, Entity>();
            _scoutHeroCooldown = new Dictionary<string, Entity>();



            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _ready.Add(player, default);

                _scoutHeroCooldown.Add(UnitTypes.Scout.ToString() + player, default);
                _scoutHeroCooldown.Add(UnitTypes.Elfemale.ToString() + player, default);
            }


            _ents[nameof(IPreVisionIdx)] = gameW.NewEntity()
                .Add(new IdxC(0));

            _ents[nameof(IInputE)] = gameW.NewEntity()
                .Add(new IsClickedC());

            _ents[nameof(IClickerObjectE)] = gameW.NewEntity()
                .Add(new CellClickC(CellClickTypes.SimpleClick))
                .Add(new RayCastC());

            _ents[nameof(Rpc)] = gameW.NewEntity()
                .Add(new RpcC(actions, namesMethods));

            _background = gameW.NewEntity()
                .Add(new NameC(nameBackground));

            _winner = gameW.NewEntity()
                .Add(new PlayerTC());

            _ready[PlayerTypes.First] = gameW.NewEntity()
                .Add(new IsStartedGameC())
                .Add(new IsReadyC());

            _ready[PlayerTypes.Second] = gameW.NewEntity()
                .Add(new IsReadyC());

            _gameInfo = gameW.NewEntity()
                .Add(new IsStartedGameC())
                .Add(new AmountMotionsC(0));

            _motionZone = gameW.NewEntity()
                .Add(new IsActiveC(false));

            _friendZone = gameW.NewEntity()
                .Add(new IsActiveC(GameModeC.IsGameMode(GameModes.WithFriendOff)));

            _infoEnvironment = gameW.NewEntity()
                .Add(new IsActiveC(false));


            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _scoutHeroCooldown[UnitTypes.Scout.ToString() + player] = gameW.NewEntity()
                    .Add(new CooldownC());

                _scoutHeroCooldown[UnitTypes.Elfemale.ToString() + player] = gameW.NewEntity()
                    .Add(new CooldownC());
            }
        }
    }

    public interface IRpc { }
}