using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string type;
    public Sprite image = null;
    public int dropChance;
   // public int currentDropChance;
    public int goldCost;
    public int ironCost;
    public int woodCost;
    public int leatherCost;
    public bool stackable;
    public bool wasPickedUp = false;
    public bool isDropped;
    public bool isEquipment;
    public bool isUsable;
    //public bool isGatherable;
    public string title;


/*    public Item(string name)
    {
        this.name = name;
    }*/
    public virtual void Use()
    {
        // Use item

        Debug.Log("Using " + name);
    }
}
