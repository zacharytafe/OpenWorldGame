using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Projectiles;

public abstract class Weapon : MonoBehaviour
{
    [BoxGroup("Stats")] public int damage = 10, maxAmmo = 500, maxClip = 20;
    [BoxGroup("Stats")] public float spread = 2f, recoil = 1f, range = 10f, attackRate = .2f;
    [BoxGroup("References")] public Transform shotOrigin;
    [BoxGroup("References")] public GameObject bulletPrefab;

    [HideInInspector] public bool canShoot = false;

    private float attackTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackRate)
        {
            canShoot = true;
        }
    }


    public virtual void Attack()
    {
        attackTimer = 0f;

        canShoot = false;
    }
}
