namespace Chessy.Game
{
    public struct StunC
    {
        private bool _isStunned;
        private int _stepsInStun;

        public bool IsStunned => _isStunned;
        public int StepsInStun => _stepsInStun;

        public void SetNewStun()
        {
            _isStunned = true;
            _stepsInStun = 2;
        }
        public void Reset()
        {
            _isStunned = false;
            _stepsInStun = 0;
        }
        public void Take()
        {
            _stepsInStun -= 1;
            if(_stepsInStun <= 0)
            {
                _isStunned = false;
            }
        }

        public void Sync(bool isStunned, int steps)
        {
            _isStunned = isStunned;
            _stepsInStun = steps;
        }
    }
}