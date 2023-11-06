using Code.Scripts.Services;
using System;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.KillSpawn
{
    public class Destroyable : MonoBehaviour, IDestroyable
    {
        [SerializeField] private float destroySpeed;
        [SerializeField] private Renderer nrenderer;
        [SerializeField] private PlayerInputBehaviour playerInputBehaviour;

        public event Action<IDestroyable> Destroyed;

        private Coroutine review;

        public void KillObj()
        {
            if(review != null)
                StopCoroutine(review);

            StartCoroutine(KillEffect());
        }

        private void Awake()
        {
            review = StartCoroutine(ReviewEffect());
        }

        private IEnumerator KillEffect()
        {
            playerInputBehaviour?.InputActions.Disable();
            float progress = nrenderer.material.GetFloat("_Progress");
            while (progress < 1.0f)
            {
                progress += Time.deltaTime * destroySpeed;
                nrenderer.material.SetFloat("_Progress", progress);
                yield return null;
            }
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }

        private IEnumerator ReviewEffect()
        {
            float progress = 1;
            while (progress > 0.0f)
            {
                progress -= Time.deltaTime * destroySpeed;
                nrenderer.material.SetFloat("_Progress", progress);
                yield return null;
            }
        }
    }
}
