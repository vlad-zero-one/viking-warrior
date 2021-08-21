using System.Collections.Generic;

public class EquippedUnit : AttackingUnit
{
    [System.Serializable]
    public struct BodyItem
    {
        public string Bodypart;
        public Item Item;

        public BodyItem(string bodypart, Item item)
        {
            Bodypart = bodypart;
            Item = item;
        }

        public string Represent()
        {
            return "Bodypart: " + Bodypart + "\n" + Item.Represent();
        }

    }

    public List<BodyItem> EquippedItems = new List<BodyItem>();


    public BodyItem CreateBodyItem(string bodypart, Item item)
    {
        BodyItem created = new BodyItem();
        bool occupiedBodypart = false;
        foreach (var bodyItem in EquippedItems)
        {
            if (bodyItem.Bodypart == bodypart)
            {
                occupiedBodypart = true;
            }
        }
        if (!occupiedBodypart)
        {
            created.Bodypart = bodypart;
            created.Item = item;
            EquippedItems.Add(created);
        }
        else
        {
            throw new System.Exception("This bodypart is already equipped!");
        }
        return created;
    }

    public void DeleteItem(string bodypart)
    {
        foreach (var bodyItem in EquippedItems)
        {
            if(bodyItem.Bodypart == bodypart)
            {
                EquippedItems.Remove(bodyItem);
                break;
            }
        }
    }

    public BodyItem SwapItem(BodyItem bodyItem)
    {
        BodyItem oldItem = new BodyItem();
        foreach (var alreadyEquipped in EquippedItems)
        {
            if (alreadyEquipped.Bodypart == bodyItem.Bodypart)
            {
                oldItem = alreadyEquipped;
                EquippedItems.Remove(alreadyEquipped);
                break;
            }
        }
        EquippedItems.Add(bodyItem);

        return oldItem;
    }
}

[System.Serializable]
public class Item
{
    public int Durability = 100;
    public string Name;

    public Item(string name, int durability)
    {
        Name = name;
        Durability = durability;
    }

    public string Represent()
    {
        return  "Name: " + Name + "\n" + "Durability: " + Durability;
    }
}