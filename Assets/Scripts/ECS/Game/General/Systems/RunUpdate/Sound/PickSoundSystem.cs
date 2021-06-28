namespace Assets.Scripts.ECS.Game.General.Systems.RunUpdate.Sound
{
    internal class PickSoundSystem : SystemGeneralReduction
    {
        private bool _isActivated;
        private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

        public override void Run()
        {
            base.Run();

            //if (!_isActivated)
            //{
            //    switch (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).UnitType)
            //    {
            //        case UnitTypes.None:
            //            break;

            //        case UnitTypes.King:
            //            break;

            //        case UnitTypes.Pawn:
            //            break;

            //        case UnitTypes.Rook:
            //            _eGM.PickArcherAudioSource.Play();
            //            break;

            //        case UnitTypes.Bishop:
            //            _eGM.PickArcherAudioSource.Play();
            //            //_eGM.PickArcherAudioSource.
            //            break;

            //        default:
            //            break;
            //    }
            //}      
        }
    }
}
