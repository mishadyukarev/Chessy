using Chessy.Model.Component;

namespace Chessy.Model
{
    public sealed class EnvironmentE
    {
        public readonly EnvironmentC EnvironmentC = new(new float[(byte)EnvironmentTypes.End]);

        internal void Dispose()
        {
            EnvironmentC.Dispose();
        }
    }
}