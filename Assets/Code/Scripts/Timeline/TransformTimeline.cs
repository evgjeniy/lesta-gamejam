using System.Collections;
using System.Linq;
using UnityEngine;

public class TransformTimeline : TimelineBase<TransformState>
{
    [SerializeField] TransformStateObject timeGameObject;

    [Header("Gizmos")]
    [SerializeField] protected bool gizmosVisualization;
    // Mesh for state visualization
    [SerializeField] Mesh gizmosMesh;

    private void Awake()
    {
        _framePeriod = 1f / framerate;
        _frames = new Deque<TransformState>(TimelineFrames);
        timeObject = timeGameObject;
    }

    protected override IEnumerator Reverse()
    {
        if(_frames.Count == 0) yield break;

        TransformState previousFrame = _frames.Last();
        foreach (var frame in _frames.Reverse())
        {
            yield return timeObject.SetStateSmooth(frame, Mathf.Abs(previousFrame.Time - frame.Time) / reverseSpeed);

            if (!_isReversed) break;

            previousFrame = frame;
        }
        _frames.Cutoff(previousFrame);
        yield break;
    }

    private void OnDrawGizmos()
    {
        if (!gizmosVisualization || _frames == null || !gizmosMesh) return;

        foreach (var frame in _frames)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireMesh(gizmosMesh, frame.Position, frame.Rotation, Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(frame.Position, (frame.Position + frame.Velocity).normalized);
        }
    }
}
