using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public bool isOpening = true;
    float edge = 0.01f;
    float speed = 0.85f;
    void FixedUpdate()
    {
        if (isOpening)
        {
            OpenTransition();
        }
        else
        {
            CloseTransition();
        }
    }
    private void OpenTransition()
    {
        if (gameObject.GetComponent<Image>().fillAmount == 1)
        {
            PlayTransitionSound();
        }
        if (gameObject.GetComponent<Image>().fillAmount != 0)
        {
            if (gameObject.GetComponent<Image>().fillOrigin != 0)
                gameObject.GetComponent<Image>().fillOrigin = 0;
            gameObject.GetComponent<Image>().fillAmount *= speed;
            if (gameObject.GetComponent<Image>().fillAmount < edge)
            {
                gameObject.GetComponent<Image>().fillAmount = 0;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1000f);
            }
        }
    }
    private void CloseTransition()
    {
        if(gameObject.GetComponent<Image>().fillAmount == 0)
        {
            PlayTransitionSound();
        }
        if (gameObject.GetComponent<Image>().fillAmount != 1)
        {
            if (gameObject.GetComponent<Image>().fillOrigin != 1)
                gameObject.GetComponent<Image>().fillOrigin = 1;
            if (gameObject.GetComponent<Image>().fillAmount == 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10f);
                gameObject.GetComponent<Image>().fillAmount = edge;
            }
            gameObject.GetComponent<Image>().fillAmount /= speed;
            if (gameObject.GetComponent<Image>().fillAmount > 1f-edge)
            {
                gameObject.GetComponent<Image>().fillAmount = 1;
                SceneManager.LoadScene(SceneChanger.nextScene);
            }
        }
    }
    private void PlayTransitionSound()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if (!isOpening)
        {
            audioSource.pitch = 0.8f;
        }
        audioSource.Play();
    }
}
