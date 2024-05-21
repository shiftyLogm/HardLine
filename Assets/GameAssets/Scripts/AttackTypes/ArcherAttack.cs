using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : RangeAttack
{


    public override void Attack()
    {
        GameObject projectileInstance = Instantiate(projectile, PlayerClassesController.Instance.player.transform.position, Quaternion.identity);
    }
}
