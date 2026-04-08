using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
	[SerializeField] private GameObject menuPanel;
	[SerializeField] private bool isMenuOpen;

	private InputAction _openMenu;

    private void Start()
    {
        _openMenu = InputSystem.actions.FindAction("UI/Menu");
		_openMenu.started += ToggleMenu;
    }

	private void OnDisable()
	{
		_openMenu.started -= ToggleMenu;
	}

	private void ToggleMenu(InputAction.CallbackContext context)
	{
		isMenuOpen = !isMenuOpen;
		menuPanel.SetActive(isMenuOpen);

		if (isMenuOpen)
		{
			GetComponent<PlayerInput>().enabled = false;
			InputSystem.actions.FindActionMap("Player").Disable(); 
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			GetComponent<PlayerInput>().enabled = true; 
			InputSystem.actions.FindActionMap("Player").Enable();
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
