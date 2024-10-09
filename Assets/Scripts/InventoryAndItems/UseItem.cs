using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField] PlayerHealthManager healthManager;
    [SerializeField] PlayerManaManager manaManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] WeaponAttack attack;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    bool usedSpeedPotion = false;
    bool equippedAxe = false;
    bool equippedPickaxe = false;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public bool Use(Item item)
    {
        switch (item.name)
        {
            case "Elixir Of Invulnerability":
                healthManager.isInvulnerable = true;
                audioManager.PlaySound("PotionDrink", 0f);
                StartCoroutine(WaitForHealth(15f));
                break;
            case "Potion Of Healing":
                audioManager.PlaySound("PotionDrink", 0f);
                healthManager.HealPlayer(20f);
                break;
            case "Potion Of Mana":
                audioManager.PlaySound("PotionDrink", 0f);
                manaManager.ReplenishMana(20f);
                break;
            case "Potion Of Speed":
                float speed = playerMovement.startingMoveSpeed;
                if (!usedSpeedPotion)
                {
                    audioManager.PlaySound("PotionDrink", 0f);
                    playerMovement.moveSpeed = playerMovement.startingMoveSpeed * 2;
                    usedSpeedPotion = true;
                    StartCoroutine(WaitForSpeed(15f, speed));
                }
                else
                    return false;
                break;
            case "Apple":
                healthManager.HealPlayer(5f);
                FindObjectOfType<Quest2>().DecreaseItemsGathered();

                break;
            case "Sword":
                foreach (Sprite tool in sprites)
                {
                    //if (tool.name == "Sword")
                       // FindObjectOfType
                }
                break;
            case "Axe":
                if (equippedAxe)
                {
                    attack.weapon = WeaponAttack.Weapons.Sword;
                    equippedAxe = false;
                }
                else
                {
                    attack.weapon = WeaponAttack.Weapons.Axe;
                    equippedAxe = true;
                }
                break;            
            case "Pickaxe":
                if (equippedPickaxe)
                {
                    attack.weapon = WeaponAttack.Weapons.Sword;
                    equippedPickaxe = false;
                }
                else
                {
                    attack.weapon = WeaponAttack.Weapons.Pickaxe;
                    equippedPickaxe = true;
                }
                break;
            default:
                break;
        }
        return true;
    }

    IEnumerator WaitForHealth(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        healthManager.isInvulnerable = false;
    }
    
    IEnumerator WaitForSpeed(float timeToWait, float speed)
    {
        yield return new WaitForSeconds(timeToWait);
        usedSpeedPotion = false;
        playerMovement.moveSpeed = speed;
        playerMovement.startingMoveSpeed = speed;
    }
}
