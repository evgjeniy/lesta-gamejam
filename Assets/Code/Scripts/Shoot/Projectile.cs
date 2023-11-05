using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour, IProjectileHit
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
        collision.collider.TryGetComponent<HitResolver>(out var hitResolver);

        if (hitResolver == null) return;

        hitResolver.ResolveHit(this, collision);
    }

    public abstract void OnHitStatic(GameObject obj, Collision context);

    public abstract void OnHitDynamic(GameObject obj, Collision context);

    public abstract void OnHitDestroyable(GameObject obj, Collision context);
}
