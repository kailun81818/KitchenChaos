using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Start()
    {
        if (gameInput != null)
        {
            gameInput.OnTestAction += GameInput_OnTestAction;
        }
    }

    private void GameInput_OnTestAction(object sender, System.EventArgs e)
    {
        if (testing)
        {
            if (kitchenObject != null)
            {
                if (secondClearCounter.kitchenObject == null)
                {
                    kitchenObject.SetKitchenObjectParent(secondClearCounter);
                }
            }
        }
    }

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            Debug.Log("Player is interacting with " + kitchenObject.GetKitchenObjectParent());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
