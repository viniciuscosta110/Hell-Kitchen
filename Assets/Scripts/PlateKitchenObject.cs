using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKicthenObject : KitchenObject {

    public event EventHandler<IngredientAddedEventArgs> OnIngredientAdded;
    public class IngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    } 

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        }

        if(kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new IngredientAddedEventArgs { 
                kitchenObjectSO = kitchenObjectSO 
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }
}
