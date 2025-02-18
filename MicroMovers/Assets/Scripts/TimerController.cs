using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text winText;
    public TMP_Text zaps;
    public GameObject timerUI;
    public GameObject winUI;
    public GameObject inventory;
    public GameObject inventoryUI;
    public float timer;
    public TruckController truckController;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        timerUI.SetActive(true);
        winUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        int elapsedTimeMinutes = Mathf.FloorToInt(timer / 60F);
        int elapsedTimeSeconds = Mathf.FloorToInt(timer - elapsedTimeMinutes * 60);
        string elapsedTime = string.Format("{0:0}.{1:00}", elapsedTimeMinutes, elapsedTimeSeconds);
        timerText.text = elapsedTime;

        zaps.text = "x" + (player.maxZaps - player.zaps).ToString();
    }

    private void UpdateTimer()
    {
        if(!truckController.win)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timerUI.SetActive(false);
            Destroy(inventory);
            Destroy(inventoryUI);
            winUI.SetActive(true);
            winText.text = "You Win!\n Score\n" + timerText.text;
        }
    }
}
