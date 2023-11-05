﻿using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Scripts.EnergySystem
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