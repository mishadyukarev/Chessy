using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using UnityEngine;


[System.Serializable]
public class PostData
{
    public string input_0 { get; set; }
}

public class PostResponse
{
    public string Input;
    public string Timestamp;
    public string Character_count;
}


public class ChekingServerResponses : MonoBehaviour
{
    async void Start()
    {
        var httpClient = new HttpClient();
        string url = "https://sirpoopy.pythonanywhere.com/put_data";


        var queryParams = new Dictionary<string, string>
        {
            { "param1", "value1" },
            { "param2", "value2" }
        };

        string json = JsonConvert.SerializeObject(queryParams);
        var response = await httpClient.GetAsync($"{url}?input={json}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PostResponse>(result);

            Debug.Log(data.Input);
            Debug.Log(data.Timestamp);
            Debug.Log(data.Character_count);

        }
    }


    void Update()
    {
    }
}
