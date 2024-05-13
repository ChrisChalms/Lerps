namespace CC.Lerps
{
    public class LerpSettings<T>
    {
        public T Start { get; set; }
        public T End { get; set; }
        public int DurationInMilliseconds { get; set; }
        public Easing.Functions? EaseType { get; set; } = null;

        public LerpSettings() { }
        public LerpSettings(T start, T end, int durationInMilliseconds, Easing.Functions easeType)
        {
            Start = start;
            End = end;
            DurationInMilliseconds = durationInMilliseconds;
            EaseType = easeType;
        }
    }
}