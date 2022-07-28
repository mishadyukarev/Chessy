namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in byte cell_0)
        {
            BuildingC(cell_0).Set(buildingT, playerT, levelT);

            if (buildingT == BuildingTypes.Farm)
            {
                PlayerInfoC(playerT).AmountFarmsInGame++;
            }
        }
    }
}