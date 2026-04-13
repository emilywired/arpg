using System;
using Microsoft.Xna.Framework.Input;

public enum MouseButtons
{
    LeftButton,
    MiddleButton,
    RightButton,
    X1Button,
    X2Button,
}

public class MouseInputManager
{
    public event Action<MouseButtons>? KeyPressed;
    public event Action<MouseButtons>? KeyReleased;
    private MouseState mouseState;
    private MouseState previousMouseState;

    public void Update()
    {
        mouseState = Mouse.GetState();

        foreach (MouseButtons button in Enum.GetValues(typeof(MouseButtons)))
        {
            ProcessButton(button);
        }

        previousMouseState = mouseState;
    }

    private void ProcessButton(MouseButtons button)
    {
        ButtonState buttonState = GetButtonState(button, mouseState);
        ButtonState previousButtonState = GetButtonState(button, previousMouseState);
        bool isNewButtonPress =
            buttonState == ButtonState.Pressed && previousButtonState == ButtonState.Released;
        bool isButtonReleased =
            buttonState == ButtonState.Released && previousButtonState == ButtonState.Pressed;

        if (isNewButtonPress)
        {
            KeyPressed?.Invoke(button);
        }
        else if (isButtonReleased)
        {
            KeyReleased?.Invoke(button);
        }
    }

    private ButtonState GetButtonState(MouseButtons button, MouseState mouseState)
    {
        return button switch
        {
            MouseButtons.LeftButton => mouseState.LeftButton,
            MouseButtons.MiddleButton => mouseState.MiddleButton,
            MouseButtons.RightButton => mouseState.RightButton,
            MouseButtons.X1Button => mouseState.XButton1,
            MouseButtons.X2Button => mouseState.XButton2,
            _ => throw new SystemException("Non-exhaustive handling of ButtonState enum"),
        };
    }
}
