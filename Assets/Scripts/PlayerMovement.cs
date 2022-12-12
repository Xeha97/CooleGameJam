using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rigidbody;
    private Vector2 movement;
    public PlayerInput input;
    private InputAction interact;
    private Interactable selectedInteractable;

    private void Awake()
    {
        input = new PlayerInput();
        interact = input.Player.Interact;
        input.Player.Interact.performed += Interact;
    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnDisable()
    {
        DisableInput();
    }
  private void OnEnable()
    {
        EnableInput();
    }

    private void OnDestroy()
    {
        input.Player.Interact.performed -= Interact;
    }

    #region Interaction

    private void Interact(InputAction.CallbackContext _)
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact();
        }
    }

    private void TrySelectInteractable(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable == null)
        {
            return;
        }

        if (selectedInteractable != null)
        {
            selectedInteractable.Deselect();
        }

        selectedInteractable = interactable;
        selectedInteractable.Select();
    }

    private void TryDeselectInteractable(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable == null)
        {
            return;
        }

        if (interactable == selectedInteractable)
        {
            selectedInteractable.Deselect();
            selectedInteractable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrySelectInteractable(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        TryDeselectInteractable(other);
    }

    public void DisableInput()
    {
        input.Disable();
    }
    
    public void EnableInput()
    {
        input.Enable();
    }
    
    #endregion
}
