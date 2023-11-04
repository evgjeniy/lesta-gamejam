using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rgBullet;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int lifeTime;
    [SerializeField] private Vector3 moveDir;

    private Vector3 lastVelocity;

    void Start()
    {
        moveDir = moveDir.normalized;
        rgBullet.velocity = moveDir * moveSpeed;    //инициализация velocity
        Destroy(gameObject, lifeTime);              //исчезнет через lifeTime секунд
    }

    private void LateUpdate()
    {
        lastVelocity = rgBullet.velocity;       //запоминаем velocity
    }

    void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);      //движение
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.gameObject.tag)
        {
            case "staticCube":      //статический куб (отскок снаряда)
                moveDir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
                rgBullet.velocity = moveDir * moveSpeed;
                break;
            case "bouncerCube":     //куб отлетает, снаряд исчезает
                Destroy(gameObject);
                break;
            case "destroyerCube":   //статический куб (снаряд исчезает)
                Destroy(gameObject);
                Destroy(collision.collider.gameObject);
                break;
        }
    }

    public class Factory
    {
        private Projectile _ref;

        public Factory(Projectile visualRef)
        {
            _ref = visualRef;
        }

        public Projectile Create(Vector3 dir)
        {
            var result = Instantiate(_ref);
            result.moveDir = dir;

            return result;
        }
    }
}