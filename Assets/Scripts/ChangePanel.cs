using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    public GameObject current;
    public GameObject desired;

    Animator anim;
    int normalHash = Animator.StringToHash("Normal");
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TogglePanels()
    {
        anim.CrossFade(normalHash, 0f);
        anim.Update(0f);
        current.SetActive(false);
        desired.SetActive(true);
    }
}
