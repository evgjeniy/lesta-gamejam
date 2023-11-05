using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timeline : MonoBehaviour, ITimeStopable, ITimeReversable
{
    public abstract void StartReverse();

    public abstract void StartTime();

    public abstract void StopReverse();

    public abstract void StopTime();
}
