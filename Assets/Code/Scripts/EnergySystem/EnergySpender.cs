using UnityEngine;

namespace Code.EnergySystem
{
    public class EnergySpender: MonoBehaviour
    {
        [SerializeField] private float energySpendPerTick;

        public float EnergySpend => energySpendPerTick;

        public void Unsubscribe()
        {
            gameObject.SetActive(false);
        }
    }
}