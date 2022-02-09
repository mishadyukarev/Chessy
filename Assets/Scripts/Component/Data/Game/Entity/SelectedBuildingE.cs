using ECS;

namespace Game.Game
{
    public sealed class SelectedBuildingE : EntityAbstract
    {
        ref BuildingTC BuildTCRef => ref Ent.Get<BuildingTC>();

        public BuildingTypes BuildT => BuildTCRef.Build;

        internal SelectedBuildingE(in EcsWorld gameW) : base(gameW)
        {
        }

        public void Set(in BuildingTypes buildT, in ClickerObjectE clickerObjectE)
        {
            BuildTCRef.Build = buildT;
            clickerObjectE.ClickT = CellClickTypes.CityBuildBuilding;
        }
    }
}