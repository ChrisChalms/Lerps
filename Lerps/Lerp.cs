using System;
using UnityEngine;

namespace CC.Lerps
{
    // The non-generic lerp with functionality for suspending, resuming, and updating duration mid-Lerp
    public abstract class Lerp : BaseLerp
    {
        public float ElapsedMs { get; protected set; }
        public bool IsRunning { get; protected set; }
        public float PercentageComplete { get; protected set; }

        protected int _duration;
        protected bool _runningPreSuspend;

        public void Reset() => ElapsedMs = 0;

        public void UpdateDuration(int durationInMs)
        {
            _duration = durationInMs;
            ElapsedMs = durationInMs * PercentageComplete;
        }
        
        public override void Suspend()
        {
            _runningPreSuspend = IsRunning;
            IsRunning = false;
        }

        public override void Resume() => IsRunning = _runningPreSuspend;
    }

    /// <summary>
    /// Adds the generic lerping to the <see cref="Lerp"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Lerp<T> : Lerp
    {
        public Action<T> ValueChanged;
        public Action OnCompleted;

        readonly T _startPoint;
        readonly T _endPoint;
        protected Easing.Functions _easeType;
        readonly Func<T, T, float, T> _lerpFunc; // Start, end, percentage

        internal Lerp(T startPoint, T endPoint, int durationInMs, Func<T, T, float, T> lerpFunc, Easing.Functions easeType, Action<T> onUpdate = null, Action onCompleted = null)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _duration = durationInMs;
            _lerpFunc = lerpFunc;
            _easeType = easeType;

            ValueChanged += onUpdate;
            OnCompleted += onCompleted;

            IsRunning = true;
        }

        public override void Step()
        {
            if (IsRunning == false)
            {
                return;
            }

            ElapsedMs += Time.deltaTime * 1000;

            if (ElapsedMs >= _duration)
            {
                IsRunning = false;
                ElapsedMs = _duration;
                ValueChanged?.Invoke(_endPoint);
                OnCompleted?.Invoke();
                Delete();
            }
            else
            {
                PercentageComplete = ElapsedMs / _duration;
                ValueChanged?.Invoke(_lerpFunc.Invoke(_startPoint, _endPoint, Easing.Interpolate(PercentageComplete, _easeType)));
            }
        }

        public override string ToString() => $"[ Start: {_startPoint}, End: {_endPoint}, Duration: {_duration}, ElapsedMS: {ElapsedMs}, EaseType: {_easeType}, LerpFunc: {_lerpFunc}]";
    }

    /// <summary>
    /// Used in the <see cref="LerpSequence"/> as a delay between sequenced <see cref="Lerp"/>
    /// </summary>
    public class LerpDelay : Lerp
    {
        public LerpDelay(int durationInMs)
        {
            _duration = durationInMs;
            IsRunning = true;
        }

        public override void Step()
        {
            if (IsRunning == false)
            {
                return;
            }

            ElapsedMs += Time.deltaTime * 1000;

            if (ElapsedMs <= _duration)
            {
                return;
            }

            IsRunning = false;
            ElapsedMs = _duration;
            Delete();
        }

        public override string ToString() => $"[ Duration: {_duration}, ElapsedMS: {ElapsedMs}]";
    }
}