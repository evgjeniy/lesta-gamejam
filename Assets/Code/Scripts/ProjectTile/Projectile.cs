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
        rgBullet.velocity = moveDir * moveSpeed;    //������������� velocity
        Destroy(gameObject, lifeTime);              //�������� ����� lifeTime ������
    }

    private void LateUpdate()
    {
        lastVelocity = rgBullet.velocity;       //���������� velocity
    }

    void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);      //��������
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.gameObject.tag)
        {
            case "staticCube":      //����������� ��� (������ �������)
                moveDir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
                rgBullet.velocity = moveDir * moveSpeed;
                break;
            case "bouncerCube":     //��� ��������, ������ ��������
                Destroy(gameObject);
                break;
            case "destroyerCube":   //����������� ��� (������ ��������)
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