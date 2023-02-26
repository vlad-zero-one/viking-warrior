using System.Collections.Generic;
using System;
using System.Linq;

public class EquippedUnit : AttackingUnit
{
    [Serializable]
    public struct BodyItem
    {
        public Bodypart Bodypart;
        public Item Item;

        public BodyItem(Bodypart bodypart, Item item)
        {
            Bodypart = bodypart;
            Item = item;
        }

        public string Represent()
        {
            return "Bodypart: " + Bodypart.ToString() + "\n" + Item.ToString();
        }

    }

    public List<BodyItem> EquippedItems = new List<BodyItem>();


    public BodyItem CreateBodyItem(Bodypart bodypart, Item item)
    {
        if (EquippedItems.Where(bodyItem => bodyItem.Bodypart == bodypart).Count() == 0)
        {
            var created = new BodyItem(bodypart, item);
            EquippedItems.Add(created);
            return created;
        }
        else
        {
            throw new Exception("This bodypart is already equipped!");
        }
    }

    public void DeleteItem(Bodypart bodypart)
    {
        var bodyItems = EquippedItems.Where(bodyItem => bodyItem.Bodypart == bodypart);

        if (bodyItems.Count() != 0)
        {
            EquippedItems.Remove(bodyItems.First());
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

[Serializable]
public class Item
{
    public int Durability = 100;
    public string Name;

    public Item(string name, int durability)
    {
        Name = name;
        Durability = durability;
    }

    public override string ToString()
    {
        return  "Name: " + Name + "\n" + "Durability: " + Durability;
    }
}