using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedArgs> OnStateChanged;
    public class OnStateChangedArgs : EventArgs {
        public State state;
    }

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    };

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;

    private State currentState;

    private float fryingTimer;
    private float burningTimer;

    private void Start() {
        currentState = State.Idle;
    }

    private void Update() {

        if(HasKitchenObject()) {
            switch(currentState) {
                case State.Idle:
                    break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if(fryingTimer > fryingRecipeSO.fryingTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.CreateKitchenObject(fryingRecipeSO.output, this);

                        currentState = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = currentState });

                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        burningTimer = 0f;
                    }
                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if(burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.CreateKitchenObject(burningRecipeSO.output, this);

                        currentState = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = currentState });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }

                    break;

                case State.Burned:
                    break;
            }
        }
    }    

    public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            if(player.HasKitchenObject()) {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    currentState = State.Frying;

                    OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = currentState });
                    fryingTimer = 0f;
                }
            }
        } else {
            if(!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
                currentState = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = currentState });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            } else {
                if(player.GetKitchenObject().TryGetPlate(out PlateKicthenObject plateKicthenObject)) {
                    if(plateKicthenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                        
                        currentState = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedArgs { state = currentState });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        return GetOutputForInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO?.output;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if(fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach(BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if(burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }

        return null;
    }
}
