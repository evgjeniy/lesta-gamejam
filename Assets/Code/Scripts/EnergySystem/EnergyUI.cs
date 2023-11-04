using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.EnergySystem
{
    public class EnergyUI : MonoBehaviour
    {
        [SerializeField] private EnergySystem energySystem;
        [SerializeField] private Slider energySlider;

        private void OnEnable()
        {
            energySystem.OnEnergyChanged += SetCurrentValue;
        }

        private void OnDisable()
        {
            energySystem.OnEnergyChanged -= SetCurrentValue;
        }

        private void Start()
        {
            SetMaxValue(energySystem.MaxEnergy);
        }
        
        public void SetMaxValue(int value)
        {
            energySlider.maxValue = value;
        }

        public void SetCurrentValue(float value)
        {
            energySlider.value = value;
        }
        
    }
}