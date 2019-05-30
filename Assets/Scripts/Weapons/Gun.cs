using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Projectiles;

public class Gun : Weapon
{
    public int maxReserve = 500, maxClip = 20;
    private int currentReserve = 0, currentClip = 0;
    public float spread = 2f, recoil = 1f, range = 10f;
    [BoxGroup("References")] public Transform shotOrigin;
    [BoxGroup("References")] public GameObject projectilePrefab;

    private CameraLook camLook;

    public void Awake()
    {
        camLook = FindObjectOfType<CameraLook>();
    }

    public void Reload()
    {
        if (currentReserve > 0)
        {
            if (currentReserve >= maxClip)
            {
                currentReserve -= maxClip - currentClip;
                currentClip = maxClip;
            }
            if (currentClip < maxClip)
            {                
                currentClip = currentReserve;
                currentReserve -= currentReserve;
            }
        }
    }

    public override void Attack()
    {
        currentClip--;
        if (currentClip == 0)
        {
            Reload();
        }  

        Camera attachedCamera = Camera.main;
        Transform camTransform = attachedCamera.transform;
        Vector3 bulletOrigin = camTransform.position;
        Quaternion bulletRotation = camTransform.rotation;
        Vector3 lineOrigin = shotOrigin.position;
        Vector3 direction = camTransform.forward;

        GameObject clone = Instantiate(projectilePrefab, camTransform.position, camTransform.rotation);
        Projectile projectile = clone.GetComponent<Projectile>();
        projectile.Fire(lineOrigin, direction);

        Vector3 euler = Vector3.up * 2f;
        euler.x = Random.Range(-1f, 1f);
        camLook.SetTargetOffset(euler * recoil);

        base.Attack();
    }
}
