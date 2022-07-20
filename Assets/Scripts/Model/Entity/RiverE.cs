using Chessy.Model.Component;
namespace Chessy.Model
{
    public sealed class RiverE
    {
        public readonly RiverC RiverC = new();
        public readonly HaveRiverAroundCellC HaveRiverC = new();
    }
}