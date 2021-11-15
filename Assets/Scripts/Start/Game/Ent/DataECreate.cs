using Chessy.Common;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class DataECreate : IEcsInitSystem
    {
        private EcsWorld _curGameW = default;

        private EcsFilter<CellVC> _cellVF = default;

        public void Init()
        {
            byte curIdx = 0;

            for (byte x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (byte y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    _curGameW.NewEntity()
                        .Replace(new XyC(curIdx, new byte[] { x, y }))
                        .Replace(new CellC(_cellVF.Get1(curIdx).Cell))
                        .Replace(new EnvC(new Dictionary<EnvTypes, bool>()))
                        .Replace(new EnvResC(true))
                        .Replace(new FireC())
                        .Replace(new CloudC())
                        .Replace(new RiverC(true));


                    _curGameW.NewEntity()
                         .Replace(new BuildC())
                         .Replace(new OwnerC())
                         .Replace(new VisibleC(true));



                    #region Unit

                    _curGameW.NewEntity()
                         .Replace(new UnitC())
                         .Replace(new LevelC())
                         .Replace(new OwnerC())
                         .Replace(new VisibleC(true));



                    _curGameW.NewEntity()
                         .Replace(new ConditionUnitC())
                         .Replace(new MoveInCondC(true))
                         .Replace(new UnitEffectsC(true))
                         .Replace(new StunC());


                    _curGameW.NewEntity()
                         .Replace(new HpC())
                         .Replace(new DamageC())
                         .Replace(new StepC())
                         .Replace(new WaterUnitC());


                    _curGameW.NewEntity()
                         .Replace(new UniqAbilC(true))
                         .Replace(new CooldownUniqC(true));


                    _curGameW.NewEntity()
                        .Replace(new ToolWeaponC());


                    _curGameW.NewEntity()
                         .Replace(new CornerArcherC());

                    #endregion


                    _curGameW.NewEntity()
                        .Replace(new TrailC(new Dictionary<DirectTypes, int>()))
                        .Replace(new VisibleC(true));


                    ++curIdx;
                }

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var audioSourceParentGO = new GameObject("AudioSource");
            //audioSourceParentGO.transform.SetParent(genZone.transform);


            new WindC(DirectTypes.Right);
            new SoundEffectC(audioSourceParentGO);

            new BuildsUpgC(true);
            new UnitPercUpgC(true);
            new UnitStepUpgC(new Dictionary<PlayerTypes, Dictionary<UnitTypes, int>>());

            new CellsForSetUnitC(true);
            new CellsForShiftCom(true);
            new CellsArsonArcherComp(true);
            new CellsAttackC(true);
            new CellsGiveTWComp(true);

            //new WhereCloudsC(true);
            new WhereEnvC(true);
            new WhereUnitsC(true);

            new InvUnitsC(true);
            new InvResC(true);
            new InvToolWeapC(true);


            new WhoseMoveC(true);
            new ScoutHeroCooldownC(true);
            new CellClickC(default);
            new IdxSel(0);
            //new SelUniqAbilC(default);


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
                InvResC.Set(PlayerTypes.Second, ResTypes.Food, 999999);
            }
        }
    }
}