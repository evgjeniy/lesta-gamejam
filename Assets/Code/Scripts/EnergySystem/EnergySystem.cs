using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.EnergySystem
{
    public class EnergySystem : MonoBehaviour
    {
        [Header("Development stage adds")]
        [SerializeField] private EnergySpender spenderPrefab;
        [SerializeField] private float energyRechargeRate;
        [SerializeField] private int minimalEnergyToAddSpender;
        [SerializeField] private int maxEnergy;

        private float _currentEnergy;
        private List<EnergySpender> _spenders = new List<EnergySpender>();

        public Action<float> OnEnergyChanged;

        public int MaxEnergy
        {
            get => maxEnergy;
            set => maxEnergy = value > 0 ? value : 0;
        }

        public float CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                _currentEnergy = Mathf.Clamp(value, 0, maxEnergy);
                OnEnergyChanged?.Invoke(_currentEnergy);
            }
        }

        private void Update()
        {
            /*
            if (Input.GetMouseButtonDown(0))
            {
                var newSpender = GameObject.Instantiate(spenderPrefab);
                AddSpender(newSpender);
            }
            */
            CheckCurrentEnergy();
            RecountEnergy();
        }

        private void OnEnable()
        {
            CurrentEnergy = MaxEnergy;
        }
        
        public void AddSpender(EnergySpender spender)
        {
            if (_currentEnergy >= minimalEnergyToAddSpender)
            {
                if (!_spenders.Contains(spender))
                {
                    _spenders.Add(spender);
                }
            }
        }

        public void RemoveSpender(EnergySpender spender)
        {
            if (_spenders.Contains(spender))
            {
                _spenders.Remove(spender);
                spender.Unsubscribe();
            }
        }

        private void RemoveAllSpenders()
        {
            foreach (var spender in _spenders)
            {
                spender.Unsubscribe();
            }

            _spenders.Clear();
        }

        private void RecountEnergy()
        {
            if (_spenders.Count > 0)
            {
                foreach (var spender in _spenders)
                {
                    CurrentEnergy -= spender.EnergySpend;
                }
            }
            else
            {
                CurrentEnergy += energyRechargeRate;
            }
        }

        private void CheckCurrentEnergy()
        {
            if (_currentEnergy <= 0)
            {
                RemoveAllSpenders();
            }
        }
    }
}