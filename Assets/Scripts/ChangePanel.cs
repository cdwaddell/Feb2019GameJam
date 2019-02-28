using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject desiredPanel;
    public AudioSource audioSource;

    Animator buttonAnimator;
    Animator currentPanelAnimator;
    Animator desiredPanelAnimator;

    int normalHash = Animator.StringToHash("Normal");
    
    void Start()
    {
        buttonAnimator = GetComponent<Animator>();
        currentPanelAnimator = currentPanel.GetComponent<Animator>();
        desiredPanelAnimator = desiredPanel.GetComponent<Animator>();
    }

    public void TogglePanels()
    {
        buttonAnimator.CrossFade(normalHash, 0f);
        buttonAnimator.Update(0f);

        desiredPanel.SetActive(true);
        audioSource.Play();
        currentPanelAnimator.SetTrigger("Close");
        desiredPanelAnimator.SetTrigger("Open");
    }
}
