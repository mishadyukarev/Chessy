using ECS;

namespace Game.Game
{
    public sealed class CellBuildingE : EntityAbstract
    {
        public ref BuildingTC BuildTC => ref Ent.Get<BuildingTC>();
        public ref PlayerTC PlayerTC => ref Ent.Get<PlayerTC>();
        public ref AmountC Health => ref Ent.Get<AmountC>();

        public CellBuildingE(in EcsWorld world) : base(world) { }

        public void SetNew(in BuildingTypes build, in PlayerTypes owner)
        {
            BuildTC.Build = build;
            PlayerTC.Player = owner;
            Health.Amount = CellBuildingValues.MaxAmountHealth(build);
        }
        public void Remove()
        {
            BuildTC.Reset();
            PlayerTC.Reset();
            Health.Reset();
        }
    }
}