using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
	public void ExitGame()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		var a = FindObjectOfType<SceneTransitioner>();
		a.FadeBlack().OnComplete(() =>
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));

	}
}
