using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        print("collisionDetected");
    }
}
