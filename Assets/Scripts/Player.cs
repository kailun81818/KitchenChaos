using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float playerRadius = 0.65f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private Vector3 moveDir;
    private bool isWalking;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        isWalking = inputVector != Vector2.zero;

        if (!isWalking)
        {
            return;
        }

        moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        Vector3 moveVector = GetMoveVector(moveDir, moveDistance);

        transform.position += moveVector * moveDistance;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    private Vector3 GetMoveVector(Vector3 moveDir, float moveDistance)
    {
        if (CanMove(moveDir, moveDistance))
        {
            return moveDir;
        }

        Vector3 moveVectorX = new Vector3(moveDir.x, 0f, 0f);
        if (CanMove(moveVectorX, moveDistance))
        {
            return moveVectorX;
        }

        Vector3 moveVectorZ = new Vector3(0f, 0f, moveDir.z);
        if (CanMove(moveVectorZ, moveDistance))
        {
            return moveVectorZ;
        }

        return Vector3.zero;
    }

    private bool CanMove(Vector3 moveVector, float moveDistance)
    {
        return !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * playerHeight), playerRadius, moveVector.normalized, moveDistance * moveVector.magnitude);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
