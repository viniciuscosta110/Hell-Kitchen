using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public override void Interact(Player player) {
        if(player.HasKitchenObject()) {
            if(player.GetKitchenObject().TryGetPlate(out PlateKicthenObject plateKicthenObject)){
                DeliveryManager.Instance.DeliverRecipe(plateKicthenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
