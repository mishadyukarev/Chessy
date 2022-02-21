//using ECS;

//namespace Game.Game
//{
//    public sealed class SelectedAbilityE : EntityAbstract
//    {


//        public AbilityTypes Ability => AbilityTC.Ability;
//        public bool Is(params AbilityTypes[] abils) => AbilityTC.Is(abils);

//        internal SelectedAbilityE(in EcsWorld gameW) : base(gameW)
//        {

//        }

//        public void Set(in AbilityTypes ability) => AbilityTC.Ability = ability;
//        public void SetAbility(in AbilityTypes ability, in ClickerObjectE clickerObjectE)
//        {
//            AbilityTC.Ability = ability;
//            CellClickCRef.Click = CellClickTypes.UniqueAbility;
//        }
//    }
//}