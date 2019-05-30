﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Effects
{
    public class Impact : Effect
    {
        public override void RunEvent()
        {
            IHealth health = hitObject.GetComponent<IHealth>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }

            // base.Kill();
        }
    }
}