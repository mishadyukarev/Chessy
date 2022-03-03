namespace Chessy.Game
{
    public class EntitiesViewUI
    {
        public readonly LeftUIEs LeftEs;
        public readonly RightUIEs RightEs;
        public readonly CenterUIEs CenterEs;
        public readonly DownUIEs DownEs;
        public readonly UpUIEs UpEs;

        public LeftEnvironmentUIEs LeftEnvEs => LeftEs.EnvironmentEs;

        public EntitiesViewUI()
        {
            LeftEs = new LeftUIEs(default);
            RightEs = new RightUIEs(default);
            CenterEs = new CenterUIEs(default);
            DownEs = new DownUIEs(default);
            UpEs = new UpUIEs(default);
        }
    }
}