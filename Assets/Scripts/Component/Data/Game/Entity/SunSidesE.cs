using ECS;
using System;

namespace Game.Game
{
    public sealed class SunSidesE : EntityAbstract
    {
        ref SunSideTC SunSideTCRef => ref Ent.Get<SunSideTC>();
        public SunSideTC SunSideTC => Ent.Get<SunSideTC>();


        public DirectTypes[] RaysSun
        {
            get
            {
                var directs = new DirectTypes[3];
                switch (SunSideTC.SunSide)
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
                switch (SunSideTC.SunSide)
                {
                    case SunSideTypes.Dawn: return true;
                    case SunSideTypes.Center: return false;
                    case SunSideTypes.Sunset: return true;
                    case SunSideTypes.Night: return false;
                    default: throw new Exception();
                }
            }
        }

        public SunSidesE(in SunSideTypes sunSide, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new SunSideTC(sunSide));
        }

        public void ToggleNext()
        {
            if (SunSideTC.SunSide == SunSideTypes.Night)
            {
                SunSideTCRef.SunSide = SunSideTypes.Dawn;
            }
            else
            {
                SunSideTCRef.SunSide++;
            }
        }
    }
}