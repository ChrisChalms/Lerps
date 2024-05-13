using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.Lerps
{
    /// <summary>
    /// The opposite of a <see cref="LerpGroup"/>.
    /// Encapsulates a queue of <see cref="Lerp"/> and steps through them sequentially in a sequence
    /// </summary>
    public class LerpSequence : BaseLerp
    {
        readonly Queue<Func<Lerp>> _lerps = new();
        Lerp _currentLerp;

        public LerpSequence(params Func<Lerp>[] lerps) => Add(lerps);

        public void Add(Func<Lerp> lerp) => _lerps.Enqueue(lerp);

        public void Add(params Func<Lerp>[] lerps)
        {
            foreach (var lerp in lerps)
            {
                Add(lerp);
            }
        }

        public override void Step()
        {
            if (_currentLerp.IsNullOrDeleted() == false)
            {
                _currentLerp.Step();
                return;
            }

            if (_lerps.Any() == false)
            {
                Delete();
                return;
            }

            _currentLerp = _lerps.Dequeue()?.Invoke();
        }
    }
}