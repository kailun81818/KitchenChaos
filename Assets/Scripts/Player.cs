using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float playerRadius = 0.65f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        isWalking = moveDir != Vector3.zero;

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

        Vector3 moveVectorX = new Vector3(moveDir.x, 0, 0);
        if (CanMove(moveVectorX, moveDistance))
        {
            return moveVectorX;
        }

        Vector3 moveVectorZ = new Vector3(0, 0, moveDir.z);
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
