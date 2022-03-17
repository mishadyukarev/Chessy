using Chessy.Common;

namespace Chessy.Game.Entity.Model
{
    public class WeatherE
    {
        public WindC WindC;
        public SunC SunC;
        public CloudC CloudC;

        public WeatherE(in WindC windC, in SunC sunC, in CloudC cloudC)
        {
            WindC = windC;
            SunC = sunC;
            CloudC = cloudC;
        }
    }
}