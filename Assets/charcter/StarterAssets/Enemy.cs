using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.Quaternion;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] float moveSpeed = 5f;
    Rigidbody rb;
    Transform target;
    Vector2 moveDirection;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    private void Start(){
        health = maxHealth;
        target = GameObject.Find("PlayerArmature 1").transform;
    }

    private void Update(){
        if(target){
            Vector3 direction = (target.position - transform.position).normalized;
          //  float angle = Math.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;;
          //  public Quaternion rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate(){
        if(target){
            rb.velocity =new Vector2(moveDirection.x , moveDirection.y) * moveSpeed;
        }
    }

    public void TakeDamage(float damageAmount){
        Debug.Log($"Damage Amount: {damageAmount}");
        health -= damageAmount;
        Debug.Log($"Health is now: {health}");
        if(health >= 0){
            Destroy (gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }
}
