using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using NaughtyAttributes;


[RequireComponent(typeof(PlayerMove), typeof(CameraLook))]
public class Combat : MonoBehaviour
{
    [ProgressBar("Health", 100, ProgressBarColor.Blue)]
    public int health = 100;

    [BoxGroup("Weapon")] public Weapon currentWeapon;
    [BoxGroup("Weapon")] public List<Weapon> weapons = new List<Weapon>();
    [BoxGroup("Weapon")] public int currentWeaponIndex = 0;

    private PlayerMove player;
    private CameraLook cameraLook;

    void Awake()
    {
        player = GetComponent<PlayerMove>();
        cameraLook = GetComponent<CameraLook>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get all weapons attached to player
        weapons = GetComponentsInChildren<Weapon>().ToList();

        SelectWeapon(0);
    }

    // Update is called once per frame
    void FixeddUpdate()
    {
        // If there is a weapon
        if (currentWeapon)
        {
            bool fire1 = Input.GetButton("Fire1");
            if (fire1)
            {
                // Check if weapon can shoot
                if (currentWeapon.canShoot)
                {
                    currentWeapon.Attack();

                    currentWeapon.Attack();
                }
            }
        }
    }

    void DisableAllWeapons()
    {
        foreach (var item in weapons)
        {
            item.gameObject.SetActive(false);
        }
    }

    void SelectWeapon(int index)
    {
        // Check if index is within bounds
        if(index >= 0 && index < weapons.Count)
        {
            DisableAllWeapons();
            currentWeapon = weapons[index];
            currentWeapon.gameObject.SetActive(true);
            currentWeaponIndex = index;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            print("Dead");
        }
    }

    public void Heal(int heal)
    {
        health += heal;
    }
}
