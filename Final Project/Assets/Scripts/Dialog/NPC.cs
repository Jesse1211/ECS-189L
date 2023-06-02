using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogPanel;
    public Text dialogText;
    public string[] dialog;
    private int index;

    public float wordSpeed;
    public bool playerIsClose;

    public GameObject button;

    void Update()
    {
        if (playerIsClose)
        {
            if (dialogPanel.activeInHierarchy)
            {
                emptyText();
            }
            else
            {
                dialogPanel.SetActive(true);
                StartCoroutine("Typing");
            }
        }

        if (dialogText.text == dialog[index])
        {
            button.SetActive(true);
        }
    }

    public void NextLine()
    {
        dialogPanel.SetActive(false);

        if (index < dialog.Length - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine("Typing");
        }
        else
        {
            emptyText();
        }
    }

    IEnumerable Typing()
    {
        foreach(char letter in dialog[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            emptyText();
        }
    }

    /// <summary>
    /// Reset the texts in the dialog
    /// </summary>
    public void emptyText()
    {
        dialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
    }
}
