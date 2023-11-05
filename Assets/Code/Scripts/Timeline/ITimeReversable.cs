namespace Code.Scripts.Timeline
{
    public interface ITimeReversable
    {
        public bool IsReversed { get; }
        public void StartReverse();
        public void StopReverse();
    }
}
