using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct EntityPool
    {
        readonly static Dictionary<string, Entity> _entsTest;

        public static ref C SelIdx<C>() where C : struct, ISelectedIdx => ref _entsTest[nameof(ISelectedIdx)].Get<C>();
        public static ref C CurIdx<C>() where C : struct, ICurrectIdx => ref _entsTest[nameof(ICurrectIdx)].Get<C>();
        public static ref C PreVisIdx<C>() where C : struct, IPreVisionIdx => ref _entsTest[nameof(IPreVisionIdx)].Get<C>();
        public static ref C Input<C>() where C : struct, IInputE => ref _entsTest[nameof(IInputE)].Get<C>();
        public static ref C ClickerObject<C>() where C : struct, IClickerObjectE => ref _entsTest[nameof(IClickerObjectE)].Get<C>();

        static EntityPool()
        {
            _entsTest = new Dictionary<string, Entity>();
        }

        public EntityPool(in WorldEcs curGameW, in string nameBackground)
        {


            _entsTest[nameof(ISelectedIdx)] = curGameW.NewEntity()
                .Add(new SelIdxC())
                .Add(new IdxC(0));

            _entsTest[nameof(ICurrectIdx)] = curGameW.NewEntity()
                .Add(new CurIdxC())
                .Add(new IdxC(0));

            _entsTest[nameof(IPreVisionIdx)] = curGameW.NewEntity()
                .Add(new PreVisIdxC())
                .Add(new IdxC(0));

            _entsTest[nameof(IInputE)] = curGameW.NewEntity()
                .Add(new ClickC());

            _entsTest[nameof(IClickerObjectE)] = curGameW.NewEntity()
                .Add(new CellClickC(CellClickTypes.SimpleClick))
                .Add(new RayCastC());


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
}