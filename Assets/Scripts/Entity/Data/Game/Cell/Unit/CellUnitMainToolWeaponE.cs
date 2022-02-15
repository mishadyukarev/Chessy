using ECS;

namespace Game.Game
{
    public sealed class CellUnitMainToolWeaponE : EntityAbstract
    {
        public ref CellUnitMainToolWeaponTC ToolWeaponTC => ref Ent.Get<CellUnitMainToolWeaponTC>();
        public ref LevelTC LevelTC => ref Ent.Get<LevelTC>();

        internal CellUnitMainToolWeaponE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}