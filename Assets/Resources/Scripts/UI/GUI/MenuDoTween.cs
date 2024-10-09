using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuDoTween : MonoBehaviour
{
    [Header("Main menu")]
    [SerializeField] private RectTransform gameNameRect;
    [SerializeField] private RectTransform scoreTextRect;
    [SerializeField] private RectTransform buttonsGroupRect;

    [Header("Options menu")]
    [SerializeField] private RectTransform optionsTextRect;
    [SerializeField] private RectTransform optBtnGroupRect;

    void Start()
    {
        MainMenuDoTween();
    }

    public void MainMenuDoTween()
    {
        gameNameRect.DOLocalMoveY(350, 3);
        scoreTextRect.DOLocalMoveY(50, 3);
        buttonsGroupRect.DOLocalMoveY(-340, 3);
    }

    public void MainMenuHide()
    {
        gameNameRect.DOLocalMoveY(700, 0);
        scoreTextRect.DOLocalMoveY(600, 3);
        buttonsGroupRect.DOLocalMoveY(-650, 0);
    }

    public void OptionsDoTween()
    {
        optionsTextRect.DOLocalMoveX(0, 3);
        optBtnGroupRect.DOLocalMoveX(0, 3);
    }
    public void OptionsHide()
    {
        optionsTextRect.DOLocalMoveX(-1800, 0);
        optBtnGroupRect.DOLocalMoveX(-1800, 0);
    }
}
