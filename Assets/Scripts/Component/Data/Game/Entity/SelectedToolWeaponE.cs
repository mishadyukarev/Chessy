using ECS;

namespace Game.Game
{
    public sealed class SelectedToolWeaponE : EntityAbstract
    {
        public ref ToolWeaponTC ToolWeaponTC => ref Ent.Get<ToolWeaponTC>();
        public ref LevelTC LevelTC => ref Ent.Get<LevelTC>();

        public SelectedToolWeaponE(in EcsWorld gameW) : base(gameW)
        {
            Ent
                .Add(new ToolWeaponTC(ToolWeaponTypes.Pick))
                .Add(new LevelTC(LevelTypes.Second));
        }

    }
}