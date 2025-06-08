using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO so;
    public WeaponSO GetWeaponSO() { return so; }
    public void setup(WeaponSO w)
    {
        so = w;
    }
}
