using UnityEngine;

namespace Code.Scripts.Timeline
{
    public struct TransformState : ITimeStamp
    {
        [SerializeField] Vector3 _position;
        [SerializeField] Quaternion _rotation;
        [SerializeField] Vector3 _velocity;
        [SerializeField] float _time;

        public TransformState(Vector3 position1, Quaternion rotation, Vector3 position2, float time) : this()
        {
            _position = position1;
            _rotation = rotation;
            _velocity = position2;
            _time = time;
        }

        public Vector3 Position => _position;
        public Quaternion Rotation => _rotation;
        public Vector3 Velocity => _velocity;
        public float Time => _time;
    }
}
