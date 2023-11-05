using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class RotatingDoor : OpenableDoor
{
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private float maxOpenAngle;
    [SerializeField] private float timeToOpen;

    private Coroutine AnimationCoroutine;

    private Vector3 _startRotation;
    // Start is called before the first frame update
    void OnEnable()
    {
        _startRotation = rotatingPart.transform.rotation.eulerAngles;
    }
    

    public override void OpenDoor()
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
                IsOpening = false;
                IsClosing = false;
            }
            AnimationCoroutine = StartCoroutine(IsOpening ? DoRotationClose() : DoRotationOpen());
        }
        else
        {
            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationOpen()
    {
        Vector3 startRotation = rotatingPart.rotation.eulerAngles;
        Vector3 endRotation = new Vector3( startRotation.x, _startRotation.y - maxOpenAngle, startRotation.z);

        IsOpening = true;
        float openPercent = 0;
        while (openPercent<1)
        {
            rotatingPart.rotation = Quaternion.Euler(Vector3.Slerp(startRotation, endRotation, openPercent));
            yield return null;
            openPercent += Time.deltaTime * Speed;
        }

        IsOpening = false;
        IsOpen = true;
    }

    private IEnumerator DoRotationClose()
    {
        Vector3 startRotation = rotatingPart.rotation.eulerAngles;
        Vector3 endRotation = new Vector3( startRotation.x, _startRotation.y, startRotation.z);

        IsClosing = true;
        float openPercent = 0;
        while (openPercent<1)
        {
            rotatingPart.rotation = Quaternion.Euler(Vector3.Slerp(startRotation, endRotation, openPercent));
            yield return null;
            openPercent += Time.deltaTime * Speed;
        }

        IsClosing = false;
        IsOpen = false;
    }
    
}
