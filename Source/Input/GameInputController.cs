using System;
using System.Collections.Generic;

// TODO: fix this retarded way of adding new handlers
public class GameInputController
{
    private readonly List<Func<bool>> escapeHandlers = [];
    private readonly List<Func<bool>> leftClickHandlers = [];
    private readonly List<Func<bool>> leftClickReleaseHandlers = [];
    private readonly List<Func<bool>> rightClickHandlers = [];
    private readonly List<Func<bool>> rightClickReleaseHandlers = [];

    public GameInputController()
    {
        Game1.InputManager.OnPress(FixedGameAction.Close, OnClose);
        Game1.InputManager.OnPress(FixedGameAction.LeftClick, OnLeftClick);
        Game1.InputManager.OnRelease(FixedGameAction.LeftClick, OnLeftClickRelease);
        Game1.InputManager.OnPress(FixedGameAction.RightClick, OnRightClick);
        Game1.InputManager.OnRelease(FixedGameAction.RightClick, OnRightClickRelease);
        Game1.InputManager.OnPress(RemappableGameAction.DebugMenu, ToggleDebugMode);
        Game1.InputManager.OnPress(RemappableGameAction.CycleResolution, CycleResolution);
        Game1.InputManager.OnPress(RemappableGameAction.ToggleFullscreen, ToggleFullscreen);
        Game1.InputManager.OnPress(
            RemappableGameAction.ToggleDisplayEnemyHealthBars,
            ToggleDisplayEnemyHealthBars
        );
    }

    public void RegisterOnClose(Func<bool> handler)
    {
        escapeHandlers.Add(handler);
    }

    public void RegisterOnLeftClick(Func<bool> handler)
    {
        leftClickHandlers.Add(handler);
    }

    public void RegisterOnLeftClickRelease(Func<bool> handler)
    {
        leftClickReleaseHandlers.Add(handler);
    }

    public void RegisterOnRightClick(Func<bool> handler)
    {
        rightClickHandlers.Add(handler);
    }

    public void RegisterOnRightClickRelease(Func<bool> handler)
    {
        rightClickReleaseHandlers.Add(handler);
    }

    private void OnClose()
    {
        HandleEventPropagation(escapeHandlers);
    }

    private void OnLeftClick()
    {
        HandleEventPropagation(leftClickHandlers);
    }

    private void OnLeftClickRelease()
    {
        HandleEventPropagation(leftClickReleaseHandlers);
    }

    private void OnRightClick()
    {
        HandleEventPropagation(rightClickHandlers);
    }

    private void OnRightClickRelease()
    {
        HandleEventPropagation(rightClickReleaseHandlers);
    }

    private void HandleEventPropagation(List<Func<bool>> handlers)
    {
        foreach (Func<bool> handler in handlers)
        {
            bool propagationStopped = handler();
            if (propagationStopped)
                break;
        }
    }

    // TODO: move these somewhere else?
    private void ToggleDebugMode()
    {
        GameState.IsDebugMode = !GameState.IsDebugMode;
    }

    private void CycleResolution()
    {
        int scale = (Game1.Config.Scale % 3) + 1;
        Game1.Config.ChangeResolutionScale(scale);
        Game1.Config.ApplyChanges();
    }

    private void ToggleFullscreen()
    {
        Game1.Config.ToggleFullScreen();
        Game1.Config.ApplyChanges();
    }

    private void ToggleDisplayEnemyHealthBars()
    {
        Game1.Config.DisplayEnemyHealthBars = !Game1.Config.DisplayEnemyHealthBars;
    }
}
