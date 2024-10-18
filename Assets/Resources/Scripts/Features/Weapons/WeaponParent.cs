using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private GameObject parent;

    private int swordCount = 0;

    public List<Weapon> swordWeapons;

    [SerializeField] private float rotationSpeed;

    private float angle = 0;

    private void OnEnable()
    {
        Weapon.OnOrbitAttacked += CalcSwordsPos;
        Weapon.OnOrbitAttacked += OrbitWeaponMove;
    }

    void OnDisable()
    {
        swordCount = 0;
        swordWeapons.Clear();
        angle = 0;

        Weapon.OnOrbitAttacked -= CalcSwordsPos;
        Weapon.OnOrbitAttacked -= OrbitWeaponMove;
    }

    void CalcSwordsPos()
    {
        if (swordCount < swordWeapons.Count)
        {
            swordCount = swordWeapons.Count;

            if (swordCount < 2)
                rotationSpeed = 100f;
            else
                rotationSpeed = 200f / swordCount;

            float swordAngle = 360f / swordWeapons.Count;

            int i = 0;

            foreach (Weapon sword in swordWeapons)
            {
                sword.prefabParent.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, swordAngle * i);
                i++;
            }
        }
    }

    private void OrbitWeaponMove()
    {
        angle -= rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}