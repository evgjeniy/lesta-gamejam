using UnityEngine;

public class KillerScript : MonoBehaviour
{
    [SerializeField] private LayerMask lm;
    private void OnTriggerEnter(Collider other)
    {
        if (((lm.value >> other.transform.gameObject.layer) & 1) == 1)
        {
            other.attachedRigidbody.TryGetComponent<Destroyable>(out var destroyable);
            destroyable?.KillObj();
        }
    }
}
