using UnityEngine;

namespace Code.Scripts.Shoot
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private int lifeTime;
        
        private Rigidbody _rigidbody;

        public Vector3 MoveDirection
        {
            get => transform.forward;
            set
            {
                value.Normalize();
                transform.forward = value;
                _rigidbody.velocity = value * moveSpeed;
            }
        }

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void Start() => Destroy(gameObject, lifeTime);

        private void OnCollisionEnter(Collision collision)
        {
            switch (collision.collider.tag)
            {
                case "staticCube":
                    MoveDirection = Vector3.Reflect(MoveDirection, collision.contacts[0].normal);
                    break;
                case "bouncerCube":
                    Destroy(gameObject);
                    break;
                case "destroyerCube":
                    Destroy(gameObject);
                    Destroy(collision.collider.gameObject);
                    break;
            }
        }
    }
}