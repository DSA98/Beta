using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour {

    [SerializeField]
    private float winningTime = 60f;
    private float timer = 0f;

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= winningTime)
        {
            Time.timeScale = 0;
            print("You have fed the citizens very well, time to rest");
        }
	}
}
