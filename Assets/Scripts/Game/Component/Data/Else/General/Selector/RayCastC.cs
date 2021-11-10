namespace Chessy.Game
{
    public struct RayCastC
    {
        private static RaycastTypes _raycastType;

        public static void Set(RaycastTypes raycast) => _raycastType = raycast;
        public static void Reset() => _raycastType = default;
        public static bool Is(RaycastTypes raycast) => _raycastType == raycast;
    }
}