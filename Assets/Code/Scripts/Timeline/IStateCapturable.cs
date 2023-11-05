using System.Collections;

public interface IStateCapturable <T>
{
    public T CaptureState(float time);
    public void SetState(T state);
    public IEnumerator SetStateSmooth(T state, float timeInterval);
}