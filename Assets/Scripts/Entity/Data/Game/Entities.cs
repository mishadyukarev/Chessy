using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class Entities
    {
        readonly ActionC[] _sounds0;
        readonly ActionC[] _sounds1;
        readonly ResourcesC[] _mistakeEconomyEs;
        readonly InfoForPlayerE[] _forPlayerEs;

        public ref InfoForPlayerE ForPlayerE(in PlayerTypes player) => ref _forPlayerEs[(byte)player - 1];
        public ref ActionC Sound(in ClipTypes clip) => ref _sounds0[(int)clip - 1];
        public ref ActionC Sound(in AbilityTypes unique) => ref _sounds1[(int)unique - 1];
        public ref ResourcesC MistakeEconomy(in ResourceTypes resT) => ref _mistakeEconomyEs[(byte)resT - 1];

        public readonly RpcE RpcE;

        public PlayerTC WhoseMove;
        public PlayerTC WinnerC;
        public PlayerTC CurPlayerI;

        public LevelTC SelectedTWLevelTC;
        public LevelTC SelUnitLevelTC;

        public SunSideTC SunSideTC;
        public CellClickC CellClickTC;
        public ToolWeaponTC SelectedTWTC;
        public DirectTC DirectWindTC;
        public BuildingTC SelectedBuildingTC;
        public RayCastTC RayCastTC;
        public MistakeTC MistakeC;
        public TimerC TimerC;
        public UnitTC SelUnitTC;
        public AbilityTC SelAbilityTC;

        public IdxC StartTeleportIdxC;
        public IdxC EndTeleportIdxC;
        public IdxC CurrentIdxC;
        public IdxC SelectedIdxC;
        public IdxC PreviousVisionIdxC;
        public IdxC CenterCloudIdxC;

        public bool IsMyMove;
        public bool MotionIsActiveC;
        public bool EnvIsActiveC;
        public bool FriendIsActiveC;
        public bool IsStartedGame;
        public bool IsClickedC;

        public int MotionsC;

        #region Pools

        public readonly InventorResourcesEs InventorResourcesEs;
        public readonly InventorToolWeaponEs InventorToolWeaponEs;

        public readonly UnitStatUpgradesEs UnitStatUpgradesEs;
        public readonly AvailableCenterUpgradeEs AvailableCenterUpgradeEs;


        #region Cells

        readonly CellEs[] _cellEs;
        public byte LengthCells => (byte)_cellEs.Length;


        public ref CellEs CellEs(in byte idx) => ref _cellEs[idx];


        #region Unit

        public ref CellUnitEs UnitEs(in byte idx) => ref CellEs(idx).UnitEs;

        public ref UnitTC UnitTC(in byte idx) => ref UnitEs(idx).UnitTC;
        public ref PlayerTC UnitPlayerTC(in byte idx) => ref UnitEs(idx).PlayerTC;
        public ref LevelTC UnitLevelTC(in byte idx) => ref UnitEs(idx).LevelTC;
        public ref ConditionUnitTC UnitConditionTC(in byte idx) => ref UnitEs(idx).ConditionTC;
        public ref IsRightArcherC UnitIsRightArcherC(in byte idx) => ref UnitEs(idx).IsRightArcherC;
        public ref HealthC UnitHpC(in byte idx) => ref UnitEs(idx).HealthC;
        public ref StepsC UnitStepC(in byte idx) => ref UnitEs(idx).StepC;
        public ref WaterC UnitWaterC(in byte idx) => ref UnitEs(idx).WaterC;
        public ref ToolWeaponTC UnitMainTWTC(in byte idx) => ref UnitEs(idx).MainToolWeaponTC;
        public ref LevelTC UnitMainTWLevelTC(in byte idx) => ref UnitEs(idx).MainLevelTC;
        public ref ToolWeaponTC UnitExtraTWTC(in byte idx) => ref UnitEs(idx).ExtraToolWeaponTC;
        public ref LevelTC UnitExtraLevelTC(in byte idx) => ref UnitEs(idx).ExtraTWLevelTC;
        public ref ProtectionC UnitExtraProtectionShieldTC(in byte idx) => ref UnitEs(idx).ExtraTWShieldC;


        #region Effects

        public ref StunC UnitStunC(in byte idx) => ref UnitEs(idx).StunC;
        public ref ProtectionC UnitEffectShield(in byte idx) => ref UnitEs(idx).ShieldEffectC;
        public ref FrozenArrawC UnitFrozenArrawC(in byte idx) => ref UnitEs(idx).FrozenArrawC;

        #endregion

        #endregion

        public ref CellWhoLastDiedHereE LastDiedE(in byte idx) => ref CellEs(idx).WhoLastDiedHereE;
        public ref UnitTC LastDiedUnitTC(in byte idx) => ref LastDiedE(idx).UnitTC;
        public ref LevelTC LastDiedLevelTC(in byte idx) => ref LastDiedE(idx).LevelTC;
        public ref PlayerTC LastDiedPlayerTC(in byte idx) => ref LastDiedE(idx).PlayerTC;

        public ref CellBuildingE BuildE(in byte idx) => ref CellEs(idx).BuildE;
        public ref BuildingTC BuildTC(in byte idx) => ref BuildE(idx).BuildingC;
        public ref HealthC BuildHpC(in byte idx) => ref BuildE(idx).HealthC;
        public ref PlayerTC BuildPlayerTC(in byte idx) => ref BuildE(idx).PlayerC;
        public ref bool BuildSmelterTC(in byte idx) => ref BuildE(idx).IsActiveSmelter;

        public ref CellEnvironmentEs EnvironmentEs(in byte idx) => ref CellEs(idx).EnvironmentEs;
        public ref ResourcesC YoungForestC(in byte idx) => ref EnvironmentEs(idx).YoungForestC;
        public ref ResourcesC AdultForestC(in byte idx) => ref EnvironmentEs(idx).AdultForestC;
        public ref ResourcesC MountainC(in byte idx) => ref EnvironmentEs(idx).MountainC;
        public ref ResourcesC HillC(in byte idx) => ref EnvironmentEs(idx).HillC;
        public ref ResourcesC FertilizeC(in byte idx) => ref EnvironmentEs(idx).FertilizeC;

        public ref CellRiverEs RiverEs(in byte idx) => ref CellEs(idx).RiverEs;
        public ref CellTrailEs TrailEs(in byte idx) => ref CellEs(idx).TrailEs;

        public ref CellEffectE EffectEs(in byte idx) => ref CellEs(idx).EffectEs;
        public ref bool HaveFire(in byte idx) => ref EffectEs(idx).HaveFire;




        public readonly CellSpaceWorker CellSpaceWorker;
        public readonly WhereWorker WhereWorker;

        #endregion

        #endregion

        public Entities(in EcsWorld gameW, in List<object> forData, in List<string> namesMethods)
        {
            CenterCloudIdxC.Idx = 60;
            FriendIsActiveC = GameModeC.IsGameMode(GameModes.WithFriendOff);
            DirectWindTC = new DirectTC(DirectTypes.Right);
            SunSideTC = new SunSideTC(SunSideTypes.Dawn);
            WhoseMove = new PlayerTC(PlayerTypes.First);
            CellClickTC = new CellClickC(CellClickTypes.SimpleClick);
            SelectedTWTC = new ToolWeaponTC(ToolWeaponTypes.Axe);
            SelectedTWLevelTC = new LevelTC(LevelTypes.First);



            var i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, System.Action>)forData[i++];
            var sounds1 = (Dictionary<AbilityTypes, System.Action>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];


            _forPlayerEs = new InfoForPlayerE[(byte)PlayerTypes.End];

            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _forPlayerEs[(byte)player - 1] = new InfoForPlayerE(true);

                for (var build = BuildingTypes.None + 1; build < BuildingTypes.End; build++)
                {
                }
            }
            _mistakeEconomyEs = new ResourcesC[(byte)ResourceTypes.End];


            _sounds0 = new ActionC[(int)ClipTypes.End];
            _sounds1 = new ActionC[(int)AbilityTypes.End];
            foreach (var item in sounds0) _sounds0[(int)item.Key - 1] = new ActionC(item.Value);
            foreach (var item in sounds1) _sounds1[(int)item.Key - 1] = new ActionC(item.Value);

            RpcE = new RpcE(actions, namesMethods);

            InventorResourcesEs = new InventorResourcesEs(gameW);
            InventorToolWeaponEs = new InventorToolWeaponEs(gameW);

            UnitStatUpgradesEs = new UnitStatUpgradesEs(gameW);

            AvailableCenterUpgradeEs = new AvailableCenterUpgradeEs(gameW);




            _cellEs = new CellEs[StartValues.ALL_CELLS_AMOUNT];
            byte idx = 0;
            for (byte x = 0; x < StartValues.X_AMOUNT; x++)
                for (byte y = 0; y < StartValues.Y_AMOUNT; y++)
                {
                    _cellEs[idx] = new CellEs(isActiveParenCells[idx], idCells[idx], new byte[] { x, y }, idx, gameW);
                    ++idx;
                }
            CellSpaceWorker = new CellSpaceWorker(_cellEs);
            WhereWorker = new WhereWorker(_cellEs);


            new CellsForSetUnitsEs(gameW);
            new EntHint(gameW);
        }
    }
}