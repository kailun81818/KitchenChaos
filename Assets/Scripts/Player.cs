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

        Vector3 moveVector = moveDir;

        float moveDistance = moveSpeed * Time.deltaTime;

        if (!CanMove(moveVector, moveDistance))
        {
            Vector3 moveVectorX = new Vector3(moveDir.x, 0, 0);
            if (CanMove(moveVectorX, moveDistance))
            {
                moveVector = moveVectorX;
            }
            else
            {
                Vector3 moveVectorZ = new Vector3(0, 0, moveDir.z);
                if (CanMove(moveVectorZ, moveDistance))
                {
                    moveVector = moveVectorZ;
                }
                else
                {
                    moveVector = Vector3.zero;
                }
            }
        }

        transform.position += moveVector * moveDistance;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
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
