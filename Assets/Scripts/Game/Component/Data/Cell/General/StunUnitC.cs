namespace Chessy.Game
{
    public struct StunUnitC
    {
        private bool _isStunned;
        private int _stepsInStun;

        public bool IsStunned => _isStunned;
        public int StepsInStun => _stepsInStun;

        public void SetNewStun()
        {
            _isStunned = true;
            _stepsInStun = 1;
        }
    }
}