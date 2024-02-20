namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void Build(in BuildingTypes buildingT, in LevelTypes levelT, in PlayerTypes playerT, in byte cell_0)
        {
            buildingCs[cell_0].Set(buildingT, playerT, levelT);

            if (buildingT == BuildingTypes.Farm)
            {
                playerInfoCs[(byte)playerT].AmountFarmsInGame++;
            }
        }
    }
}