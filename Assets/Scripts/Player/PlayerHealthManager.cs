using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float defense = 1;
    public Image healthBar;
    public bool isInvulnerable = false;
    bool targetOnRange = false;
    Vector3 respawnPosition;
    public GameObject deathScreen;
    public CoinsAndGatherables coinsAndGatherables;
    [SerializeField] SkillTree skillTree;
    //Attributes attributes;

    // Start is called before the first frame update
    void Start()
    {
        if (skillTree.skill1Activated)
            maxHealth += maxHealth * 0.1f;
        currentHealth = maxHealth;
        SetHealth();
        respawnPosition = new Vector3 (49, 8, -1);
    }

    // Amount of damage to give to player
    public void HurtPlayer(float damage)
    {
        //Debug.Log(damage/defense);
        if (!isInvulnerable)
            currentHealth -= damage/defense;

        if (currentHealth <= 0)
        {
            deathScreen.SetActive (true);
            coinsAndGatherables.SubCoins(10);
            Time.timeScale = 0f;
            transform.position = respawnPosition;
        }
        SetHealth();
    }

    // Change the health bar depending on current health
    public void SetHealth()
    {
        //Debug.Log("max " + maxHealth + "current " + currentHealth);
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    // Heal the player with the given amount
    public void HealPlayer(float amount)
    {
/*        if (skillTree.skill7Activated)
        {
            if ((currentHealth + amount) > maxHealth)
                currentHealth = maxHealth;
            else
                currentHealth += amount * 2;
        }
        else
        {
            if ((currentHealth + amount) > maxHealth)
                currentHealth = maxHealth;
            else
                currentHealth += amount;
        }*/
        if ((currentHealth + amount) > maxHealth)
            currentHealth = maxHealth;
        else if (skillTree.skill7Activated)
            currentHealth += amount * 2;
        else
            currentHealth += amount;


        SetHealth();
    }

    public void SetMaxHealth(float amount)
    {
        maxHealth += amount;
        SetHealth();
    }

    public void FullHealPlayer()
    {
        currentHealth = maxHealth;
        SetHealth();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Fire" && !targetOnRange)
        {
            targetOnRange = true;
            StartCoroutine(WaitFor(1.5f));
        }
    }

    IEnumerator WaitFor(float delay)
    {
        if (targetOnRange)
        {
            HealPlayer(10f);
            yield return new WaitForSeconds(delay);
            targetOnRange=false;
        }
    }
}
