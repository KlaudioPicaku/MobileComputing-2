using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISignIn : MonoBehaviour
{
    [SerializeField] Text errorText;
    string username;
    string password;
    string emailAddress;
    private void OnEnable()
    {
        UserAccountManager.OnLoginFailed.AddListener(OnSignInFailed);
        UserAccountManager.OnLoginSuccess.AddListener(OnSignInSuccess);

    }
    private void OnDisable()
    {

        UserAccountManager.OnLoginSuccess.RemoveListener(OnSignInSuccess);
        UserAccountManager.OnLoginFailed.RemoveListener(OnSignInFailed);

    }
    void OnSignInFailed(string error)
    {
        errorText.text = error;
    }
    void OnSignInSuccess()
    {
     
    }

    public void UpdateUsername(string _username)
    {
        username = _username;
    }
    public void UpdatePassword(string _password)
    {
        password = _password;
    }
    public void CreateAccount()
    {
        UserAccountManager.Instance.SignIn(username, password);
    }
}
