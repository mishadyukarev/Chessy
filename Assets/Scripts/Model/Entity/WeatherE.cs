namespace Chessy.Model
{
    public sealed class WeatherE
    {
        public readonly WindC WindC = new();
        public readonly SunC SunC = new();

        internal void Dispose()
        {
            WindC.Dispose();
            SunC.Dispose();
        }
    }
}