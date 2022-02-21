using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreateAccount : MonoBehaviour{

    [SerializeField] Text errorText;

    string username;
    string password; 
    string emailAddress;
    string displayName;

    private void OnEnable()
    {
        UserAccountManager.OnCreateAccountFailed.AddListener(OnCreateAccountFailed);

            }
    private void OnDisable()
    {
        UserAccountManager.OnCreateAccountFailed.RemoveListener(OnCreateAccountFailed);

    }
    void OnCreateAccountFailed(string error)
    {
        errorText.text = error;
    }
    public void UpdateUsername(string _username)
    {
        username = _username;
    }
    public void UpdatePassword(string _password)
    {
        password = _password;
    }
    public void UpdateEmailAddress(string _emailAddress)
    {
        emailAddress = _emailAddress;
    }
    public void UpdateDisplayName(string _displayName)
    {
        displayName=_displayName;
    }
    public void CreateAccount()
    {
        Debug.Log(emailAddress);
        Debug.Log(password);
        Debug.Log(username);
        UserAccountManager.Instance.CreateAccount(username, emailAddress, password,displayName);
    }
}
