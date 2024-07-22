using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Speed: MonoBehaviour
{

    public Button onButton;
    public Button offButton;
    public Button highButton;
    public Button lowButton;

    private string serverURL = "http://192.168.190.17";
    public void Start()
    {
        if (onButton == null || offButton == null || highButton == null || lowButton == null)
        {
            Debug.LogError("One or more buttons are not assigned.");
            return;
        }
        onButton.onClick.AddListener(TurnMotorOn);
        offButton.onClick.AddListener(TurnMotorOff);
        highButton.onClick.AddListener(SetHighSpeed);
        lowButton.onClick.AddListener(SetLowSpeed);
    }

    public void TurnMotorOn()
    {
        StartCoroutine(SendRequest("speed=150"));
    }
    public void TurnMotorOff()
    {
        StartCoroutine(SendRequest("speed=0"));
    }
    public void SetHighSpeed()
    {
        StartCoroutine(SendRequest("speed=255"));
    }
    public void SetLowSpeed()
    {
        StartCoroutine(SendRequest("speed=100"));
    }
    IEnumerator SendRequest(string requestData)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(serverURL + "/speed?" + requestData))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Request failed: " + request.error);
            }
        }
    }
}
