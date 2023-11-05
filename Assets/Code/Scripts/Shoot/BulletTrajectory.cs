using UnityEngine;

public class BulletTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject player;
    [SerializeField] private float maxLen;

    void LateUpdate()
    {
        Vector3 firstPoint = player.transform.position;    
        lr.SetPosition(0, firstPoint);                      //первая точка
        Vector3 secondPoint = Input.mousePosition;
        secondPoint.z = Mathf.Abs(player.transform.position.z - Camera.main.transform.position.z); //вторая точка
        secondPoint = Camera.main.ScreenToWorldPoint(secondPoint);
        if (Vector3.Distance(firstPoint, secondPoint) > maxLen) //проверка длины
        {
            Vector3 vec = secondPoint - firstPoint;
            vec = vec.normalized * maxLen;              //приведение к максимальной длине
            secondPoint = firstPoint + vec;
        }
        lr.SetPosition(1, secondPoint);
    }
}
