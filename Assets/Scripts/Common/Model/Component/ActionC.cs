using System;

namespace Chessy.Common.Component
{
    public struct ActionC
    {
        public Action Action;

        public ActionC(in Action action) => Action = action;

        public void Invoke() => Action?.Invoke();
    }
}