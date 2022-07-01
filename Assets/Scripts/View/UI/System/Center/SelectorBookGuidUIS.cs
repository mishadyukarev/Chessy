//using Chessy.Model.Entity;
//using Chessy.View.UI.Entity;

//using Chessy.Model.Entity; namespace Chessy.View.UI
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