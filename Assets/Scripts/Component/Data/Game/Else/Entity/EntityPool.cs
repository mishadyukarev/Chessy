using Game.Common;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        readonly static Dictionary<string, EcsEntity> _ents;
        readonly static Dictionary<string, Entity> _entsTest;

        public static ref C SelIdx<C>() where C : struct, ISelectedIdx => ref _ents[nameof(ISelectedIdx)].Get<C>();
        public static ref C CurIdx<C>() where C : struct, ICurrectIdx => ref _ents[nameof(ICurrectIdx)].Get<C>();
        public static ref C PreVisIdx<C>() where C : struct, IPreVisionIdx => ref _ents[nameof(IPreVisionIdx)].Get<C>();
        public static ref C Input<C>() where C : struct, IInputE => ref _ents[nameof(IInputE)].Get<C>();
        public static ref C ClickerObject<C>() where C : struct, IClickerObjectE => ref _ents[nameof(IClickerObjectE)].Get<C>();

        static EntityPool()
        {
            _ents = new Dictionary<string, EcsEntity>();
            _entsTest = new Dictionary<string, Entity>();
        }

        public EntityPool(in EcsWorld curGameW, in string nameBackground)
        {
            _entsTest[nameof(ISelectedIdx)] = new Entity(new Dictionary<string, object>())
                .AddComponent(new SelIdxC())
                .AddComponent(new IdxC(0));


            //_ents[nameof(ISelectedIdx)] = curGameW.NewEntity()
            //    .Replace(new SelIdxC())
            //    .Replace(new IdxC(0));

            _ents[nameof(ICurrectIdx)] = curGameW.NewEntity()
                .Replace(new CurIdxC())
                .Replace(new IdxC(0));

            _ents[nameof(IPreVisionIdx)] = curGameW.NewEntity()
                .Replace(new PreVisIdxC())
                .Replace(new IdxC(0));

            _ents[nameof(IInputE)] = curGameW.NewEntity()
                .Replace(new ClickC());

            _ents[nameof(IClickerObjectE)] = curGameW.NewEntity()
                .Replace(new CellClickC(default));


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
            new CellClickC(default);


            new PlyerWinnerC(default);
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

    //public struct WorldEcs
    //{
    //    Dictionary<int, Entity> _ents;

    //    public WorldEcs(in Dictionary<int, Entity> ents) => _ents = ents;

    //    public Entity NewEntity()
    //    {
    //        _ents[_ents.Count] = new Entity(new Dictionary<string, object>());
    //        return _ents[_ents.Count];
    //    }
    //}

    public struct Entity
    {
        int _index;

        Dictionary<string, object> _components;

        public Entity(in Dictionary<string, object> components)
        {
            _index = default;///////////////////
            _components = components;
        }

        public Entity AddComponent<T>(T component) where T : struct
        {
            _components[nameof(T)] = component;
            return this;
        }
        //public ref T Component<T>() where T : struct
        //{
        //    ref (T)_components[nameof(T)];
        //}
    }

    public struct ComponentPool<T>
    {
        Dictionary<int, T> _components;

        public ComponentPool(in Dictionary<int, T> dict)
        {
            _components = dict;
        }
    }
}