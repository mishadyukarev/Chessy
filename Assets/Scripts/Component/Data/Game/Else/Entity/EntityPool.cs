using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        readonly static Dictionary<string, Entity> _ents;

        public static ref C SelIdx<C>() where C : struct, ISelectedIdx => ref _ents[nameof(ISelectedIdx)].Get<C>();
        public static ref C CurIdx<C>() where C : struct, ICurrectIdx => ref _ents[nameof(ICurrectIdx)].Get<C>();
        public static ref C PreVisIdx<C>() where C : struct, IPreVisionIdx => ref _ents[nameof(IPreVisionIdx)].Get<C>();
        public static ref C Input<C>() where C : struct, IInputE => ref _ents[nameof(IInputE)].Get<C>();
        public static ref C ClickerObject<C>() where C : struct, IClickerObjectE => ref _ents[nameof(IClickerObjectE)].Get<C>();
        public static ref C Rpc<C>() where C : struct, IRpc => ref _ents[nameof(Rpc)].Get<C>();


        static EntityPool()
        {
            _ents = new Dictionary<string, Entity>();
        }

        public EntityPool(in WorldEcs curGameW, in string nameBackground, in List<object> actions, in List<string> namesMethods)
        {
            _ents[nameof(ISelectedIdx)] = curGameW.NewEntity()
                .Add(new SelIdxC())
                .Add(new IdxC(0));

            _ents[nameof(ICurrectIdx)] = curGameW.NewEntity()
                .Add(new CurIdxC())
                .Add(new IdxC(0));

            _ents[nameof(IPreVisionIdx)] = curGameW.NewEntity()
                .Add(new PreVisIdxC())
                .Add(new IdxC(0));

            _ents[nameof(IInputE)] = curGameW.NewEntity()
                .Add(new ClickC());

            _ents[nameof(IClickerObjectE)] = curGameW.NewEntity()
                .Add(new CellClickC(CellClickTypes.SimpleClick))
                .Add(new RayCastC());



            _ents[nameof(Rpc)] = curGameW.NewEntity()
                .Add(new RpcC(actions, namesMethods));


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

            new BackgroundC(nameBackground);


            new WhoseMoveC(true);
            new ScoutHeroCooldownC(true);


            new PlayerWinnerC(default);
            new ReadyC(new Dictionary<PlayerTypes, bool>());
            new MotionsC(0);
            new MistakeC(new Dictionary<ResTypes, int>());

            new HintC(new Dictionary<VideoClipTypes, bool>());
            new PickUpgC(new Dictionary<PlayerTypes, bool>());
            new GetterUnitsC(new Dictionary<UnitTypes, bool>());
            new EnvInfoC();
            new BuildAbilC(true);
            new FriendC(GameModesCom.IsGameMode(GameModes.WithFriendOff));
        }
    }

    public interface IRpc { }
}