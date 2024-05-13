using System;
using UnityEngine;

namespace CC.Lerps
{
    // Entry point for all things lerping
    public static class Lerper
    {
        const Easing.Functions DefaultEaseType = Easing.Functions.QuadraticEaseInOut;

        public static Lerp<float> Lerp(float start, float end, int durationInMilliseconds, Easing.Functions easeType = DefaultEaseType, Action<float> onUpdate = null, Action onCompleted = null)
            => LerpInternal(start, end, durationInMilliseconds, Mathf.Lerp, easeType, onUpdate, onCompleted);

        public static Lerp<Vector2> Lerp(Vector2 start, Vector2 end, int durationInMilliseconds, Easing.Functions easeType = DefaultEaseType, Action<Vector2> onUpdate = null, Action onCompleted = null)
            => LerpInternal(start, end, durationInMilliseconds, Vector2.Lerp, easeType, onUpdate, onCompleted);

        public static Lerp<Vector3> Lerp(Vector3 start, Vector3 end, int durationInMilliseconds, Easing.Functions easeType = DefaultEaseType, Action<Vector3> onUpdate = null, Action onCompleted = null)
            => LerpInternal(start, end, durationInMilliseconds, Vector3.Lerp, easeType, onUpdate, onCompleted);
        public static Lerp<Vector3> Slerp(Vector3 start, Vector3 end, int durationInMilliseconds, Easing.Functions easeType = DefaultEaseType, Action<Vector3> onUpdate = null, Action onCompleted = null)
            => LerpInternal(start, end, durationInMilliseconds, Vector3.Slerp, easeType, onUpdate, onCompleted);

        public static Lerp<Color> Lerp(Color start, Color end, int durationInMilliseconds, Easing.Functions easeType = DefaultEaseType, Action<Color> onUpdate = null, Action onCompleted = null)
            => LerpInternal(start, end, durationInMilliseconds, Color.Lerp, easeType, onUpdate, onCompleted);

        public static Lerp<Quaternion> Lerp(Quaternion start, Quaternion end, int durationInMilliseconds, Easing.Functions easeType = DefaultEaseType, Action<Quaternion> onUpdate = null, Action onCompleted = null)
            => LerpInternal(start, end, durationInMilliseconds, Quaternion.Lerp, easeType, onUpdate, onCompleted);
        public static Lerp<Quaternion> Slerp(Quaternion start, Quaternion end, int durationInMilliseconds, Easing.Functions easeType = DefaultEaseType, Action<Quaternion> onUpdate = null, Action onCompleted = null)
            => LerpInternal(start, end, durationInMilliseconds, Quaternion.Slerp, easeType, onUpdate, onCompleted);

        public static Lerp<T> Lerp<T>(LerpSettings<T> settings, Action<T> onUpdate = null, Action onCompleted = null)
            => LerpInternal(settings.Start, settings.End, settings.DurationInMilliseconds, GetLerpFunc<T>(), settings.EaseType ?? DefaultEaseType, onUpdate, onCompleted);

        public static LerpDelay Delay(int delayInMilliseconds) => new LerpDelay(delayInMilliseconds);

        static Lerp<T> LerpInternal<T>(T start, T end, int durationInMilliseconds, Func<T, T, float, T> lerpFunc,
            Easing.Functions easeType = DefaultEaseType, Action<T> onUpdate = null, Action onCompleted = null) =>
            new(start, end, durationInMilliseconds, lerpFunc, easeType, onUpdate, onCompleted);
        
        static Func<T, T, float, T> GetLerpFunc<T>()
        {
            if (typeof(T) == typeof(float))
            {
                return new Func<float, float, float, float>(Mathf.Lerp) as Func<T, T, float, T>;
            }
            if (typeof(T) == typeof(Vector2))
            {
                return new Func<Vector2, Vector2, float, Vector2>(Vector2.Lerp) as Func<T, T, float, T>;
            }
            if (typeof(T) == typeof(Vector3))
            {
                return new Func<Vector3, Vector3, float, Vector3>(Vector3.Lerp) as Func<T, T, float, T>;
            }
            if (typeof(T) == typeof(Color))
            {
                return new Func<Color, Color, float, Color>(Color.Lerp) as Func<T, T, float, T>;
            }
            if (typeof(T) == typeof(Quaternion))
            {
                return new Func<Quaternion, Quaternion, float, Quaternion>(Quaternion.Lerp) as Func<T, T, float, T>;
            }

            throw new NotImplementedException($"Couldn't get lerp func for type {typeof(T)}");
        }
    }
}