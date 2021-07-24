using Assets.Scripts.Workers.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers
{
    internal abstract class MainMasterWorker : MainGeneralWorker
    {
        protected static EntitiesGameMasterManager EGMM => Main.Instance.ECSmanager.EntitiesGameMasterManager;
    }
}
