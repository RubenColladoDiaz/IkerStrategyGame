using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellerDialogue : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip npcVoice;

    [Header("Dialogues")]
    public GameObject dialoguePanel;
     public GameObject dialogueOmitir;
    public TMP_Text dialogueText;
    [TextArea(4, 6)] public string[] dialogueLines;

    //Variables privats
    private bool isPlayerinRange;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;
    private AudioSource TalkSound;

    private void Start()
    {
        TalkSound = GetComponent<AudioSource>();
        TalkSound.clip = npcVoice;
    }

    void Update()
    {
        if (isPlayerinRange && Input.GetButtonDown("Fire1"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
                dialogueOmitir.SetActive(true);
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
        //Para omitir texto
        if(Input.GetKeyDown(KeyCode.C))
        {
            Omitir();
            TalkSound.Stop();
            dialogueOmitir.SetActive(false);
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            dialogueOmitir.SetActive(false);
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            TalkSound.Play();
            yield return new WaitForSecondsRealtime(typingTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerinRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerinRange = false;
        }

    }

     private void Omitir()
    {
            if(didDialogueStart)
            {
                //Salta al final del diálogo actual
                lineIndex = dialogueLines.Length -1;
                NextDialogueLine();
            }
    }

}
