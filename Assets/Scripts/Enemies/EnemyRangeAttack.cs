using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    //public float damageToGive;
   // public GameObject target;
   // public Animator animator;
    //public float timeToHit;
    //private float attackSpeed;

    public GameObject arrow;
    public Transform arrowPos;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (timer > 2)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(arrow, arrowPos.position, Quaternion.identity);


/*        if (timeToHit <= 0f)
        {
            timeToHit = attackSpeed;
            animator.Play("Attack");
        }
        else
            timeToHit -= Time.deltaTime;*/
    }
}
