using System;

namespace CC.Lerps
{
    // Used as a base for all Lerps, groups, sequences, etc..
    // TODO: Do we need to track a state?
    public abstract class BaseLerp
    {
        public bool IsDeleted { get; protected set; }
        
        public event Action Deleted;

        public abstract void Step();
        public virtual void Suspend() { }
        public virtual void Resume() { }
        
        public virtual void Delete()
        {
            if (IsDeleted)
            {
                return;
            }

            IsDeleted = true;
            Deleted?.Invoke();
        }
    }
}