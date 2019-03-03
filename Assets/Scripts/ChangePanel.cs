using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject desiredPanel;
    public AudioSource audioSource;

    Animator buttonAnimator;
    Animator currentPanelAnimator;
    Animator desiredPanelAnimator;
    CanvasGroup desiredCanvasGroup;
    CanvasGroup currentCanvasGroup;
    
    void Start()
    {
        buttonAnimator = GetComponent<Animator>();
        currentPanelAnimator = currentPanel.GetComponent<Animator>();
        desiredPanelAnimator = desiredPanel.GetComponent<Animator>();
        desiredCanvasGroup = desiredPanel.GetComponent<CanvasGroup>();
        currentCanvasGroup = currentPanel.GetComponent<CanvasGroup>();
    }

    public void TogglePanels()
    {
        buttonAnimator.Play("Normal");
        
        desiredCanvasGroup.interactable = true;

        audioSource.Play();
        currentPanelAnimator.SetTrigger("Close");
        desiredPanelAnimator.SetTrigger("Open");
    }
}
