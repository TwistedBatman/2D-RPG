using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.fixedDeltaTime;
        //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + 180);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.ToString() == "Enemies")
        {
            collision.gameObject.GetComponentInParent<EnemyHealthManager>().HurtEnemy(damage);
            Destroy(gameObject);
        }
    }
}
