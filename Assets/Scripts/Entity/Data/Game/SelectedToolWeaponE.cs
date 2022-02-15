//using ECS;

//namespace Game.Game
//{
//    public sealed class SelectedToolWeaponE : EntityAbstract
//    {


//        public ToolWeaponTypes ToolWeapon
//        {
//            get => ToolWeaponTC.ToolWeapon;
//            set => ToolWeaponTC.ToolWeapon = value;
//        }
//        public LevelTypes Level
//        {
//            get => LevelTC.Level;
//            set => LevelTC.Level = value;
//        }

//        public bool Is(params LevelTypes[] levTs) => LevelTC.Is(levTs);

//        public SelectedToolWeaponE(in EcsWorld gameW) : base(gameW)
//        {

//        }

//        public void Set(in ToolWeaponTypes twT, in LevelTypes levT)
//        {
//            ToolWeaponTC.ToolWeapon = twT;
//            LevelTC.Level = levT;
//        }
//    }
//}