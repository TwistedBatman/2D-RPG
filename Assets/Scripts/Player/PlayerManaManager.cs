using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaManager : MonoBehaviour
{
    public float maxMana = 100;
    float currentMana;
    public Image ManaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        SetMana();
        //LoseMana(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Amount of mana to lose
    public void LoseMana(float amount)
    {
        currentMana -= amount;
        SetMana();
        if (currentMana < maxMana)
            StartCoroutine(ReplenishManaOverTime(1f));
    }

    public void SetMaxMana(float amount)
    {
        maxMana = 100 + amount;
        SetMana();
    }

    // Change the mana bar depending on current mana
    public void SetMana()
    {
        ManaBar.fillAmount = currentMana / 100;
    }

    // If the mana isn't full, replenish it over time
    IEnumerator ReplenishManaOverTime(float sec)
    {
        while (currentMana < maxMana)
        {
            yield return new WaitForSeconds(sec);
            currentMana += 10;
            SetMana();
        }
    }

    // Replenish mana with the given amount
    public void ReplenishMana(float amount)
    {
        // If mana exceeds 100 with the given amount, set it to 100
        if ((currentMana + amount) > 100)
            currentMana = 100;
        else
            currentMana += amount;

        SetMana();
    }
}
