using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text label;

    private void Awake()
    {
        GameManager.LoseAction += LoseGame;
        GameManager.WinAction += WinGame;
    }

    private void WinGame()
    {
        gameObject.SetActive(true);
        label.text = Constants.WinLabel;
    }
    
    private void LoseGame()
    {
        gameObject.SetActive(true);
        label.text = Constants.LoseLabel;
    }
}
