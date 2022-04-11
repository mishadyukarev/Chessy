namespace Chessy.Game.Model.Component
{
    public struct NeedStepsC
    {
        readonly float[] _needStepsForShift;

        internal float[] NeedStepsCopy => (float[])_needStepsForShift.Clone();
        public float NeedSteps(in byte cell) => _needStepsForShift[cell];

        internal NeedStepsC(in float[] needSteps) => _needStepsForShift = needSteps;

        internal void Set(in byte cell, in float steps) => _needStepsForShift[cell] = steps;
        internal void Sync(in float[] steps)
        {
            for (int i = 0; i < steps.Length; i++)
            {
                _needStepsForShift[i] = steps[i];
            }
        }
    }
}