using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpenableDoor : MonoBehaviour
{
    public bool IsOpening;
    public bool IsClosing;
    public bool IsOpen;
    public float Speed;

    public virtual void OpenDoor()
    {
    }
}
