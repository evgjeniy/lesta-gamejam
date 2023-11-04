using UnityEngine;

public class SnariadMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rgBullet;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int lifeTime;

    public Vector3 moveDir;
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
                Object.Destroy(gameObject);
                break;
            case "destroyerCube":   //статический куб (снаряд исчезает)
                Object.Destroy(gameObject);
                break;
        }
    }
}