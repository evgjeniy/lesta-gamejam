using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Doors
{
    public class PhysicsButton : MonoBehaviour
    {
        public Rigidbody buttonTopRigid;
        public Transform buttonTop;
        public Transform buttonLowerLimit;
        public Transform buttonUpperLimit;
        public float threshHold;
        public float force = 10;
        private float upperLowerDiff;
        public bool isPressed;
        private bool prevPressedState;
        public AudioSource pressedSound;
        public AudioSource releasedSound;
        public Collider[] CollidersToIgnore;
        public UnityEvent onPressed;
        public UnityEvent onReleased;

        private void Start()
        {
            var localCollider = GetComponent<Collider>();
            if (localCollider != null)
            {
                Physics.IgnoreCollision(localCollider, buttonTop.GetComponentInChildren<Collider>());

                foreach (var singleCollider in CollidersToIgnore)
                {
                    Physics.IgnoreCollision(localCollider, singleCollider);
                }
            }

            if (transform.eulerAngles != Vector3.zero)
            {
                var savedAngle = transform.eulerAngles;
                transform.eulerAngles = Vector3.zero;
                upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
                transform.eulerAngles = savedAngle;
            }
            else
            {
                upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Pressed();
        }

        private void Update()
        {
            buttonTop.transform.localPosition = new Vector3(0, buttonTop.transform.localPosition.y, 0);
            buttonTop.transform.localEulerAngles = new Vector3(0, 0, 0);
            if (buttonTop.localPosition.y >= 0)
                buttonTop.transform.position = new Vector3(buttonUpperLimit.position.x, buttonUpperLimit.position.y,
                    buttonUpperLimit.position.z);
            else
                buttonTopRigid.AddForce(buttonTop.transform.up * force * Time.deltaTime);

            if (buttonTop.localPosition.y <= buttonLowerLimit.localPosition.y)
                buttonTop.transform.position = new Vector3(buttonLowerLimit.position.x, buttonLowerLimit.position.y,
                    buttonLowerLimit.position.z);


            isPressed = Vector3.Distance(buttonTop.position, buttonLowerLimit.position) < upperLowerDiff * threshHold;

            if (isPressed && prevPressedState != isPressed) Pressed();
            if (!isPressed && prevPressedState != isPressed) Released();
        }

        /*
        private void FixedUpdate(){
            var localVelocity = transform.InverseTransformDirection(buttonTop.GetComponent<Rigidbody>().velocity);
            var rb = buttonTop.GetComponent<Rigidbody>();
            localVelocity.x = 0;
            localVelocity.z = 0;
            rb.velocity = transform.TransformDirection(localVelocity);
        }
        */

        private void Pressed()
        {
            prevPressedState = isPressed;
            pressedSound.pitch = 1;
            pressedSound.Play();
            onPressed.Invoke();
        }

        private void Released()
        {
            prevPressedState = isPressed;
            releasedSound.pitch = Random.Range(1.1f, 1.2f);
            releasedSound.Play();
            onReleased.Invoke();
        }
    }
}