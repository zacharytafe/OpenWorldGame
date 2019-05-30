using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Effects
{
    public class Effect : MonoBehaviour
    {
        public float effectRate = 5f;
        public int damage = 2;
        [Tooltip("What visual effect to spawn on target when hit")]
        public GameObject visualEffectPrefab;
        [HideInInspector] public Transform hitObject;

        private float effectTimer = 0f;      

        protected virtual void Start()
        {
            GameObject clone = Instantiate(visualEffectPrefab, hitObject.transform);
            clone.transform.position = transform.position;
            clone.transform.rotation = transform.rotation;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            effectTimer += Time.deltaTime;
            if (effectTimer >= effectRate)
            {
                RunEvent();
            }
        }

        public virtual void RunEvent()
        {
            Destroy(gameObject);
        }
    }
}