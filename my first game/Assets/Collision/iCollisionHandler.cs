using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCollision 
{
    void CollisionEnter(string colliderName, GameObject other);
}
