using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Controllers;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShowTextTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    
    [TextArea(3,3)]
    [SerializeField] private string textToShow;

    [SerializeField] private float timeOfTextVisibility;
    
    void OnEnable()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.GetComponent<PlayerController>() != null)
        {
            textField.text = textToShow;
            StartCoroutine(ShowText());
            
        }
    }

    private IEnumerator ShowText()
    {
        textField.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeOfTextVisibility);
        textField.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
}
