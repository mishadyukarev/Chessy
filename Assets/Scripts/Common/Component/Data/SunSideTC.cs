using System;

namespace Game.Game
{
    public struct SunSideTC
    {
        public SunSideTypes SunSide;

        public DirectTypes[] RaysSun
        {
            get
            {
                var directs = new DirectTypes[3];
                switch (SunSide)
                {
                    case SunSideTypes.Dawn:
                        {
                            directs[0] = DirectTypes.UpRight;
                            directs[1] = DirectTypes.Right;
                            directs[2] = DirectTypes.DownRight;
                        }
                        break;
                    case SunSideTypes.Sunset:
                        {
                            directs[0] = DirectTypes.UpLeft;
                            directs[1] = DirectTypes.Left;
                            directs[2] = DirectTypes.DownLeft;
                        }
                        break;
                    default: throw new Exception();
                }

                return directs;
            }
        }
        public bool IsAcitveSun
        {
            get
            {
                switch (SunSide)
                {
                    case SunSideTypes.Dawn: return true;
                    case SunSideTypes.Center: return false;
                    case SunSideTypes.Sunset: return true;
                    case SunSideTypes.Night: return false;
                    default: throw new Exception();
                }
            }
        }

        public SunSideTC(in SunSideTypes sunSide) => SunSide = sunSide;

        public void ToggleNext() => SunSide = SunSide == SunSideTypes.Night ? SunSideTypes.Dawn : ++SunSide;
    }
}