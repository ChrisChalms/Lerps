namespace CC.Lerps
{
    public static class LerpExtensions
    {
        public static bool IsNullOrDeleted(this Lerp lerp) => lerp?.IsDeleted ?? true;
    }
}