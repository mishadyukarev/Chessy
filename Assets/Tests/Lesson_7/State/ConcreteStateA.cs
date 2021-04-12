namespace BehavioralPatterns.State.ExampleFirst
{
    public sealed class ConcreteStateA : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateB();
        }
    }
}
