using com.absence.timersystem;

namespace com.absence.variablesystem.mutations
{
    /// <summary>
    /// Lets you mutate a variable for some time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TimedMutation<T> : Mutation<T>
    {
        public float Duration { get; private set; }
        public Timer Timer { get; private set; }

        public TimedMutation(T mutationValue, MutationType mutationType, float duration) : base(mutationValue, mutationType)
        {
            Duration = duration;
            Timer = Timer.Create(duration);
            Timer.Restart();
        }
    }
}