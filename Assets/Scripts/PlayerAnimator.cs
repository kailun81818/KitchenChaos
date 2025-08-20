using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on PlayerAnimator.");
        }
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player != null && player.IsWalking());
    }
}
