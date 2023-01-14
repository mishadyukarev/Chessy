using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.System
{
    public partial class SystemsModel : SystemAbstract, IUpdate
    {
        readonly List<Action> _runs;

        internal readonly UnitSystems UnitSs;
        internal readonly GetDataCellsAfterAnyDoingS_M GetDataCellsS;
        internal readonly ExecuteAIBotLogicAfterUpdateS_M AIBotS;
        internal readonly ExecuteUpdateEverythingMS ExecuteUpdateEverythingMS;
        internal readonly RpcInternetSs RpcSs;
        internal readonly ForPhotonSceneS ForPhotonSceneS;
        internal readonly TryShiftCloudsMS TryShiftCloudsMS;
        internal readonly TryExecuteShiftingUnitS TryExecuteShiftingUnitS;
        internal readonly OnPhotonSerializeViewS OnPhotonSerializeViewS;
        internal readonly SetNewUnitOnCellS SetNewUnitOnCellS;
        internal readonly TruceS TruceS;

        public readonly ForButtonsSystemsModel ForUISs;


        public SystemsModel(in EntitiesModel eM) : base(eM)
        {
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
            RpcSs = new RpcInternetSs(this, eM);
            TryShiftCloudsMS = new TryShiftCloudsMS(this, eM);
            TryExecuteShiftingUnitS = new TryExecuteShiftingUnitS(this, eM);
            SetNewUnitOnCellS = new SetNewUnitOnCellS(this, eM);
            TruceS = new TruceS(this, eM);

            ForPhotonSceneS = new ForPhotonSceneS(this, eM);
            ForUISs = new ForButtonsSystemsModel(this, eM);

            OnPhotonSerializeViewS = new OnPhotonSerializeViewS(this, eM);



            Application.runInBackground = true;
            _bookC.OpenedNowPageBookT = PageBookTypes.None;
            AboutGameC.SceneT = SceneTypes.Menu;
            _dateTimeLastUpdate = DateTime.Now;
            _settingsC.IsOpenedBarWithSettings = true;
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            AboutGameC.SceneT = newSceneT;

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
            AboutGameC.GameModeT = GameModeTypes.TrainingOffline;
            PhotonNetwork.CreateRoom(default);
        }


        internal void RainyGiveWaterToUnitsAround(in byte cellIdx)
        {
            foreach (var cellIdxDirect in _idxsAroundCellCs[cellIdx].IdxCellsAroundArray)
            {
                if (_unitCs[cellIdxDirect].HaveUnit)
                {
                    if (_unitCs[cellIdx].PlayerT == _unitCs[cellIdxDirect].PlayerT)
                    {
                        TryExecuteAddingUnitAnimationM(cellIdxDirect);

                        _unitWaterCs[cellIdxDirect].Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                    }
                }
            }
        }

        internal void TryExecuteAddingUnitAnimationM(in byte cellIdx)
        {
            if (ValuesChessy.MAX_WATER_FOR_ANY_UNIT - _unitWaterCs[cellIdx].Water >= ValuesChessy.WATER_FOR_ADDING_WATER_ANIMATION)
            {
                var idxCellView = _unitWhereViewDataCs[cellIdx].ViewIdxCell;

                RpcSs.ExecuteAnimationCellDirectlyToGeneral(idxCellView, CellAnimationDirectlyTypes.AddingWaterUnit, RpcTarget.All);

                _dataFromViewC.AnimationCellDirectly(CellAnimationDirectlyTypes.AddingWaterUnit).Invoke(idxCellView);
            }
        }

        internal void ExecuteSoundActionClip(in ClipTypes clipT) => _dataFromViewC.SoundAction(clipT).Invoke();
        internal void ExecuteSoundActionAbility(in AbilityTypes abilityT) => _dataFromViewC.SoundAction(abilityT).Invoke();

        internal void ExecuteAnimationClip(in byte cellIdx, in AnimationCellTypes animationCellT) => _dataFromViewC.AnimationCell(cellIdx, animationCellT).Invoke();
        internal void ExecuteAnimationCellDirectlyClip(in byte cellIdx, in CellAnimationDirectlyTypes cellAnimationDirectlyT) => _dataFromViewC.AnimationCellDirectly(cellAnimationDirectlyT).Invoke(cellIdx);

        internal void ExecuteMistake(in MistakeTypes mistakeT, in float[] needRes)
        {
            Mistake(mistakeT);
            _dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();

            if (mistakeT == MistakeTypes.Economy)
            {
                _mistakeC.NeedResourcesRef(ResourceTypes.Food) = 0;
                _mistakeC.NeedResourcesRef(ResourceTypes.Wood) = 0;
                _mistakeC.NeedResourcesRef(ResourceTypes.Ore) = 0;
                _mistakeC.NeedResourcesRef(ResourceTypes.Iron) = 0;
                _mistakeC.NeedResourcesRef(ResourceTypes.Gold) = 0;

                _mistakeC.NeedResourcesRef(ResourceTypes.Food) = needRes[0];
                _mistakeC.NeedResourcesRef(ResourceTypes.Wood) = needRes[1];
                _mistakeC.NeedResourcesRef(ResourceTypes.Ore) = needRes[2];
                _mistakeC.NeedResourcesRef(ResourceTypes.Iron) = needRes[3];
                _mistakeC.NeedResourcesRef(ResourceTypes.Gold) = needRes[4];
            }
        }

        internal void SetNextLesson()
        {
            if (AboutGameC.LessonT == LessonTypes.End - 1)
            {
                AboutGameC.LessonT = LessonTypes.None;
            }
            else AboutGameC.LessonT++;
        }
        internal void SetPreviousLesson()
        {
            AboutGameC.LessonT--;
        }
    }
}