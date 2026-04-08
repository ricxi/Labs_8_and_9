using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlayButton : MonoBehaviour
{
    [SerializeField] private string NextSceneName;

    public void OnPlayButtonClicked() => SceneManager.LoadScene(NextSceneName);
}
