using ECS;

namespace Game.Game
{
    public sealed class CellUnitTWE : EntityAbstract
    {
        public ref ToolWeaponC ToolWeaponC => ref Ent.Get<ToolWeaponC>();
        public ref LevelTC LevelC => ref Ent.Get<LevelTC>();
        public ref AmountC Protection => ref Ent.Get<AmountC>();

        public CellUnitTWE(in EcsWorld gameW) : base(gameW) { }
    }
}