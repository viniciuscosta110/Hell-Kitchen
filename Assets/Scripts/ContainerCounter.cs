using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerInteract;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if(!player.HasKitchenObject()) {
            KitchenObject.CreateKitchenObject(kitchenObjectSO, player);
            
            OnPlayerInteract?.Invoke(this, EventArgs.Empty);
        }
    }
}
