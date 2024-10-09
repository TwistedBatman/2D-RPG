using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public Animator animator;
    public int damageToGive;
    bool targetOnRange = false;
    public float timeToWait = 0.7f;
    //bool attacked = false;
    Collider2D player;

    public void HitPlayer()
    {
        if (targetOnRange)
            player.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            targetOnRange = true;
            player = collision;
            switch (tag)
            {
                case "GoblinSword":
                    animator.SetBool("SwordAttack", true);
                    break;
                case "GoblinAxe":
                    animator.SetBool("AxeAttack", true);
                    break;
                case "GoblinPickaxe":
                    animator.SetBool("PickaxeAttack", true);
                    break;
                default:
                    animator.SetBool("Attack", true);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            targetOnRange = false;
            player = collision;
            switch (tag)
            {
                case "GoblinSword":
                    animator.SetBool("SwordAttack", false);
                    break;
                case "GoblinAxe":
                    animator.SetBool("AxeAttack", false);
                    break;
                case "GoblinPickaxe":
                    animator.SetBool("PickaxeAttack", false);
                    break;
                default:
                    animator.SetBool("Attack", false);

                    break;
            }
        }
    }

    public void StopMoving()
    {
        GetComponentInParent<EnemyAI>().runSpeed = 0;
    }

    public void ContinueMoving()
    {
        GetComponentInParent<EnemyAI>().runSpeed = GetComponentInParent<EnemyAI>().startingRunSpeed;
    }
    /*
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !attacked)
            {
                targetOnRange = true;
                attacked = true;
                animator.SetBool("Attack", true);
                StartCoroutine(WaitToAttack(collision));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log(animator.playbackTime);
            if (collision.gameObject.tag == "Player")
            {
                targetOnRange = false;
                animator.SetBool("Attack", false);
            }
        }

        IEnumerator WaitToAttack(Collider2D collision)
        {
            yield return new WaitForSeconds(timeToWait);
            if (targetOnRange)
            {            
                collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
            }
            attacked = false;
        }*/
}
