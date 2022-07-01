//using System; using Chessy.Model.Entity;

//using Chessy.Model.Entity; namespace Chessy.View.UI
//{
//    sealed class ToggleSceneUIS
//    {
//        readonly EntitiesViewUICommon _eUIC;

//        internal ToggleSceneUIS(in EntitiesViewUICommon eUIC)
//        {
//            _eUIC = eUIC;
//        }

//        internal void ToggleScene(in SceneTypes newSceneT)
//        {
//            switch (newSceneT)
//            {
//                case SceneTypes.None:
//                    throw new Exception();

//                case SceneTypes.Menu:
//                    {
//                        _eUIC.CanvasE.MenuCanvasGOC.SetActive(true);
//                        _eUIC.CanvasE.GameCanvasGOC.SetActive(false);
//                        break;
//                    }

//                case SceneTypes.Game:
//                    {
//                        _eUIC.CanvasE.MenuCanvasGOC.SetActive(false);
//                        _eUIC.CanvasE.GameCanvasGOC.SetActive(true);
//                        break;
//                    }
//                default: throw new Exception();
//            }
//        }
//    }
//}