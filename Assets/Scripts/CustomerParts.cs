using UnityEngine;

public class CustomerParts : MonoBehaviour
{
    void Start()
    {
        var bodyIndex = Random.Range(1, 3);
        var hairIndex = Random.Range(1, 3);
        var body = transform.Find($"Body{bodyIndex}");
        var hair = transform.Find($"Hair{hairIndex}");

        for (var i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        body.gameObject.SetActive(true);
        hair.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
