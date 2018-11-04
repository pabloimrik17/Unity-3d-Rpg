using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    Camera cam;
    PlayerMotor motor;

    public Interactable focus;
    public LayerMask movementMask;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

		if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                Debug.Log("Hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);
                UnsetFocus();
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null) {
                    SetFocus(interactable);
                }

            }
        }
    }

    void SetFocus(Interactable newFocus) {
        if (focus != newFocus) {

            if (focus != null) {
                focus.OnDefocused();
            }
            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void UnsetFocus() {
        if (focus != null) {
            focus.OnDefocused();
        }
        
        focus = null;
        motor.UnfollowTarget();
    }
}
