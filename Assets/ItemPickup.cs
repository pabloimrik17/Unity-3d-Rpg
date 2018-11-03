using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;

    public override void Interact() {
        base.Interact();

        PickUp();
    }

    private void PickUp() {
        Debug.Log("Picking up item." + item.name);

        if (Inventory.instance.add(item)) {
            Destroy(gameObject);
        }
    }
}