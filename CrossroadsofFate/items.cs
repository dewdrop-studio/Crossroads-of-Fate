

using System.Collections.Generic;

namespace CrossroadsofFate{
    public class Items{
        static IList<InventoryItem> ItemCache;
        public enum ItemType{
            WEAPON,
            ARMOR,
            CONSUMABLE,
            KEY,
            QUEST,
            MISC
        }



        public class InventoryItem{
            public int id;
            public string name;
            public string description;
            public int quantity = 1;
            public bool stackable = true;
            public ItemType type;
        }

        static public void PrepareItemCache(){
            // Load items from file
        }

        static public InventoryItem GetItem(int id){
            foreach(InventoryItem i in ItemCache){
                if (i.id == id){
                    return i;
                }
            }
            return null;
        }


    }
}