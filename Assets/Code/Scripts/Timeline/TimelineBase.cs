using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class TimelineBase<T> : Timeline where T : ITimeStamp
{
    // Duration of timeline in seconds
    [SerializeField] protected int duration = 5;
    // Frames per second
    [SerializeField] protected int framerate = 24;
    [SerializeField] protected float reverseSpeed = 0.5f;

    protected ITimeObject<T> timeObject;

    protected Deque<T> _frames;

    // Timeline duration in frames
    public int TimelineFrames => duration * framerate;

    protected float _timelineShift;
    // Last taken frame time
    protected float _lastFrame;
    // Period between frames in seconds
    protected float _framePeriod;

    private void Update()
    {
        if (_isStoped) return;
        if (Time.time - _lastFrame < _framePeriod) return;

        CaptureState();

        if (_frames.Count >= TimelineFrames)
            RemoveFirstFrame();
    }

    private void CaptureState()
    {
        _lastFrame = Time.time;
        _frames.AddBack(timeObject.CaptureState(Time.time));
    }

    private void RemoveFirstFrame()
    {
        _frames.RemoveFront();
    }

    public override void StartReverse()
    {
        _isReversed = true;
        StopTime();
        StartCoroutine(Reverse());
    }

    public override void StopReverse()
    {
        _isReversed = false;
    }

    public override void StopTime()
    {
        timeObject.StopTime();
        _isStoped = true;
    }

    public override void StartTime()
    {
        timeObject.StartTime();
        _isStoped = false;
    }

    protected abstract IEnumerator Reverse();
}
