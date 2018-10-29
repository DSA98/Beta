using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsUI : MonoBehaviour {

    [SerializeField]
    private Image uiImage = null;

    private void Update()
    {
        uiImage.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
