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
                Object.Destroy(gameObject);
                break;
            case "destroyerCube":   //����������� ��� (������ ��������)
                Object.Destroy(gameObject);
                break;
        }
    }
}