using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class CreateComps
    {
        public CreateComps()
        {
            new WindC(DirectTypes.Right);


            BuildsUpgC.Start();
            UnitUpgC.StartGame();
            new UnitAvailPickUpgC(true);
            new BuildAvailPickUpgC(new Dictionary<string, bool>());
            new WaterAvailPickUpgC(new Dictionary<PlayerTypes, bool>());


            new SetUnitCellsC(true);
            new ShiftCellsC(true);
            new ArsonCellsC(true);
            new AttackCellsC(true);
            new CellsGiveTWC(true);

            new WhereEnvC(true);
            new WhereUnitsC(true);
            new WhereBuildsC(true);

            new InvUnitsC(true);
            new InvResC(true);
            new InvTWC(true);

            new BackgroundC(BackgroundVC.Name);


            new WhoseMoveC(true);
            new ScoutHeroCooldownC(true);
            new CellClickC(default);
            new SelIdx(0);


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


            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
            {
                InvResC.Set(ResTypes.Food, PlayerTypes.Second, 999999);
            }
        }
    }
}