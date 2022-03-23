using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model.Master;
using Chessy.Game.Values;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Game.System.Model
{
    public sealed class SystemsModelGame : IToggleScene, IEcsRunSystem
    {
        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesModelGame _eMGame;

        Ray _ray;
        const float RAY_DISTANCE = 100;


        public readonly SelectorS SelectorS;
        public readonly AttackUnitS AttackUnitS;
        public readonly KillUnitS KillUnitS;
        public readonly AttackShieldS AttackShieldS;
        public readonly SetLastDiedS SetLastDiedS;
        public readonly MistakeS MistakeS;


        #region UI

        //Down
        public readonly DoneClickS DoneClickS;
        public readonly OpenCityClickS OpenCityClickS;
        public readonly GetHeroDownS GetHeroClickDownS;
        public readonly GetPawnS GetPawnClickS;
        public readonly ToggleToolWeaponS ToggleToolWeaponClickS;
        //Left
        public readonly EnvironmentInfoS EnvironmentInfoClickS;
        public readonly ReadyS ReadyClickS;
        public readonly GetKingClickS GetKingClickS;
        public readonly BuildBuildingClickS BuildBuildingClickS;
        //Center
        public readonly GetHeroClickCenterS GetHeroClickCenterS;
        //Right
        public readonly AbilityClickS AbilityClickS;
        public readonly ConditionClickS ConditionClickS;

        #endregion


        #region Master

        public readonly IncreaseWindSnowyS_M IncreaseWindSnowyS_M;
        public readonly UpdateS_M UpdateS_M;
        public readonly GiveTakeToolWeaponS_M GiveTakeToolWeaponS_M;
        public readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        public readonly AttackUnit_M AttackUnit_M;
        public readonly UnitEatFoodUpdateS_M UnitEatFoodUpdateS_M;
        public readonly BuyS_M BuyS_M;
        public readonly MeltS_M MeltS_M;
        public readonly BuyBuildingS_M BuyBuildingS_M;

        public readonly WorldMeltIceWallUpdateMS WorldMeltIceWallUpdateS_M;

        #endregion


        public SystemsModelGame(in EntitiesModelGame eMGame, in EntitiesModelCommon eMCommon)
        {
            _eMGame = eMGame;
            _eMCommon = eMCommon;

            UpdateS_M = new UpdateS_M(this, eMGame);
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {

        }

        public void Run()
        {
            if (_eMCommon.SceneC.Scene == SceneTypes.Game)
            {
                _ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                var raycast = Physics2D.Raycast(_ray.origin, _ray.direction, RAY_DISTANCE);

                //#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL

                //            if (EventSystem.current.IsPointerOverGameObject())
                //            {
                //                raycastC.Raycast = RaycastTypes.UI;
                //                return;
                //            }

                //#endif


                var rayCastT = RaycastTypes.None;

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    rayCastT = RaycastTypes.UI;
                }
                else if (raycast)
                {
                    for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                    {
                        int one = _eMGame.CellEs(idx_0).CellE.InstanceIDC;
                        int two = raycast.transform.gameObject.GetInstanceID();

                        if (one == two)
                        {
                            if (_eMGame.CellsC.Current != _eMGame.CellsC.PreviousVision)
                            {
                                _eMGame.CellsC.PreviousVision = _eMGame.CellsC.Current;
                            }

                            _eMGame.CellsC.Current = idx_0;
                            rayCastT = RaycastTypes.Cell;
                        }
                    }

                    if (rayCastT == RaycastTypes.None) rayCastT = RaycastTypes.Background;
                }


                SelectorS.Run(rayCastT, _eMGame);


#if UNITY_ANDROID
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //{
            //    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //    {
            //        RayCastC.Set(RaycastTypes.UI);
            //    }
            //}
#endif
            }
        }
    }
}