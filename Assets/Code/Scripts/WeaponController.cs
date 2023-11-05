using Code.Scripts.EnergySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Projectile> weaponType;
    [SerializeField] private EnergySystem energySystem;

    private int _currentProjectileType;

    private int CurrentType
    {
        get => _currentProjectileType;
        set
        {
            if (value < 0)
                _currentProjectileType = weaponType.Count - 1;
            else if (value > weaponType.Count - 1)
                _currentProjectileType = 0;
            else
                _currentProjectileType = value;
        }
    }

    public void ChangeTypeNext()
    {
        CurrentType++;
    }

    public void ChangeTypePrev()
    {
        CurrentType--;
    }

    public void Shoot(Vector3 direction)
    {
        SpawnProjectile(direction);
    }

    private Projectile SpawnProjectile(Vector3 direction)
    {
        var result = Instantiate(weaponType[CurrentType], transform.position, Quaternion.identity);
        result.MoveDirection = direction;
        result.energySystem = energySystem;
        return result;
    }
}
