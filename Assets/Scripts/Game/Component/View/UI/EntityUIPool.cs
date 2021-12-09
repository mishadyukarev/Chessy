﻿using Game.Common;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct EntityUIPool
    {
        readonly static Dictionary<CanvasEntTypes, EcsEntity> _uI;
        readonly static Dictionary<ResTypes, EcsEntity> _economyUp;


        //Up
        public static ref T EconomyUp<T>(ResTypes res) where T : struct => ref _economyUp[res].Get<T>();
        public static ref T AlphaUp<T>() where T : struct => ref _uI[CanvasEntTypes.Alpha].Get<T>();
        public static ref T LeaveUp<T>() where T : struct => ref _uI[CanvasEntTypes.Leave].Get<T>();
        public static ref T DirectWindUp<T>() where T : struct => ref _uI[CanvasEntTypes.DirectWind].Get<T>();

        //Center
        public static ref T EndGameCenter<T>() where T : struct => ref _uI[CanvasEntTypes.EndGame].Get<T>();
        public static ref T MotionCenter<T>() where T : struct => ref _uI[CanvasEntTypes.Motion].Get<T>();



        static EntityUIPool()
        {
            _uI = new Dictionary<CanvasEntTypes, EcsEntity>();
            _economyUp = new Dictionary<ResTypes, EcsEntity>();

            for (var type = CanvasEntTypes.First; type < CanvasEntTypes.End; type++)
            {
                _uI.Add(type, default);
            }
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                _economyUp.Add(res, default);
            }
        }

        public EntityUIPool(in EcsWorld curGameW)
        {
            //Main
            ToggleZoneVC.ReplaceZone(SceneTypes.Game);

            var genZone = new GameObject("GeneralZone");
            ToggleZoneVC.Attach(genZone.transform);


            new VideoClipsResC(true);
            new SpritesResC(true);

            SoundC.SavedVolume = SoundC.Volume;



            new GenerZoneVC(genZone);

            var backGroundGO = GameObject.Instantiate(PrefabResC.BackGroundCollider2D,
                MainGoVC.Pos + new Vector3(7, 5.5f, 2), MainGoVC.Rot);

            GenerZoneVC.Attach(backGroundGO.transform);

            var aSParent = new GameObject("AudioSource");
            GenerZoneVC.Attach(aSParent.transform);


            new SoundEffectVC(aSParent);
            new BackgroundVC(backGroundGO, PhotonNetwork.IsMasterClient);



            ///Canvas
            ///

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
                    .Replace(new EconomyUpUIC(res))
                    .Replace(new TextUIC(upZone_GO.transform.Find("ResourcesZone").Find(res.ToString()).Find(res.ToString() + "_TMP").GetComponent<TextMeshProUGUI>()));
            }

            _uI[CanvasEntTypes.Leave] = curGameW.NewEntity()
                .Replace(new ButtonC(CanvasC.FindUnderCurZone<Button>("ButtonLeave")));

            _uI[CanvasEntTypes.DirectWind] = curGameW.NewEntity()
                .Replace(new DirWindUIC())
                .Replace(new ImageUIC(upZone_GO.transform.Find("WindZone").Find("Direct_Image").GetComponent<Image>()));

            _uI[CanvasEntTypes.Alpha] = curGameW.NewEntity()
                .Replace(new ButtonC(upZone_GO.transform.Find("Alpha_Button").GetComponent<Button>()));


            ///Center
            _uI[CanvasEntTypes.EndGame] = curGameW.NewEntity()
                .Replace(new EndGameUIC())
                .Replace(new TextUIC(centerZone_GO.transform.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>()));

            _uI[CanvasEntTypes.Motion] = curGameW.NewEntity()
                .Replace(new MotionsUIC())
                .Replace(new TextUIC(centerZone_GO.transform.Find("MotionZone").Find("MotionText").GetComponent<TextMeshProUGUI>()));

            new ReadyUIC(centerZone_GO.transform.Find("ReadyZone").gameObject);
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