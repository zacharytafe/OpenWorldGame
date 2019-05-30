using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Projectiles.Effects;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : Projectile
    {
        public float speed = 50f;
        [BoxGroup("References")] public GameObject effectPrefab;
        [BoxGroup("References")] public Transform line;

        private Rigidbody rigid;
        private Vector3 start, end;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        void Start()
        {
            start = transform.position;  
        }

        // Update is called once per frame
        void Update()
        {
            line.transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }

        void OnCollisionEnter(Collision col)
        {
            end = transform.position;
            ContactPoint contact = col.contacts[0];

            Vector3 bulletDir = end - start;

            Quaternion lookRotation = Quaternion.LookRotation(bulletDir);
            Quaternion rotation = lookRotation * Quaternion.AngleAxis(-90, Vector3.right);

            GameObject clone = Instantiate(effectPrefab, contact.point, rotation);

            float impactAngle = 180 - Vector3.Angle(bulletDir, contact.normal);
            clone.transform.localScale = clone.transform.localScale / (1 + impactAngle / 45);

            Effect effect = clone.GetComponent<Effect>();
            effect.damage += damage;
            effect.hitObject = col.transform;

            Destroy(gameObject);
        }

        public override void Fire(Vector3 lineOrigin, Vector3 direction)
        {
            line.transform.position = lineOrigin;
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }

       
    }
}