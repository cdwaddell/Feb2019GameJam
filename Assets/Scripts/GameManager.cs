using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    private const int FirstCustomerTimingInSeconds = 5;
    private Dictionary<string, GameObject> CustomerList = new Dictionary<string, GameObject>(); 

    public GameObject LaundryQueue;
    public Transform LaundryPrefab;

    public GameObject CustomersQueue;
    public Transform CustomerPrefab;

    public Transform OrderPrefab;

    public GameObject Appliances;
    public Canvas Canvas;
    public AudioMixer AudioMixer;
    public GameObject Player;

    public GameObject CustomerPurgatory;

    private GameObject AddCustomer(out Customer customer)
    {
        //find the first location in the queue without a customer
        GameObject selectedSlot = null;
        foreach (Transform slot in CustomersQueue.transform)
        {
            var enumerator = slot.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                selectedSlot = slot.gameObject;
                break;
            }
        }
        if(selectedSlot == null)
        {
            customer = null;
            return null;
        }

        var customerPrefab = Instantiate(CustomerPrefab, selectedSlot.transform, false);
        customer = customerPrefab.GetComponent<Customer>();

        return customer.gameObject;
    }

    private GameObject AddLaundry(out Laundry laundry)
    {
        GameObject selectedLaundry = null;
        foreach (Transform slot in LaundryQueue.transform)
        {
            var enumerator = slot.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                selectedLaundry = slot.gameObject;
                break;
            }
        }

        if (selectedLaundry == null)
        {
            laundry = null;
            return null;
        }

        var laundryPrefab = Instantiate(LaundryPrefab, selectedLaundry.transform, false);
        laundry = laundryPrefab.gameObject.GetComponent<Laundry>();
        laundry.PlayerTransitioned += PlayerTransitioned;

        return laundryPrefab.gameObject;
    }

    private GameObject MoveCustomer(string OrderId)
    {
        var customer = FindCustomer(OrderId, out _);
        if (customer == null) return null;

        customer.transform.SetParent(CustomerPurgatory.transform, false);
        return customer;
    }

    private GameObject FindCustomer(string OrderId, out Customer customer)
    {
        if (!CustomerList.ContainsKey(OrderId))
        {
            customer = null;
            return null;
        }
        var go = CustomerList[OrderId];
        customer = go.GetComponent<Customer>();

        return go;
    }
    
    private List<Order> Orders = new List<Order>();
    private GameObject CurrentlyActiveObject = null;
    
    public void Start()
    {
        Invoke("CustomerEnters", FirstCustomerTimingInSeconds);

        foreach (var appliance in Appliances.GetComponentsInChildren<OnPersonEnter>())
        {
            appliance.PlayerTransitioned += PlayerTransitioned;
        }
    }

    public void Update()
    {
        var activeObject = CurrentlyActiveObject;
        if(Input.GetKey(KeyCode.Space) && activeObject != null)
        {
            switch(activeObject.name)
            {
                case "FoldingTable":
                    ProcessFoldingTable(activeObject);
                    break;
                case "SortingTable":
                    ProcessSortingTable(activeObject);
                    break;
                case "WasherOrange":
                case "WasherYellow":
                    ProcessWasher(activeObject);
                    break;
                case "DryerOrange":
                case "DryerYellow":
                    ProcessDryer(activeObject);
                    break;
                case "Press":
                    ProcessPress(activeObject);
                    break;
                case "CashRegister":
                    ProcessCashRegister(activeObject);
                    break;
            }

            foreach (Transform order in LaundryQueue.transform)
            {
                if (order.childCount == 0) continue;

                if (order.GetChild(0).gameObject == activeObject)
                {
                    var laundry = order.GetChild(0).GetComponent<Laundry>();
                    if (laundry != null)
                    {
                        ProcessClickedOnLaundry(laundry.Order);
                        laundry.gameObject.transform.SetParent(CustomerPurgatory.transform, false);
                    }
                }
            }
        }
        //check if customers are angry

        //check if waiting customer should come back in

        //shuffle queue

        //send customers away
    }

    private void PlayerTransitioned(object sender, PlayerTransitionedEventArgs e)
    {
        var fromGameObject = (GameObject)sender;
        if (e.Type == PlayerTransitionType.Enter && CurrentlyActiveObject != fromGameObject)
            CurrentlyActiveObject = fromGameObject;
        else if (e.Type == PlayerTransitionType.Exit && CurrentlyActiveObject == fromGameObject)
            CurrentlyActiveObject = null;
    }

    private void CustomerEnters()
    {
        var customer = AddCustomer(out _);

        if(customer == null)
        {
            AngryCustomer();
        }

        Invoke("CustomerEnters", CalculateNextCustomerTime());
    }

    #region UnattendedAppliances
    private void ProcessDryer(GameObject activeObject)
    {
        //TODO: fill this in
        ProcessUnattendedAppliance(activeObject);
    }

    private void ProcessWasher(GameObject activeObject)
    {
        //TODO: fill this in
        ProcessUnattendedAppliance(activeObject);
    }
    #endregion

    #region AttendedAppliances
    private void ProcessSortingTable(GameObject activeObject)
    {
        //TODO: fill this in
        ProcessAttendedAppliance(activeObject);
    }

    private void ProcessFoldingTable(GameObject activeObject)
    {
        //TODO: fill this in
        ProcessAttendedAppliance(activeObject);
    }

    private void ProcessPress(GameObject activeObject)
    {
        //TODO: fill this in
        ProcessAttendedAppliance(activeObject);
    }
    #endregion


    private float CalculateNextCustomerTime()
    {
        //TODO: fill this in
        return 5;
    }

    private void CustomerLeaves(Customer customer)
    {
        //TODO: fill this in
        AngryCustomer();
    }

    private void AngryCustomer()
    {
        //TODO: fill this in
    }

    private void ProcessClickedOnLaundry(Order order)
    {
        var playerScript = Player.GetComponent<PlayerMobility>();
        playerScript.HoldLaundry(order);
    }

    private void ProcessCashRegister(GameObject activeObject)
    {
        //get first customer
        var queueLocation = CustomersQueue.transform.GetChild(0);
        if (queueLocation.transform.childCount == 0)
            return;
        var customerObject = queueLocation.transform.GetChild(0);
        if (customerObject == null)
            return;

        var customer = customerObject.GetComponent<Customer>();

        //make sure there is a customer that needs an order
        if (customer == null || customer.Order != null)
            return;
        var panel = Canvas.transform.GetChild(0);

        AddLaundry(out var laundry);
        if (laundry == null)
            return;

        var orderPrefab = Instantiate(OrderPrefab, panel, false);
        var order = orderPrefab.GetComponent<Order>();

        laundry.Order = order;
        customer.Order = order;

        CustomerList.Add(order.GetOrderString(), customerObject.gameObject);
        Orders.Add(order);
        MoveCustomer(order.GetOrderString());

        var customerGo = customer.gameObject;
        customerGo.transform.parent = null;

        customerGo.SetActive(false);
        //TODO: fill this in
    }

    private void ProcessAttendedAppliance(GameObject activeObject)
    {
        //TODO: fill this in
    }

    private void ProcessUnattendedAppliance(GameObject activeObject)
    {
        //TODO: fill this in
    }
}
