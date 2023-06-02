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
    private bool first_time_in = true;

    public float wordSpeed;
    public bool playerIsClose;

    public GameObject button;

    void Update()
    {
        // have to add some other conditions.
        // can revise this if needed.
        if (first_time_in && playerIsClose)
        {
            first_time_in = false;

            if (dialogPanel.activeInHierarchy)
            {
                emptyText();
            }
            else
            {
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogText.text == dialog[index])
        {
            UnityEngine.Debug.Log(1122);
            button.SetActive(true);
        }
    }

    public void NextLine()
    {
        button.SetActive(false);

        if (index < dialog.Length - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            emptyText();
        }
    }

    IEnumerator Typing()
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
