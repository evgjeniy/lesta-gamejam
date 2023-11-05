﻿using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.EnergySystem
{
    public class EnergyUI : MonoBehaviour
    {
        [SerializeField] private EnergySystem energySystem;
        [SerializeField] private Slider energySlider;

        private void OnEnable()
        {
            if (energySystem == null) return;
            energySystem.OnEnergyChanged += SetCurrentValue;
        }

        private void OnDisable()
        {
            if (energySystem == null) return;
            energySystem.OnEnergyChanged -= SetCurrentValue;
        }

        public void BindPlayer(EnergySystem energySystem)
        {
            energySystem.OnEnergyChanged += SetCurrentValue;
            SetCurrentValue(energySystem.CurrentEnergy);
        }

        private void Start()
        {
            if (energySystem == null) return;
            SetCurrentValue(energySystem.CurrentEnergy);
        }

        public void SetCurrentValue(float value)
        {
            energySlider.value = value;
        }
        
    }
}