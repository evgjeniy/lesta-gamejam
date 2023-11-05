using UnityEngine;

namespace Tarodev_Trajectory_Line._Scripts
{
    public class Rotator : MonoBehaviour {
        [SerializeField] private Vector3 _rot;


        private void Update() {
            transform.Rotate(_rot * Time.deltaTime);
        }
    }
}