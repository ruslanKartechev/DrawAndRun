
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DrawAndRun
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private Button restart;
        [SerializeField] private TextMeshProUGUI finalScore;
        public void Init( )
        {
            restart.onClick.AddListener(() => { GameManager.instance.levelManager.RestartLevel(); });
        }
        public void Show()
        {
            gameOverUI.SetActive(true);
            finalScore.text = "Final Score: " + GameManager.instance.playerData.score;
        }
        public void Hide()
        {
            gameOverUI.SetActive(false);
        }
    }
}
