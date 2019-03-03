using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Droppable : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData data)
        {
            if (data.pointerDrag != null)
            {
                Debug.Log("Dropped object was: " + data.pointerDrag);
            }
        }
    }
}
