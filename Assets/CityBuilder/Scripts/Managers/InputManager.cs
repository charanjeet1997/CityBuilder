using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionReference mouseInput,keyboardInput;

    public static Action<Vector3Int> onMouseDown;
    public static Action<Vector3Int> onMouseDrag;
    public static Action onMouseUp;

    public Camera mainCamera;
    Vector2 mousePosition;
    [field:SerializeField]public Vector3 CameraMovementVector { get; private set; }

    public LayerMask groundLayer;
    

    private void OnEnable()
    {
        mouseInput.action.started += OnMouseInputActionHandled;
        mouseInput.action.performed += OnMouseInputActionHandled;
        mouseInput.action.canceled += OnMouseInputActionHandled;
        keyboardInput.action.performed += OnKeyboardInputActionHandled;
        keyboardInput.action.canceled += OnKeyboardInputActionHandled;
    }

    private void OnDisable()
    {
        mouseInput.action.started -= OnMouseInputActionHandled;
        mouseInput.action.performed -= OnMouseInputActionHandled;
        mouseInput.action.canceled -= OnMouseInputActionHandled;
        keyboardInput.action.performed -= OnKeyboardInputActionHandled;
        keyboardInput.action.canceled -= OnKeyboardInputActionHandled;
    }

    private Vector3Int? RaycastGround()
    {
        if (mainCamera == null)
            return null;
        Vector3 mousePos = mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3Int rayCastHitPointInt = Vector3Int.RoundToInt(hit.point);
            return rayCastHitPointInt;
        }
        return null;
    }

    private void OnMouseInputActionHandled(InputAction.CallbackContext obj)
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        Debug.Log($"Mouse Input 1");
        mousePosition = obj.ReadValue<Vector2>();
        var mouseHitPos = RaycastGround();
        if(mouseHitPos == null)
            return;
        Debug.Log($"Mouse Input 2");
        if (obj.started)
        {
            Debug.Log("Mouse Input Started");
            onMouseDown?.Invoke(mouseHitPos.Value);
        }

        if (obj.performed)
        {
            Debug.Log("Mouse Input Performed");
            onMouseDrag?.Invoke(mouseHitPos.Value);
        }
        if (obj.canceled)
        {
            mousePosition = Vector2.zero;
            onMouseUp?.Invoke();
        }
    }
    
    private void OnKeyboardInputActionHandled(InputAction.CallbackContext obj)
    {
        Vector2 inputVector = Vector3.zero;
        if(obj.performed)
            inputVector = obj.ReadValue<Vector2>();
        if(obj.canceled) 
            inputVector = Vector2.zero;
        CameraMovementVector = new Vector3(inputVector.x, 0, inputVector.y);
    }
}
