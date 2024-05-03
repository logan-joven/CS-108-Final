using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    GameObject player;
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "HP: " + player.GetComponent<PlayerTopDown>().health.ToString();
    }
}
