//namespace Chessy.Model
//{
//    sealed class DownScoutUIS : SystemAbstract, IEcsRunSystem
//    {
//        readonly DownScoutUIE _scoutE;

//        internal DownScoutUIS(in DownScoutUIE scoutUIE, in Chessy.Model.Model.Entity.EntitiesModel ents) : base(ents)
//        {
//            _scoutE = scoutUIE;
//        }

//        public void Run()
//        {
//            var curPlayer = E.CurPlayerITC.Player;

//            var isActive = E.UnitInfoE(curPlayer, LevelTypes.First, UnitTypes.Scout).HaveInInventor;
//            var cooldown = E.UnitInfoE(curPlayer, LevelTypes.First, UnitTypes.Scout).HeroCooldownC.Cooldown;


//            _scoutE.ButtonC.SetActive(isActive);

//            if (isActive && cooldown > 0)
//            {
//                _scoutE.CooldownTextC.SetActiveParent(true);
//                _scoutE.CooldownTextC.TextUI.text = cooldown.ToString();
//            }
//            else
//            {
//                _scoutE.CooldownTextC.SetActiveParent(false);
//            }
//        }
//    }
//}