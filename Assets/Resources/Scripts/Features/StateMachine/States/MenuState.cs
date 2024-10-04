using UnityEngine;

public class MenuState : BaseState
{
    public override void OnStart()
    {
        GameManager.Instance.MenuCanvas.SetActive(true);
    }

    public override void OnEnd()
    {
        GameManager.Instance.MenuCanvas.SetActive(false);
    }
}