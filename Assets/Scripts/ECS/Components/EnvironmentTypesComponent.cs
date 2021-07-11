using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ECS.Components
{
    internal struct EnvironmentTypesComponent
    {
        private EnvironmentTypes _environmentType;

        internal EnvironmentTypes EnvironmentType => _environmentType;

        internal void StartFill(EnvironmentTypes environmentType = default) => _environmentType = environmentType;

        internal void SetEnvironmentType(EnvironmentTypes environmentType) => _environmentType = environmentType;
        internal void ResetEnvironmentType() => _environmentType = default;
    }
}
