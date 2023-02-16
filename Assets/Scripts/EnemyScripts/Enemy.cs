using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    void Move();
    void Attack();
    void TakeDamage(float damage);


}
