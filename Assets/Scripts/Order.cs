using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Order : MonoBehaviour
{
    public const string WashStatus = "WashStatus";

    private const string SortString = "Sort";
    private const string WashString = "Wash";
    private const string DryString = "Dry";
    private const string PressString = "Press";
    private const string FoldString = "Fold";
    private const string OrderIdString = "OrderNumber";

    public bool Sort => (WashStates & WashStates.Sort) == WashStates.Sort;
    public bool Wash => (WashStates & WashStates.Wash) == WashStates.Wash;
    public bool Dry => (WashStates & WashStates.Wash) == WashStates.Wash; //everything washed must be dried
    public bool Press => (WashStates & WashStates.Press) == WashStates.Press;
    public bool Fold => true; //everything gets folded

    private static int CurrentOrderNumber = 0;
    private const int MaxOrderNumber = 676;
    
    private bool CurrentlyHeld;
    private int OrderNumber;
    private string OrderString;
    private WashStates WashStates;

    void Start()
    {
        OrderNumber = GetNextOrder();
        OrderString = GetOrderString();
        //get the order state
        WashStates = (WashStates) UnityEngine.Random.Range(1, 7);

        foreach(var child in transform.Cast<Transform>().Select(x => x.gameObject))
        {
            var alphaComponenent = child.GetComponent<CanvasGroup>();
            switch(child.name)
            {
                case OrderIdString:
                    var text = child.GetComponent<Text>();
                    text.text = OrderString;
                    break;
                case SortString:
                    HideIfNeeded(alphaComponenent, Sort);
                    break;
                case WashString:
                    HideIfNeeded(alphaComponenent, Wash);
                    break;
                case DryString:
                    HideIfNeeded(alphaComponenent, Dry);
                    break;
                case PressString:
                    HideIfNeeded(alphaComponenent, Press);
                    break;
                case FoldString:
                    HideIfNeeded(alphaComponenent, Fold);
                    break;
            }
        }
    }

    private void HideIfNeeded(CanvasGroup child, bool check)
    {
        if (!check)
            child.alpha = 0.25f;
    }

    private static int GetNextOrder()
    {
        if (CurrentOrderNumber == MaxOrderNumber)
            throw new Exception("Too many orders generated");

        return ++CurrentOrderNumber;
    }

    public string GetOrderString()
    {
        var orderCharArray = new char[2];
        orderCharArray[1] = (char)(Math.DivRem(OrderNumber, 26, out var result) + 65);
        orderCharArray[0] = (char)(Math.DivRem(result, 26, out _) + 65);

        return new string(orderCharArray);
    }
}
