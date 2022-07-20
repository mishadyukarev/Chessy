using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.System;

namespace Chessy.View.System
{
    abstract class SystemViewAbstract : SystemAbstract
    {
        protected SystemViewAbstract(in EntitiesModel eM) : base(eM) { }

        internal abstract void Sync();
    }
}