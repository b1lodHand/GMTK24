namespace com.absence.variablesystem.mutations
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Use to add a timed mutation to a variable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <param name="mutation"></param>
        public static void AddTimedMutation<T>(this Variable<T> v, TimedMutation<T> mutation)
        {
            v.AddMutation(mutation);
            mutation.Timer.OnComplete += (s) => v.RemoveMutation(mutation);
        }

        /// <summary>
        /// Use to add a timed mutation to a verctor3 variable.
        /// </summary>
        /// <param name="v3"></param>
        /// <param name="mutation"></param>
        public static void AddTimedMutation(this Variable_Vector3 v3, TimedMutation<float> mutation)
        {
            v3.X.AddTimedMutation(mutation);
            v3.Y.AddTimedMutation(mutation);
            v3.Z.AddTimedMutation(mutation);
        }
    }
}
