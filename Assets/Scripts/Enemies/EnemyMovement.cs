using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public bool foundTarget = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        if (foundTarget)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    // Update is called once per frame

}
