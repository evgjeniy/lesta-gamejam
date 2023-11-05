using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeReversable
{
    public bool IsReversed { get; }
    public void StartReverse();
    public void StopReverse();
}
