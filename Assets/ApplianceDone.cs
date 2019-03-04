using UnityEngine;

public class ApplianceDone : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var appliance  = animator.gameObject.GetComponent<Appliance>();
        if(appliance != null)
        {
            appliance.Done = true;
        }
    }
}
