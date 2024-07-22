using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Temperature : MonoBehaviour
{
    public TextMeshProUGUI Temp;
    public Button ButtonTemp;

    private string serverURL = "http://192.168.190.17/temperature";

  public  void Start()
    {
        Temp = GameObject.Find("InputTemp").GetComponent<TextMeshProUGUI>();
        ButtonTemp.onClick.AddListener(OnButtonClicked);
    }
  public  void OnButtonClicked()
    {
        StartCoroutine(GetTemperature());
    }
    IEnumerator GetTemperature()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(serverURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Temp.text = "Error: " + request.error;
            }
            else
            {
                string jsonString = request.downloadHandler.text;
                TemperatureData temperatureData = JsonUtility.FromJson<TemperatureData>(jsonString);
                Temp.text = " Temperature: " + temperatureData.temperature.ToString() + " °C ";
            }
        }
    }

    [Serializable]
    public class TemperatureData
    {
        public float temperature;
    }
}
