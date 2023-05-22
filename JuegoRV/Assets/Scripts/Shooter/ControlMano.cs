using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Script que hace referencia a las animaciones del modelo de las manos (Visto en clase)
public class ControlMano : MonoBehaviour
{
    [SerializeField] InputActionReference actionGrip;
    [SerializeField] InputActionReference actionTrigger;
    private Animator handAnimator;
    void Start()
    {
        actionTrigger.action.performed += TriggerPress;
        actionGrip.action.performed += GripPress;
        handAnimator = GetComponent<Animator>();
    }
    private void GripPress(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }
    private void TriggerPress(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    }
}
