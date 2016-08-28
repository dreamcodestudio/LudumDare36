using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHudManager : MonoBehaviour
{
    public Text playerEnergyLabel;
    public Text userMsg;
    public Button restartBtn;

    private void Awake()
    {
        restartBtn.onClick.AddListener(OnRestartBtnClick);
    }

    private void OnDestroy()
    {
        restartBtn.onClick.RemoveListener(OnRestartBtnClick);
    }

    private void OnRestartBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
