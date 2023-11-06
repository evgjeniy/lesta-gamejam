using System;
using UnityEngine;

namespace Code.Scripts.Energy
{
    public class EnergySpender: MonoBehaviour
    {
        [SerializeField] private float energySpendPerTick;

        public float EnergySpend => energySpendPerTick;

        public event Action OnDestroy;

        public void Unsubscribe()
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}