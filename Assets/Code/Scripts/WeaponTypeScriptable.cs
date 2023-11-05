using Code.Scripts.Shoot;
using UnityEngine;

namespace Code.Scripts
{
    [CreateAssetMenu(fileName = "WeaponScriptable", menuName = "Weapon/WeaponType", order =1)]
    public class WeaponTypeScriptable : ScriptableObject
    {
        public Projectile ProjectileType;
        public Color ProjectileColor;
    
    }
}
