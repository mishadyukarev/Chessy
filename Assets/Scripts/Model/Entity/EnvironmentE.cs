using Chessy.Model.Component;

namespace Chessy.Model
{
    public sealed class EnvironmentE
    {
        public readonly EnvironmentC EnvironmentC = new(new double[(byte)EnvironmentTypes.End]);

        internal void Dispose()
        {
            EnvironmentC.Dispose();
        }
    }
}