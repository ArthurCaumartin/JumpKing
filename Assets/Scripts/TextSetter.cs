using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))] 
public class TextSetter : MonoBehaviour
{
    [SerializeField] string _prefixe;
    [SerializeField] string _sufixe;
    [SerializeField] FloatReference _floatValue;
    TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        _text.text = _prefixe + (_prefixe.Length == 0 ? "" : " ") + _floatValue.Value.ToString() + " " +_sufixe;
    }
}
