using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D rb;
    private Transform target;

    public GameObject impactEffect;

    private LogicManager logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = LogicManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * projectileSpeed;

        transform.LookAt(target.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Debug.Log(collision);

        if(collision.tag == "PlayerHit")
        {
            logic.decrementLife();
        }
        
        Destroy(gameObject);
    }

    void Update()
    {
        
    }

}
