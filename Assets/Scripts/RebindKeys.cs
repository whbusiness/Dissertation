using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class RebindKeys : MonoBehaviour
{
    [SerializeField] InputActionReference RebindPlaceObject;
    [SerializeField] GameObject RebindPlaceText, WaitingPlaceText;
    public InputActionRebindingExtensions.RebindingOperation rebindOp;
    [SerializeField] Button RebindPlace;
    public int movement;
    [SerializeField]
    private Toggle InvertToggle;
    private IsSpeechRecogOn saveValue;

    private void Awake()
    {
        saveValue = FindObjectOfType<IsSpeechRecogOn>();

    }

    public void RebindingPlace()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RebindPlaceObject.action.Disable();
        RebindPlaceText.SetActive(false);
        WaitingPlaceText.SetActive(true);
        rebindOp = RebindPlaceObject.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => CompleteRebindRocket())
            .Start();
            /*operation => CompleteRebindForRocket();
            {
                RebindMovementText.SetActive(true);
                rebindOp.Dispose();
                RebindRocket.action.Enable();
            }
            )
            .Start();   */     
    }
    private void CompleteRebindRocket()
    {
        WaitingPlaceText.SetActive(false);
        RebindPlaceText.SetActive(true);
        rebindOp.Dispose();
        RebindPlaceObject.action.Enable();
        RebindPlace.Select();
    }

    public void OnInvertMovement()
    {
        if (InvertToggle.isOn)
        {
            movement = -1;
        }
        if (!InvertToggle.isOn)
        {
            movement = 1;
        }
        saveValue.isInverted = InvertToggle.isOn;
    }

}
