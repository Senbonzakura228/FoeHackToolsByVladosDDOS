using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class RequestIdController : MonoBehaviour
{
    [SerializeField] private int inputRequestId;
    [SerializeField] private TMP_InputField _inputField;

    private void Start()
    {
        MainDataStorage.RequestService.RequestIdChangeHandler += OnOutsideChange;
    }

    public void ChangeCurrentRequestId()
    {
        int id;
        int.TryParse(_inputField.text, out id);

        if (id <= 0) return;
        inputRequestId = id;
        MainDataStorage.RequestService.StartupRequestIdSet(inputRequestId);
    }

    private void OnOutsideChange()
    {
        _inputField.text = MainDataStorage.RequestService.currentRequestId.ToString();
    }
}