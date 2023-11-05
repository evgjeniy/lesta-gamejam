using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingDoor : OpenableDoor
{
    [SerializeField] private Transform door;

    [SerializeField] private Transform destination;

    [SerializeField] private float speed;

    private Coroutine open;

    public override void OpenDoor()
    {
        Debug.Log("Open Door");
        if(open == null)
            open = StartCoroutine(OpenDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        float startTime = Time.time;
        Vector3 startPosition = door.position;
        while (Vector3.Distance(door.position, destination.position) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float journeyFraction = distanceCovered / Vector3.Distance(door.position, destination.position);

            door.position = Vector3.Lerp(startPosition, destination.position, journeyFraction);
            yield return null;
        }
    }
}
