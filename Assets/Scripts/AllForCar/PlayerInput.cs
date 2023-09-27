using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CarController _carController;

    public void Initialization(CarController carController)
    {
        _carController = carController;
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
}
