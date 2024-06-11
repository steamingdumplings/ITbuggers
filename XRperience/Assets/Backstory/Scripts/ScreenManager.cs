using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public GameObject BlueCodeImage;
    public GameObject WarningImage;
    public GameObject AIFace;
    public Animator mouthAnimator;
    public float delay = 5.0f;
    public AudioSource typingAudioSource;
    public AudioSource warningAudioSource;
    public AudioSource robotVoiceoverAudioSource;
    public FadeScreen fadeScreen;

    void Start()
    {
        StartCoroutine(ChangeScreenAfterDelay());
    }

    private IEnumerator ChangeScreenAfterDelay()
    {
        typingAudioSource.Play();
        yield return new WaitForSeconds(delay);
        typingAudioSource.Stop();

        // Switch the images
        BlueCodeImage.SetActive(false);
        WarningImage.SetActive(true);

        warningAudioSource.Play();
        yield return new WaitForSeconds(1.0f);

        // Show the robot image
        AIFace.SetActive(true);

        if (mouthAnimator != null)
        {
            mouthAnimator.SetTrigger("StartMouthAnimation");
        }

        //Decrease warning sound
        warningAudioSource.volume = 0.6f; // Set to desired lower volume
        robotVoiceoverAudioSource.Play();
        yield return new WaitForSeconds(robotVoiceoverAudioSource.clip.length);
        warningAudioSource.volume = 1.0f;

        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // Load the next scene
        SceneManager.LoadScene("Environment");
    }
}