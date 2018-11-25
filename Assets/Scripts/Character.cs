using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField] protected Damageable damageable;
    [SerializeField] protected Collider2D attackTrigger;
    [SerializeField] protected CharacterPhysics2D physics;
    [SerializeField] protected Animator anim;

    public Damageable Damageable
    {
        get
        {
            return damageable;
        }

        set
        {
            damageable = value;
        }
    }

    public Collider2D AttackTrigger
    {
        get
        {
            return attackTrigger;
        }

        set
        {
            attackTrigger = value;
        }
    }

    public CharacterPhysics2D CharacterPhysics
    {
        get
        {
            return physics;
        }

        set
        {
            physics = value;
        }
    }

    public Animator Anim
    {
        get
        {
            return anim;
        }

        set
        {
            anim = value;
        }
    }
}
