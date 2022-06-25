//namespace Chessy.Model
//{
//    sealed class CenterPickFractionUIS : SystemUIAbstract, IEcsRunSystem
//    {
//        internal CenterPickFractionUIS(in EntitiesViewUI entsUI, in Chessy.Model.EntitiesModel ents) : base(entsUI, ents)
//        {
//        }

//        public void Run()
//        {
//            var curPlayer = E.CurPlayerITC.Player;

//            var isActivatedZone = E.PlayerE(curPlayer).FractionTypes == FractionTypes.None && !E.UnitInfoE(curPlayer, LevelTypes.First, UnitTypes.King).HaveInInventor;

//            UIE.CenterEs.UpgradeE.Parent.SetActive(isActivatedZone);

//            if (isActivatedZone)
//            {

//            }
//        }
//    }
//}