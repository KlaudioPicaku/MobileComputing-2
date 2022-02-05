using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;
    [SerializeField] AudioClip typingClip;
    [SerializeField] AudioSourceGroup audioSourceGroup;
    [SerializeField] Transform positionCamera;
    [SerializeField] Transform currentCameraPosition;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject dialogBox;
    [SerializeField] float offsetNew = -2.6f;
    [SerializeField] float offsetPrev;
    public float smoothSpeed = 0.125f;

    [SerializeField] Button playDialogue1Button;
    //[SerializeField] Button playDialogue2Button;
    //[SerializeField] Button playDialogue3Button;
    private bool notSet = true;
    [TextArea]
    public string dialogue1;
    [TextArea]
    public string dialogue2;
    [TextArea]
    public string dialogue3;

    private DialogueVertexAnimator dialogueVertexAnimator;
    void Awake() {
        dialogueVertexAnimator = new DialogueVertexAnimator(textBox, audioSourceGroup);
        dialogBox.SetActive(false);
        //playDialogue1Button.onClick.AddListener(delegate { PlayDialogue1(); });
        //playDialogue2Button.onClick.AddListener(delegate { PlayDialogue2(); });
        //playDialogue3Button.onClick.AddListener(delegate { PlayDialogue3(); });
    }
    private void Update()
    {
        if (dialogBox.activeSelf && notSet)
        {
            offsetPrev = camera.GetComponent<CameraFollow>().offset.y;
            camera.GetComponent<CameraFollow>().offset.y = offsetPrev + offsetNew;
            notSet = false;
        }
        else if (!dialogBox.activeSelf) 
        {
            notSet = true;
        }
        //if (camera.GetComponent<CameraFollow>().enabled == false)
        //{
        //    Vector2 smoothPosition = Vector2.Lerp(firstPosition, finalposition, smoothSpeed);
        //    currentCameraPosition.position = smoothPosition;
        //    Debug.Log(smoothPosition);
        //}
    }
    public void closeDialog()
    {
        camera.GetComponent<CameraFollow>().offset.y -= offsetNew;
    }
    private void PlayDialogue1() {
        PlayDialogue(dialogue1);
    }

    private void PlayDialogue2() {
        PlayDialogue(dialogue2);
    }

    private void PlayDialogue3() {
        PlayDialogue(dialogue3);
    }


    private Coroutine typeRoutine = null;
    void PlayDialogue(string message) {
        this.EnsureCoroutineStopped(ref typeRoutine);
        dialogueVertexAnimator.textAnimating = false;
        List<DialogueCommand> commands = DialogueUtility.ProcessInputString(message, out string totalTextMessage);
        typeRoutine = StartCoroutine(dialogueVertexAnimator.AnimateTextIn(commands, totalTextMessage, typingClip, null));
    }
}
