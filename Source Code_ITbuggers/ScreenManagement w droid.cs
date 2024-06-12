using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenManagementwdroid : MonoBehaviour
{
    public GameObject BlueCodeImage;
    public GameObject WarningImage;
    public GameObject AIFace;
    public Animator mouthAnimator;
    public GameObject robotSphere;
    public float delay = 5.0f;
    public AudioSource typingAudioSource;
    public AudioSource warningAudioSource;
    public AudioSource robotVoiceoverAudioSource;
    public float freeLookDuration = 5.0f; // Duration for free look
    public float transitionDuration = 2.0f; // Duration for transition back to initial view
    public float lookUpDuration = 3.0f;
    public float lookUpAngle = 75.0f; // Angle to look up to the sky
    public FadeScreen fadeScreen;

    private Transform playerCamera;
    private Quaternion initialViewRotation;
    private GameObject robotSphereInstance;
    private RobotAttack robotAttack;

    void Start()
    {
        playerCamera = Camera.main.transform;
        initialViewRotation = playerCamera.rotation;
        mouthAnimator = AIFace.transform.Find("AILip").GetComponent<Animator>();
        // Start the coroutine to change the screen
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

        yield return new WaitForSeconds(freeLookDuration);

        // Auto direct player view up
        StartCoroutine(LookUpAndShowRobotAttack());
    }

    private IEnumerator LookUpAndShowRobotAttack()
    {
        // Look up
        float elapsedTime = 0f;
        Quaternion startingRotation = playerCamera.rotation;
        Quaternion lookUpRotation = Quaternion.Euler(initialViewRotation.eulerAngles + Vector3.right * lookUpAngle);

        while (elapsedTime < transitionDuration)
        {
            playerCamera.rotation = Quaternion.Slerp(startingRotation, lookUpRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.rotation = lookUpRotation;

        ActivateRobot();

        yield return new WaitForSeconds(lookUpDuration);

        if (robotAttack != null)
        {
            robotAttack.StartAttack();
        }

        // Fade to the next scene using FadeScreen
        yield return new WaitForSeconds(1.0f); // Wait for a moment before starting the fade
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // Load the next scene
        SceneManager.LoadScene("Environment");
    }

    private void ActivateRobot()
    {
        if (robotSphereInstance == null && robotSphere != null)
        {
            robotSphereInstance = Instantiate(robotSphere, playerCamera.position + playerCamera.forward * 10f, Quaternion.identity);
        }
    }
}