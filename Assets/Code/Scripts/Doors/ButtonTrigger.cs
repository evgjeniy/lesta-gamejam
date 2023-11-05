using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Doors
{
    [RequireComponent(typeof(Collider))]
    public class ButtonTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent actionPressed;
        [SerializeField] private UnityEvent actionReleased;

        private void Start()
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ConfigurableJoint>() != null)
            {
                actionPressed?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<ConfigurableJoint>() != null)
            {
                actionReleased?.Invoke();
            }
        }
    }
}