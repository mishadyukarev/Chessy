using Chessy.View.Component;

namespace Chessy.View.Entity
{
    readonly struct UnitMainVE
    {
        internal readonly GameObjectVC GOVC;
        internal readonly SpriteRendererVC SRVC;

        internal UnitMainVE(in GameObjectVC gOVC, in SpriteRendererVC sRVC)
        {
            GOVC = gOVC;
            SRVC = sRVC;
        }
    }
}