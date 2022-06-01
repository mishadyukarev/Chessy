using Chessy.Game;
using System;

namespace Chessy.Common
{
    public struct SunSideTC
    {
        public SunSideTypes SunSideT { get; /*internal*/ set; }

        public DirectTypes[] RaysSun
        {
            get
            {
                var directs = new DirectTypes[3];
                switch (SunSideT)
                {
                    case SunSideTypes.Dawn:
                        {
                            directs[0] = DirectTypes.UpRight;
                            directs[1] = DirectTypes.Right;
                            directs[2] = DirectTypes.RightDown;
                        }
                        break;
                    case SunSideTypes.Sunset:
                        {
                            directs[0] = DirectTypes.LeftUp;
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
                switch (SunSideT)
                {
                    case SunSideTypes.Dawn: return true;
                    case SunSideTypes.Center: return false;
                    case SunSideTypes.Sunset: return true;
                    case SunSideTypes.Night: return false;
                    default: throw new Exception();
                }
            }
        }

        public SunSideTC(in SunSideTypes sunSide) => SunSideT = sunSide;

        public void ToggleNext() => SunSideT = SunSideT == SunSideTypes.Night ? SunSideTypes.Dawn : ++SunSideT;
    }
}