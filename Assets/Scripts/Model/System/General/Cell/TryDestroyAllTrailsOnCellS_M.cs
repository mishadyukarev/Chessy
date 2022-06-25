using Chessy.Model;
using Chessy.Model;

namespace Chessy
{
    public static partial class SystemStatic
    {
        internal static void TryDestroyAllTrailsOnCell(this EntitiesModel e, in byte cell)
        {
            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                if (e.HealthTrail(cell).IsAlive(dirT))
                {
                    e.HealthTrail(cell).Health(dirT) = 0;
                }
            }
        }
    }
}