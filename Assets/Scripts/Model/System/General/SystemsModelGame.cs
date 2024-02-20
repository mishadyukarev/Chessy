using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using System;
using UnityEngine;

namespace Chessy.Model.System
{
    public partial class SystemsModel : SystemAbstract, IUpdate
    {
        readonly UpdateModelS _updateS;

        internal readonly UnitSystems unitSs;
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
            unitSs = new UnitSystems(this, eM);
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
            bookC.OpenedNowPageBookT = PageBookTypes.None;
            aboutGameC.SceneT = SceneTypes.Menu;


            _updateS = new UpdateModelS(this, eM);
        }
        public void Update() => _updateS.Update();
        public void ToggleScene(in SceneTypes newSceneT)
        {
            aboutGameC.SceneT = newSceneT;

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
            aboutGameC.GameModeT = GameModeTypes.PlayerMeAgainstBot;
            PhotonNetwork.CreateRoom(default);
        }


        internal void RainyGiveWaterToUnitsAround(in byte cellIdx)
        {
            foreach (var cellIdxDirect in idxsAroundCellCs[cellIdx].IdxCellsAroundArray)
            {
                if (unitCs[cellIdxDirect].HaveUnit)
                {
                    if (unitCs[cellIdx].PlayerT == unitCs[cellIdxDirect].PlayerT)
                    {
                        TryExecuteAddingUnitAnimationM(cellIdxDirect);

                        unitWaterCs[cellIdxDirect].Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                    }
                }
            }
        }

        internal void TryExecuteAddingUnitAnimationM(in byte cellIdx)
        {
            if (ValuesChessy.MAX_WATER_FOR_ANY_UNIT - unitWaterCs[cellIdx].Water >= ValuesChessy.WATER_FOR_ADDING_WATER_ANIMATION)
            {
                var idxCellView = unitWhereViewDataCs[cellIdx].ViewIdxCell;

                RpcSs.ExecuteAnimationCellDirectlyToGeneral(idxCellView, CellAnimationDirectlyTypes.AddingWaterUnit, RpcTarget.All);

                dataFromViewC.AnimationCellDirectly(CellAnimationDirectlyTypes.AddingWaterUnit).Invoke(idxCellView);
            }
        }

        internal void ExecuteSoundActionClip(in ClipTypes clipT) => dataFromViewC.SoundAction(clipT).Invoke();
        internal void ExecuteSoundActionAbility(in AbilityTypes abilityT) => dataFromViewC.SoundAction(abilityT).Invoke();

        internal void ExecuteAnimationClip(in byte cellIdx, in AnimationCellTypes animationCellT) => dataFromViewC.AnimationCell(cellIdx, animationCellT).Invoke();
        internal void ExecuteAnimationCellDirectlyClip(in byte cellIdx, in CellAnimationDirectlyTypes cellAnimationDirectlyT) => dataFromViewC.AnimationCellDirectly(cellAnimationDirectlyT).Invoke(cellIdx);

        internal void ExecuteMistake(in MistakeTypes mistakeT, in float[] needRes)
        {
            Mistake(mistakeT);
            dataFromViewC.SoundAction(ClipTypes.WritePensil).Invoke();

            if (mistakeT == MistakeTypes.Economy)
            {
                mistakeC.NeedResourcesRef(ResourceTypes.Food) = 0;
                mistakeC.NeedResourcesRef(ResourceTypes.Wood) = 0;
                mistakeC.NeedResourcesRef(ResourceTypes.Ore) = 0;
                mistakeC.NeedResourcesRef(ResourceTypes.Iron) = 0;
                mistakeC.NeedResourcesRef(ResourceTypes.Gold) = 0;

                mistakeC.NeedResourcesRef(ResourceTypes.Food) = needRes[0];
                mistakeC.NeedResourcesRef(ResourceTypes.Wood) = needRes[1];
                mistakeC.NeedResourcesRef(ResourceTypes.Ore) = needRes[2];
                mistakeC.NeedResourcesRef(ResourceTypes.Iron) = needRes[3];
                mistakeC.NeedResourcesRef(ResourceTypes.Gold) = needRes[4];
            }
        }

        internal void SetNextLesson()
        {
            if (aboutGameC.LessonT == LessonTypes.End - 1)
            {
                aboutGameC.LessonT = LessonTypes.None;
            }
            else aboutGameC.LessonT++;
        }
        internal void SetPreviousLesson()
        {
            aboutGameC.LessonT--;
        }
    }
}