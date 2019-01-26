using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransitioner : MonoBehaviour
{
	[Tooltip("How long the scenes last. Must include the first one.")]
	public float[] SceneDurations;
	[Tooltip("Identifiers of the scenes to be loaded.")]
	public int[] SceneNumbers;
	[Tooltip("How long does the fade lasts.")]
	public float TransitionDuration;
	public RawImage TransitionImage;
	public Ease EaseFunction;

	private int _i = 0;
	private Tweener _tweener;
	private string _methodStr = "LoadNext";

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += Code;
	}

	void Code(Scene scene, LoadSceneMode mode)
	{
		_tweener = DOTween.To(
			() => TransitionImage.color,
			(color) => TransitionImage.color = color,
			Color.clear,
			TransitionDuration);
		_tweener.SetEase(EaseFunction);
		_tweener.OnComplete(() => { Invoke(_methodStr, SceneDurations[_i]); });
	}

	void LoadNext()
	{
		if (_i < SceneNumbers.Length-1)
		{
			_tweener = DOTween.To(
				() => TransitionImage.color,
				(color) => TransitionImage.color = color,
				Color.black,
				TransitionDuration);
			_tweener.OnComplete(() =>
			{
				SceneManager.LoadScene(SceneNumbers[_i++]);
			});
		}

		else
		{
			SceneManager.sceneLoaded -= Code;

			_tweener = DOTween.To(
				() => TransitionImage.color,
				(color) => TransitionImage.color = color,
				Color.black,
				TransitionDuration);
			_tweener.OnComplete(() =>
			{
				SceneManager.LoadScene(SceneNumbers[_i]);
				_tweener = DOTween.To(
					() => TransitionImage.color,
					(color) => TransitionImage.color = color,
					Color.clear,
					TransitionDuration);
				SceneManager.sceneLoaded += MyFadeClear;
			});
		}

	}

	public Tweener FadeBlack()
	{
		_tweener = DOTween.To(
			() => TransitionImage.color,
			(color) => TransitionImage.color = color,
			Color.black,
			TransitionDuration);
		return _tweener;
	}

	void MyFadeClear(Scene a, LoadSceneMode mode)
	{
		FadeClear();
	}

	public Tweener FadeClear()
	{
		_tweener = DOTween.To(
			() => TransitionImage.color,
			(color) => TransitionImage.color = color,
			Color.clear,
			TransitionDuration);
		return _tweener;
	}

}
