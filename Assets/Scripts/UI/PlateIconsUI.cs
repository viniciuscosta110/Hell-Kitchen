using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
    [SerializeField] private PlateKicthenObject plateKicthenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        plateKicthenObject.OnIngredientAdded += PlateKicthenObject_OnIngredientAdded;
    }

    private void PlateKicthenObject_OnIngredientAdded(object sender, PlateKicthenObject.IngredientAddedEventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach(Transform child in transform) {
            if(child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        plateKicthenObject.GetKitchenObjectSOList().ForEach(kitchenObjectSO => {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        });
    }
}
