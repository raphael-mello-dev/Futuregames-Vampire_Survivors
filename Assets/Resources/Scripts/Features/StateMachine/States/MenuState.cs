using UnityEngine;

public class MenuState : BaseState
{
    public override void OnStart()
    {
        GameManager.Instance.MenuCanvas.SetActive(true);
        
        if (GameObject.FindObjectOfType<ProjectilesManager>().gameObject.activeInHierarchy)
            GameObject.FindObjectOfType<ProjectilesManager>().gameObject.SetActive(false);
    }

    public override void OnEnd()
    {
        GameManager.Instance.MenuCanvas.SetActive(false);
    }
}