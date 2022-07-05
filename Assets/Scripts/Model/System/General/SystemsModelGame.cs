using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        protected readonly EntitiesModel _e;
        readonly List<Action> _runs;

        internal readonly UnitSystems UnitSs;
        internal readonly GetDataCellsAfterAnyDoingS_M GetDataCellsS;
        internal readonly ExecuteAIBotLogicAfterUpdateS_M AIBotS;
        internal readonly ExecuteUpdateEverythingMS ExecuteUpdateEverythingMS;
        internal readonly RpcSs RpcSs;
        internal readonly ForPhotonSceneS ForPhotonSceneS;

        public readonly ForButtonsSystemsModel ForUISs;

        public SystemsModel(in EntitiesModel eM)
        {
            _e = eM;

            _runs = new List<Action>()
            {
                new InputS(this, eM).Update,
                new CheatsS(this, eM).Update,
                new RayS(this, eM).Update,
                new SelectorS(this, eM).Update,

                new MistakeS(this, eM).Update,
            };
   
            UnitSs = new UnitSystems(this, eM);
            GetDataCellsS = new GetDataCellsAfterAnyDoingS_M(this, eM);
            AIBotS = new ExecuteAIBotLogicAfterUpdateS_M(this, eM);
            ExecuteUpdateEverythingMS = new ExecuteUpdateEverythingMS(this, eM);
            RpcSs = new RpcSs(this, eM);

            ForPhotonSceneS = new ForPhotonSceneS(this, eM);
            ForUISs = new ForButtonsSystemsModel(this, eM);

            Application.runInBackground = true;
            var nowTime = DateTime.Now;
            _e.AdC = new AdC(nowTime);
            _e.OpenedNowPageBookT = PageBookTypes.None;
            _e.SceneT = SceneTypes.Menu;
        }
        
        public void ToggleScene(in SceneTypes newSceneT)
        {
            _e.SceneT = newSceneT;

            switch (newSceneT)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        break;
                    }

                case SceneTypes.Game:
                    {
                        //_eMC.IsOpenedBook = true;
                        //_eMC.PageBookTC.PageBookT = PageBookTypes.Main;
                        break;
                    }
                default: throw new Exception();
            }
        }

        public void ComeIntoTrainingAfterDownloadingGame()
        {
            PhotonNetwork.OfflineMode = true;
            _e.GameModeT = GameModeTypes.TrainingOffline;
            PhotonNetwork.CreateRoom(default);
        }


        internal void RainyGiveWaterToUnitsAround(in byte cellIdx)
        {
            foreach (var cellIdxDirect in _e.AroundCellsE(cellIdx).CellsAround)
            {
                if (_e.UnitT(cellIdxDirect).HaveUnit())
                {
                    if (_e.UnitPlayerT(cellIdx) == _e.UnitPlayerT(cellIdxDirect))
                    {
                        _e.WaterUnitC(cellIdxDirect).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                    }
                }
            }
        }

        internal void ExecuteSoundAction(in ClipTypes clipT) => _e.SoundAction(clipT).Invoke();
        internal void ExecuteSoundAction(in AbilityTypes abilityT) => _e.SoundAction(abilityT).Invoke();

        internal void ExecuteAnimationClip(in byte cellIdx, in AnimationCellTypes animationCellT) => _e.DataFromViewC.AnimationCell(cellIdx, animationCellT).Invoke();

        internal void ActiveMotion() => _e.MotionTimer = 4;

        internal void ExecuteMistake(in MistakeTypes mistakeT, in float[] needRes)
        {
            Mistake(mistakeT);
            _e.SoundAction(ClipTypes.WritePensil).Invoke();

            if (mistakeT == MistakeTypes.Economy)
            {
                _e.MistakeEconomy(ResourceTypes.Food).Resources = 0;
                _e.MistakeEconomy(ResourceTypes.Wood).Resources = 0;
                _e.MistakeEconomy(ResourceTypes.Ore).Resources = 0;
                _e.MistakeEconomy(ResourceTypes.Iron).Resources = 0;
                _e.MistakeEconomy(ResourceTypes.Gold).Resources = 0;

                _e.MistakeEconomy(ResourceTypes.Food).Resources = needRes[0];
                _e.MistakeEconomy(ResourceTypes.Wood).Resources = needRes[1];
                _e.MistakeEconomy(ResourceTypes.Ore).Resources = needRes[2];
                _e.MistakeEconomy(ResourceTypes.Iron).Resources = needRes[3];
                _e.MistakeEconomy(ResourceTypes.Gold).Resources = needRes[4];
            }
        }

        internal void SetNextLesson()
        {
            if (_e.CommonInfoAboutGameC.LessonT == LessonTypes.End - 1)
            {
                _e.CommonInfoAboutGameC.LessonT = LessonTypes.None;
            }
            else _e.CommonInfoAboutGameC.LessonT++;
        }
        internal void SetPreviousLesson()
        {
            _e.CommonInfoAboutGameC.LessonT--;
        }
    }
}