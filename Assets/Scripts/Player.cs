using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movespeed = 7f;

    private void Update()
    {
        Vector2 inputVector = new(0f, 0f);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1f;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * movespeed * Time.deltaTime;
    }
}
