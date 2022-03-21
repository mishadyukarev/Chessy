using Chessy.Common.Enum;

namespace Chessy.Common.Entity
{
    public sealed class EntitiesModel
    {
        public PageBoookTypes CurrentPageBookT;
        public bool IsOpenedBook;

        public bool IsOnHint;


        public EntitiesModel(in TestModes testMode)
        {
            CurrentPageBookT = PageBoookTypes.Main;


            IsOnHint = testMode != TestModes.Standart;
        }
    }
}