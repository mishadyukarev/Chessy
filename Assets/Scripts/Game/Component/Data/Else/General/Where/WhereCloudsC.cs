using System.Collections.Generic;

namespace Chessy.Game
{
    public struct WhereCloudsC
    {
        private static List<byte> _clouds;

        public static List<byte> CloudsInGame
        {
            get
            {
                var clouds = new List<byte>();
                foreach (var idx in _clouds)
                {
                    clouds.Add(idx);
                }
                return clouds;
            }
        }

        public WhereCloudsC(bool needNew) : this()
        {
            if (needNew) _clouds = new List<byte>();
        }

        //public static List<byte> Clouds => _clouds.Copy();

        public static void Add(byte idxCell) => _clouds.Add(idxCell);
        public static void Remove(byte idxCell) => _clouds.Remove(idxCell);

        public static byte Cloud => _clouds[0];
    }
}