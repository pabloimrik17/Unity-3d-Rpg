using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    #region Singleton
    public static Inventory instance;

    private void Awake() {
        if (instance != null) {
            Debug.Log("ERROR MORE THAN ONE INSTANCE");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public List<Item> items = new List<Item>();

    public bool add(Item item) {
        if(!item.isDefaultItem) {        
            if (items.Count >= space) {
                Debug.Log("FULL INVENTORY");
                return false;
            }

            items.Add(item);

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }

        return true;
        
    }

    public void remove(Item item) {
        items.Remove(item);

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
