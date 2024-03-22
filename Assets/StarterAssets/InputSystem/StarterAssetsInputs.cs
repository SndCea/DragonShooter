using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool shoot;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

        [Header("Zoom")]
        public bool zoom;

		[Header("Minimap")]
		[SerializeField] GameObject Minimap;


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnShoot(InputValue value)
		{
			ShotInput(value.isPressed);
			if (value.isPressed && FindObjectsOfType<Weapon>() != null && FindObjectsOfType<Weapon>().Length < 2)
			{
				FindObjectOfType<Weapon>().Shoot();
			}
		}

		public void OnReloadBullets()
		{
            if (FindObjectsOfType<Weapon>() != null && FindObjectsOfType<Weapon>().Length < 2)
            {
                FindObjectOfType<Weapon>().Reload();
            }

        }

		public void OnMap ()
        {
            Minimap.SetActive(!Minimap.activeSelf);
            
        }
        public void OnTutorial()
        {
			if (GameCanvasManager.GameManagerInstance != null)
            {
                GameCanvasManager.GameManagerInstance.EnableCanvasTutorial();
            }

        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

		public void OnZoom(InputValue value)
		{
			ZoomInput(value.isPressed);
		}
        public void OnCursorStatus(InputValue value)
        {
            SetCursorState();
        }

#endif

        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        public void ShotInput(bool newShootState)
        {
            shoot = newShootState;
        }

        public void ZoomInput(bool newZoomState)
        {
            zoom = newZoomState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState();
		}

		private void SetCursorState()
		{
			if (Cursor.lockState != CursorLockMode.Locked)
			{
                Cursor.lockState =  CursorLockMode.Locked;
            } else
			{
                Cursor.lockState = CursorLockMode.None;
            }
		}
	}
	
}