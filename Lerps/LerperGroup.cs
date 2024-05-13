using System.Collections.Generic;
using System.Linq;

namespace CC.Lerps
{
    /// <summary>
    /// Encapsulated a group of lerps so they can be suspended, resumed, and ticked as one
    /// </summary>
    public class LerpGroup : BaseLerp
    {
        readonly List<Lerp> _lerps = new();

        public LerpGroup(params Lerp[] lerps) => _lerps = lerps.ToList();

        public void Add(params Lerp[] lerps) => _lerps.AddRange(lerps);

        public override void Step()
        {
            var deletedLerps = 0;
            foreach (var lerp in _lerps)
            {
                if (lerp.IsDeleted)
                {
                    deletedLerps++;
                }
                
                lerp.Step();
            }

            if (deletedLerps == _lerps.Count)
            {
                Delete();
            }   
        }

        public override void Suspend()
        {
            foreach (var lerp in _lerps)
            {
                lerp.Suspend();
            }

            base.Suspend();
        }

        public override void Resume()
        {
            foreach (var lerp in _lerps)
            {
                lerp.Resume();
            }

            base.Resume();
        }
    }
}