using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private OpenableDoor connectedDoor;
    private void Start()
    {
        gameObject.GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ConfigurableJoint>() != null)
        {
            connectedDoor.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ConfigurableJoint>() != null)
        {
            connectedDoor.OpenDoor();
        }
    }
}
