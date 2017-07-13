using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using VRTK;

public class SceneChangerWithFade : MonoBehaviour
{
	public float fadeSpeed = 0.1f;
	public float unfadeSpeed = 0.1f;
	public Color fadeColor = Color.black;
    public bool allowKeyboard = false;

	private VRTK_HeadsetFade fadeReference;

	// This seems to be necessary if you're devving with GI...
	private void Awake()
	{
		DynamicGI.UpdateEnvironment();
	}

	void Start() {
		// Grab our fader.
		fadeReference = this.GetComponent<VRTK_HeadsetFade> ();

		// Since this'll get called on a new scene, let's fade in.
		// Have to do this in a "return 0 coroutine" because Unity.
		StartCoroutine(WaitFrameThenUnfade());

	}
	private IEnumerator WaitFrameThenUnfade() {
		yield return 0;

		// Start at the fade color, then unfade.
		fadeReference.Fade(fadeColor, 0f);
		fadeReference.Unfade(unfadeSpeed);
	}

    private void Update()
    {
        if (allowKeyboard) { 
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                nextSceneIndex++;
			    if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
				    nextSceneIndex = 0;
			    }
            }
            else if (Input.GetKeyUp(KeyCode.Backspace))
            {
                nextSceneIndex--;
			    if (nextSceneIndex < 0) {
				    nextSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
			    }
            }

            if (nextSceneIndex == currentSceneIndex)
            {
                return;
            }

            // If we're here, we're doing this.
            StartFadeLoad(nextSceneIndex);
        }
    }

	public void StartFadeLoadPreviousScene()
	{
		StartFadeLoad(SceneManager.GetActiveScene().buildIndex - 1);
	}
	public void StartFadeLoadNextScene()
	{
		StartFadeLoad(SceneManager.GetActiveScene().buildIndex + 1);
	}

    private void StartFadeLoad(int nextSceneIndex)
    {
        // Inline event callback, baby:
        fadeReference.HeadsetFadeComplete += (object sender, HeadsetFadeEventArgs e) => {
            SceneManager.LoadScene(nextSceneIndex);
        };

        // Start the fade!
        fadeReference.Fade(fadeColor, fadeSpeed);

    }
}