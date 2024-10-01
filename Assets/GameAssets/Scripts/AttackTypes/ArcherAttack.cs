using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : RangeAttack
{

    private GameObject _player;
    
    private bool _canAttack = true;
    float _cooldown;

    public override void Attack()
    {
        _player = PlayerClassesController.Instance.player;
        if(_canAttack)
        {
            
            HUD.Instance.weaponAttack.PlayOneShot(weaponSFX);
            GameObject projectileInstance = Instantiate(projectile, _player.transform.position, Quaternion.identity);

            Rigidbody2D projectileRb = projectileInstance.GetComponent<Rigidbody2D>();

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Posiçao do mouse
            Vector2 myPos = _player.transform.position; // Posiçao do _player
            Vector2 direction = (mousePos-myPos); // Direçao do projetil
            direction.Normalize();
            projectileRb.velocity = direction * _player.GetComponent<EntityStats>().projectileForce;

            // Rotacionando o projetil
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectileInstance.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);

            _canAttack = false;
            _cooldown = _player.GetComponent<EntityStats>().attackCooldown;
            return;
        }
    }

    void Update()
    {
        CooldownAttack();
    }

    void CooldownAttack()
    {
        if(_cooldown <= 0 && !_canAttack)
        {
            _canAttack = true;
            return;
        }
        _cooldown -= Time.deltaTime;

        
    }
}
