using System.Collections;
using UnityEngine;

namespace Code.Scripts.Doors
{
    public class RotatingDoor : OpenableDoor
    {
        [SerializeField] private Transform rotatingPart;
        [SerializeField] private float maxOpenAngle;
        [SerializeField] private float timeToOpen;

        private Coroutine _animationCoroutine;
        private Vector3 _startRotation;

        private void OnEnable()
        {
            _startRotation = rotatingPart.transform.rotation.eulerAngles;
        }

        public override void OpenDoor()
        {
            if (!IsOpen)
            {
                if (_animationCoroutine != null)
                {
                    StopCoroutine(_animationCoroutine);
                    IsOpening = false;
                    IsClosing = false;
                }

                _animationCoroutine = StartCoroutine(IsOpening ? DoRotationClose() : DoRotationOpen());
            }
            else
            {
                _animationCoroutine = StartCoroutine(DoRotationClose());
            }
        }

        private IEnumerator DoRotationOpen()
        {
            var startRotation = rotatingPart.rotation.eulerAngles;
            var endRotation = new Vector3(startRotation.x, _startRotation.y - maxOpenAngle, startRotation.z);

            IsOpening = true;
            float openPercent = 0;
            while (openPercent < 1)
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
            var startRotation = rotatingPart.rotation.eulerAngles;
            var endRotation = new Vector3(startRotation.x, _startRotation.y, startRotation.z);

            IsClosing = true;
            float openPercent = 0;
            while (openPercent < 1)
            {
                rotatingPart.rotation = Quaternion.Euler(Vector3.Slerp(startRotation, endRotation, openPercent));
                yield return null;
                openPercent += Time.deltaTime * Speed;
            }

            IsClosing = false;
            IsOpen = false;
        }
    }
}