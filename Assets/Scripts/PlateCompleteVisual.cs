using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKicthenObject plateKicthenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectList;

    private void Start() {
        plateKicthenObject.OnIngredientAdded += PlateKicthenObject_OnIngredientAdded;
        kitchenObjectSO_GameObjectList.ForEach(kitchenObjectSO_GameObject => {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        });
    }

    private void PlateKicthenObject_OnIngredientAdded(object sender, PlateKicthenObject.IngredientAddedEventArgs e) {
        kitchenObjectSO_GameObjectList.ForEach(kitchenObjectSO_GameObject => {
            if(kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        });
    }
}
