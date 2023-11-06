using System.Collections;
using UnityEngine;

namespace Code.Scripts.Timeline
{
    public class TransformStateObject : MonoBehaviour, ITimeObject<TransformState>
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void StopTime()
        {
            _rigidbody.isKinematic = true;
        }

        public void StartTime()
        {
            _rigidbody.isKinematic = false;
        }

        public TransformState CaptureState(float time)
        {
            return new TransformState
            (
                transform.position,
                transform.rotation,
                _rigidbody.velocity,
                time
            );
        }

        public void SetState(TransformState state)
        {
            if(state.Position.x != float.NaN && state.Position.y != float.NaN && state.Position.z != float.NaN)
                transform.position = state.Position;
            transform.rotation = state.Rotation;

            if (!_rigidbody.isKinematic)
                _rigidbody.velocity = state.Velocity;
        }

        public IEnumerator SetStateSmooth(TransformState state, float timeInterval)
        {
            float distance = Vector3.Distance(transform.position, state.Position);

            if (distance == 0)
            {
                yield return new WaitForSeconds(timeInterval);
            }
            else
            {
                float startTime = Time.time;
                while (Vector3.Distance(transform.position, state.Position) > 0.01f)
                {
                    float elapsed = Time.time - startTime;
                    float t = Mathf.Clamp01(elapsed / timeInterval);

                    var position = Vector3.Lerp(transform.position, state.Position, t);
                    var rotation = Quaternion.Lerp(transform.rotation, state.Rotation, t);

                    SetState(new TransformState(position, rotation, _rigidbody.velocity, 0));

                    yield return null;
                }
            }
        }
    }
}
