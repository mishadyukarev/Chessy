using ECS;
using Game.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace Game.Game
{
    public struct EntityUIPool
    {
        readonly static Dictionary<CanvasEntTypes, Entity> _uI;
        readonly static Dictionary<ResTypes, Entity> _economyUp;


        //Up
        public static ref T EconomyUp<T>(in ResTypes res) where T : struct => ref _economyUp[res].Get<T>();
        public static ref T AlphaUp<T>() where T : struct => ref _uI[CanvasEntTypes.Alpha].Get<T>();
        public static ref T LeaveUp<T>() where T : struct => ref _uI[CanvasEntTypes.Leave].Get<T>();
        public static ref T DirectWindUp<T>() where T : struct => ref _uI[CanvasEntTypes.DirectWind].Get<T>();

        //Center
        public static ref T EndGameCenter<T>() where T : struct => ref _uI[CanvasEntTypes.EndGame].Get<T>();
        public static ref T MotionCenter<T>() where T : struct => ref _uI[CanvasEntTypes.Motion].Get<T>();
        public static ref T JoinDiscordCenter<T>() where T : struct => ref _uI[CanvasEntTypes.JoinDiscord].Get<T>();
        public static ref T ReadyCenter<T>() where T : struct => ref _uI[CanvasEntTypes.Ready].Get<T>();


        static EntityUIPool()
        {
            _uI = new Dictionary<CanvasEntTypes, Entity>();
            _economyUp = new Dictionary<ResTypes, Entity>();

            for (var type = CanvasEntTypes.First; type < CanvasEntTypes.End; type++)
            {
                _uI.Add(type, default);
            }
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                _economyUp.Add(res, default);
            }
        }

        public EntityUIPool(in WorldEcs curGameW)
        {
            CanvasC.SetCurZone(SceneTypes.Game);

            var upZone_GO = CanvasC.FindUnderCurZone("UpZone");
            var centerZone_GO = CanvasC.FindUnderCurZone("CenterZone");
            var downZone_GO = CanvasC.FindUnderCurZone("DownZone");
            var leftZone_GO = CanvasC.FindUnderCurZone("LeftZone");
            var rightZone_go = CanvasC.FindUnderCurZone("RightZone");


            var uniqAbilZone_trans = rightZone_go.transform.Find("UniqueAbilitiesZone");


            ///Up
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                _economyUp[res] = curGameW.NewEntity()
                    .Add(new EconomyUpUIC(res))
                    .Add(new TextUIC(upZone_GO.transform.Find("ResourcesZone").Find(res.ToString()).Find(res.ToString() + "_TMP").GetComponent<TextMeshProUGUI>()));
            }

            _uI[CanvasEntTypes.Leave] = curGameW.NewEntity()
                .Add(new ButtonC(CanvasC.FindUnderCurZone<Button>("ButtonLeave")));

            _uI[CanvasEntTypes.DirectWind] = curGameW.NewEntity()
                .Add(new DirWindUIC())
                .Add(new ImageUIC(upZone_GO.transform.Find("WindZone").Find("Direct_Image").GetComponent<Image>()));

            _uI[CanvasEntTypes.Alpha] = curGameW.NewEntity()
                .Add(new ButtonC(upZone_GO.transform.Find("Alpha_Button").GetComponent<Button>()));


            ///Center
            _uI[CanvasEntTypes.EndGame] = curGameW.NewEntity()
                .Add(new EndGameUIC())
                .Add(new TextUIC(centerZone_GO.transform.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>()));

            _uI[CanvasEntTypes.Motion] = curGameW.NewEntity()
                .Add(new MotionsUIC())
                .Add(new TextUIC(centerZone_GO.transform.Find("MotionZone").Find("MotionText").GetComponent<TextMeshProUGUI>()));


            var readyZone = centerZone_GO.transform.Find("ReadyZone");

            _uI[CanvasEntTypes.JoinDiscord] = curGameW.NewEntity()
                .Add(new ButtonC(readyZone.Find("JoinDiscordButton").GetComponent<Button>()));

            _uI[CanvasEntTypes.Ready] = curGameW.NewEntity()
                .Add(new ButtonC(readyZone.Find("ReadyButton").GetComponent<Button>()));


            new MistakeUIC(centerZone_GO);
            new KingZoneUIC(centerZone_GO);
            new SelectorUIC(centerZone_GO);
            new FriendZoneUIC(centerZone_GO.transform);
            new HintViewUIC(centerZone_GO.transform, Common.HintC.IsOnHint);
            new PickUpgUIC(centerZone_GO.transform);
            new HeroesViewUIC(centerZone_GO.transform);


            ///Down
            new TwGiveTakeUIC(downZone_GO);
            new DonerUICom(downZone_GO);
            new UpgUnitUIC(downZone_GO.transform);

            var takeUnitZone = downZone_GO.transform.Find("TakeUnitZone");
            new GetPawnArcherUIC(takeUnitZone);
            new GetScoutUIC(takeUnitZone);
            new GetHeroDownUIC(takeUnitZone);


            ///Left
            new CutyLeftUIC(leftZone_GO);
            new EnvirUIC(leftZone_GO);


            ///Right
            new StatUIC(rightZone_go);
            new UniqButtonsUIC(uniqAbilZone_trans);
            new BuildAbilitUIC(rightZone_go.transform.Find("BuildingZone"));
            new ExtraTWZoneUIC(rightZone_go.transform);
            new EffectsUIC(rightZone_go.transform);
            new ProtectUIC(rightZone_go.transform.Find("ConditionZone"));
            new RelaxUIC(rightZone_go.transform.Find("ConditionZone"));
        }
    }
}