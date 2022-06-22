using Chessy.Common.Entity;

namespace Chessy.Menu
{
    public sealed class EntitiesModelMenu
    {
        public readonly EntitiesModelCommon Common;

        public EntitiesModelMenu(in EntitiesModelCommon common)
        {
            Common = common;
        }
    }
}
