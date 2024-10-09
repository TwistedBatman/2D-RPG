using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float health;
    float currentHealth;
    public int ExpAmount;
    int coinsToGive;
    public PlayerEXP playerExp;
    //public Quest quest;
    public Quest1 quest1;
    public int minCoinsToGive, maxCoinsToGive;
    public Animator animator;
    bool dying = false;
    bool containsParameter;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        playerExp = FindObjectOfType<PlayerEXP>();
        quest1 = FindObjectOfType<Quest1>();
        containsParameter = AnimatorContainsParameter();
        //quest = FindObjectOfType<Quest1>().quest;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to call when an enemy is damaged
    public void HurtEnemy(float damage)
    {
        //Debug.Log(currentHealth);
        currentHealth -= damage;
        //animator.SetTrigger("Hurt");
        if (!dying)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(WaitFor(0.2f));
        }
        if (currentHealth <= 0)
        {
            dying = true;
            if (AnimatorContainsParameter())
                animator.SetTrigger("Death");
            else
                DestroyAndGiveRewards();
            GetComponentInParent<EnemyAI>().runSpeed = 0;
            GetComponentInParent<EnemyAI>().dead = true;
        }
    }

    // Destroy the object when it reached 0 health or lower and give EXP to player, it's called at the last frame of death animation
    public void DestroyAndGiveRewards()
    {
        playerExp.ExpToGive(ExpAmount);
        if (quest1.isActive)  // If there is an active quest
        {
            quest1.IncreaseEnemiesKilled(tag);
        }
        GetComponent<LootToDrop>().InstantiateLoot(transform.position);
        coinsToGive = Random.Range(minCoinsToGive, maxCoinsToGive);
        FindObjectOfType<CoinsAndGatherables>().AddCoins(coinsToGive);
        Destroy(transform.parent.gameObject);
    }

    public bool AnimatorContainsParameter()
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == "Death")
                return true;
        }
        return false;
    }

    IEnumerator WaitFor(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
