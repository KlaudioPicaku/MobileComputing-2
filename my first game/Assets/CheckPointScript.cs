using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] PlayerScript _player;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool interacted = false;
    [SerializeField] bool referenceSceneHasLoaded = false;
    [SerializeField] bool loadBase = false;
    //[SerializeField] SaveManager autosave;
    private void Awake()
    {
        //_player = FindObjectOfType<PlayerScript>().GetComponent<PlayerScript>();
        //autosave = FindObjectOfType<SaveManager>().GetComponent<SaveManager>();

    }
    private void FixedUpdate()
    {
        if (!referenceSceneHasLoaded)
        {
            Scene scene = SceneManager.GetSceneByName("Persistent");
            if((scene.IsValid() && scene.isLoaded) || FindObjectOfType<PlayerScript>().transform != null)
            {
                referenceSceneHasLoaded = true;
                loadBase = true;
            }
        }
        if (loadBase)
        {
            _player = FindObjectOfType<PlayerScript>().GetComponent<PlayerScript>();
            loadBase = false;
        }
        if (referenceSceneHasLoaded)
        {
            Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, 0.3f, playerLayer);
            if (player.Length > 0)
            {
                _player.toBeSaved.sceneName = this.gameObject.scene.name;
                //autosave.SaveGame();
                Debug.Log(_player.toBeSaved.sceneName);
                interacted = true;
            }
        }
    }
}
