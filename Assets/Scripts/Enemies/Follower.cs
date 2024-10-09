using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public EnemyAI objectToFollow;
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, objectToFollow.transform.position, objectToFollow.walkSpeed * Time.deltaTime);
    }
}
