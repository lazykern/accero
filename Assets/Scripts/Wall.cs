using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                Bounce(other.rigidbody, other.contacts[0].normal);
                break;
        }
    }

    static void Bounce(Rigidbody rigidbody, Vector3 normal)
    {
        var direction = Vector3.Reflect(rigidbody.velocity, normal);
        rigidbody.velocity = direction;
    }
}
