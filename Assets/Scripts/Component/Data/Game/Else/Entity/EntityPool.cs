using ECS;
using Game.Common;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        readonly static Dictionary<string, Entity> _ents;
        readonly static Dictionary<ClipTypes, Entity> _sounds0;
        readonly static Dictionary<UniqueAbilityTypes, Entity> _sounds1;

        public static ref C SelIdx<C>() where C : struct, ISelectedIdx => ref _ents[nameof(ISelectedIdx)].Get<C>();
        public static ref C CurIdx<C>() where C : struct, ICurrectIdx => ref _ents[nameof(ICurrectIdx)].Get<C>();
        public static ref C PreVisIdx<C>() where C : struct, IPreVisionIdx => ref _ents[nameof(IPreVisionIdx)].Get<C>();
        public static ref C Input<C>() where C : struct, IInputE => ref _ents[nameof(IInputE)].Get<C>();
        public static ref C ClickerObject<C>() where C : struct, IClickerObjectE => ref _ents[nameof(IClickerObjectE)].Get<C>();
        public static ref C Rpc<C>() where C : struct, IRpc => ref _ents[nameof(Rpc)].Get<C>();

        public static ref C Sound<C>(ClipTypes clip) where C : struct
        {
            if (!_sounds0.ContainsKey(clip)) throw new Exception();
            return ref _sounds0[clip].Get<C>();
        }
        public static ref C Sound<C>(UniqueAbilityTypes clip) where C : struct
        {
            if (!_sounds1.ContainsKey(clip)) throw new Exception();
            return ref _sounds1[clip].Get<C>();
        }


        static EntityPool()
        {
            _ents = new Dictionary<string, Entity>();
            _sounds0 = new Dictionary<ClipTypes, Entity>();
            _sounds1 = new Dictionary<UniqueAbilityTypes, Entity>();
        }

        public EntityPool(in WorldEcs curGameW, in string nameBackground, in List<object> actions, in List<string> namesMethods, in Dictionary<ClipTypes, System.Action> action0, in Dictionary<UniqueAbilityTypes, System.Action> action1)
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



            foreach (var item in action0) _sounds0.Add(item.Key, curGameW.NewEntity().Add(new ActionC(item.Value)));
            foreach (var item in action1) _sounds1.Add(item.Key, curGameW.NewEntity().Add(new ActionC(item.Value)));



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