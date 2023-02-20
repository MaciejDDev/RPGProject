using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] GameObject _previewObject;
    [SerializeField] GameObject _placedObject;


    public void Place()
    {
        _previewObject.SetActive(false);
        _placedObject.SetActive(true);
    }
}
