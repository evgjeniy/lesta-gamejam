using System.Collections;
using UnityEngine;

namespace Code.Scripts.Doors
{
    public class SlidingDoor : OpenableDoor
    {
        [SerializeField] private Transform door;
        [SerializeField] private Transform destination;
        [SerializeField] private float speed;

        private Coroutine _open;

        public override void OpenDoor()
        {
            Debug.Log("Open Door");
            _open ??= StartCoroutine(OpenDoorCoroutine());
        }

        private IEnumerator OpenDoorCoroutine()
        {
            var startTime = Time.time;
            var startPosition = door.position;

            var distanceCovered = (Time.time - startTime) * speed;
            var journeyFraction = distanceCovered / Vector3.Distance(startPosition, destination.position);
            
            while (Vector3.Distance(door.position, destination.position) > 0.1f)
            {
                door.position = Vector3.Lerp(startPosition, destination.position, journeyFraction);
                yield return null;
            }
        }
    }
}
