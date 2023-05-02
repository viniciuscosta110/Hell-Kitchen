using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            if(player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            if(!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                if(player.GetKitchenObject().TryGetPlate(out PlateKicthenObject plateKicthenObject)) {
                    if(plateKicthenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    if(GetKitchenObject().TryGetPlate(out plateKicthenObject)) {
                        if(plateKicthenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }
}
