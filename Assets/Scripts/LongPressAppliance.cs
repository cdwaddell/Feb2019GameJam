using UnityEngine;

public class LongPressAppliance : Appliance
{
    public bool Interactible { get; internal set; }
    public override void PauseAnimation()
    {
        Interactible = false;
        base.PauseAnimation();
    }
}

public class Appliance: OnPersonEnter
{
    public Order Order { get; set; }
    public bool Done { get; set; }
    public bool InteractionInProgress { get; set; }
    public void StartAnimation()
    {
        var loadingBar = transform.Find("loadingBar");
        loadingBar.gameObject.SetActive(true);
        var animator = loadingBar.GetComponent<Animator>();
        animator.SetTrigger("UnPause");
    }

    public virtual void PauseAnimation()
    {
        var loadingBar = transform.Find("loadingBar");
        var animator = loadingBar.GetComponent<Animator>();
        animator.SetTrigger("Pause");
    }
}
