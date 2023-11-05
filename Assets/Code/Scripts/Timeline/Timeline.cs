using UnityEngine;

namespace Code.Scripts.Timeline
{
    public abstract class Timeline : MonoBehaviour, ITimeStopable, ITimeReversable
    {

        protected bool _isStoped = false;
        protected bool _isReversed = false;

        public bool IsReversed => _isReversed;
        public bool IsStoped => _isStoped;

        public abstract void StartReverse();

        public abstract void StartTime();

        public abstract void StopReverse();

        public abstract void StopTime();
    }
}
