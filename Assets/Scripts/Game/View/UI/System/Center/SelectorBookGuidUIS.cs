//using Chessy.Common.Entity;
//using Chessy.Game.View.UI.Entity;

//namespace Chessy.Game.View.UI.System
//{
//    public struct SelectorBookGuidUIS : IEcsRunSystem
//    {
//        readonly SelectionBookGuidUIE _selectionE;
//        readonly EntitiesModelCommon _eMCommon;

//        public SelectorBookGuidUIS(in SelectionBookGuidUIE selBookUIE, in EntitiesModelCommon eMCommon)
//        {
//            _selectionE = selBookUIE;
//            _eMCommon = eMCommon;
//        }

//        public void Run()
//        {
//            _selectionE.ParentGOVC.SetActive(_eMCommon.BookE.IsOpenedBook);
//        }
//    }
//}