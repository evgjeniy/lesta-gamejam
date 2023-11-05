using UnityEngine;

namespace Code.Scripts.Shoot
{
    public class BulletTrajectory : MonoBehaviour
    {
        [SerializeField] private LineRenderer lr;
        [SerializeField] private GameObject player;
        [SerializeField] private float maxLen;

        private void LateUpdate()
        {
            var firstPoint = player.transform.position;    
            lr.SetPosition(0, firstPoint);                      //ïåðâàÿ òî÷êà
            var secondPoint = Input.mousePosition;
            secondPoint.z = Mathf.Abs(player.transform.position.z - Camera.main.transform.position.z); //âòîðàÿ òî÷êà
            secondPoint = Camera.main.ScreenToWorldPoint(secondPoint);
            if (Vector3.Distance(firstPoint, secondPoint) > maxLen) //ïðîâåðêà äëèíû
            {
                var vec = secondPoint - firstPoint;
                vec = vec.normalized * maxLen;              //ïðèâåäåíèå ê ìàêñèìàëüíîé äëèíå
                secondPoint = firstPoint + vec;
            }
            lr.SetPosition(1, secondPoint);
        }
    }
}
