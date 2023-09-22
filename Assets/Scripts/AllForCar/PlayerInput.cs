using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CarController _carController;

    private void Update()
    {
        //SetMobileMove();
        //SetMoveValues();
    }

    public void OnForwardButtonDown()
    {
        _carController.ForwardValue = 1;
    }

    public void OnBackButtonDown()
    {
        _carController.ForwardValue = -1;
    }

    public void OnVerticalButtonUp()
    {
        _carController.ForwardValue = 0;
    }

    public void OnLeftButtonDown()
    {
        _carController.TurnValue = -1;
    }

    public void OnRightButtonDown()
    {
        _carController.TurnValue = 1;
    }

    public void OnHorizontalButtonUp()
    {
        _carController.TurnValue = 0;
    }

    private void SetMoveValues()
    {
        _carController.ForwardValue = Input.GetAxisRaw(Constants.Vertical);
        _carController.TurnValue = Input.GetAxisRaw(Constants.Horizontal);
    }
}
