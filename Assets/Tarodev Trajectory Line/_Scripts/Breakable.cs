using UnityEngine;

namespace Tarodev_Trajectory_Line._Scripts
{
    public class Breakable : MonoBehaviour {
        [SerializeField] private GameObject _replacement;
        [SerializeField] private float _breakForce = 2;
        [SerializeField] private float _collisionMultiplier = 100;
        [SerializeField] private LayerMask layerMask;

        private bool _broken;

        private void OnCollisionEnter(Collision collision) {
            if (_broken) return;
            if (((layerMask.value >> collision.gameObject.layer) & 1) != 1) return;

            if (collision.relativeVelocity.magnitude >= _breakForce) {
                _broken = true;
                var replacement = Instantiate(_replacement, transform.position, transform.rotation);
                replacement.transform.localScale = transform.localScale;

                var rbs = replacement.GetComponentsInChildren<Rigidbody>();
                foreach (var rb in rbs) {
                    rb.AddExplosionForce(collision.relativeVelocity.magnitude * _collisionMultiplier, collision.contacts[0].point, 2);
                }

                gameObject.SetActive(false);
            }
        }
    }
}