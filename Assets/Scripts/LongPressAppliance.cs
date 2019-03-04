using UnityEngine;

public class LongPressAppliance : OnPersonEnter
{
    public Order Order { get; set; }
    public bool Done { get; set; }
    public bool Interactible { get; internal set; }
}
