using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    
    public static event EventHandler OnAnyDropedObject;

    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    
    public virtual void Interact(Player player){
        Debug.Log("BaseCounter Interact();");
    }

    public virtual void InteractAlternate(Player player){
        //Debug.Log("BaseCounter Interact();");
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null) OnAnyDropedObject?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
