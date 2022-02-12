using ECS;

namespace Game.Game
{
    public sealed class SelectedToolWeaponE : EntityAbstract
    {
        ref ToolWeaponTC ToolWeaponTC => ref Ent.Get<ToolWeaponTC>();
        ref LevelTC LevelTC => ref Ent.Get<LevelTC>();

        public ToolWeaponTypes ToolWeaponT => ToolWeaponTC.ToolWeapon;
        public LevelTypes LevelT => LevelTC.Level;

        public bool Is(params LevelTypes[] levTs) => LevelTC.Is(levTs);

        public SelectedToolWeaponE(in EcsWorld gameW) : base(gameW)
        {
            Ent
                .Add(new ToolWeaponTC(ToolWeaponTypes.Pick))
                .Add(new LevelTC(LevelTypes.Second));
        }

        public void Set(in ToolWeaponTypes twT, in LevelTypes levT)
        {
            ToolWeaponTC.ToolWeapon = twT;
            LevelTC.Level = levT;
        }
    }
}