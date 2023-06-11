using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{

    public class NPC : MonoBehaviour
    {
        public GameObject dialogPanel;
        public Text dialogText;
        private string[] dialog;
        //public string[] dialog;
        public GameObject button;

        private int index = 0;
        private bool first_time_in = true;
        private float wordSpeed = 0.06f;
        private bool playerIsClose;

        private void Start()
        {
            Debug.Log(this.gameObject.name);
            if (this.gameObject.name == "Deer")
            {
                dialog = new string[] {
                    "Our noble daughter, the embodiment of Earth, the guardian of all patron Saints. We beseech your aid, for the Earth languishes in the grip of great disaster caused by human folly",
                    "Pollution ravages our land, afflicting the very patrons we hold dear. Venture forth beyond the forest's edge, armed with your righteous weapon, and cleanse the malevolent creatures born from this foul pollution.",
                    "Utilize your enchanted bag to gather the purity orbs, potent orbs that possess the power to heal our wounded God and preserve the sacred order of the Earth's patron Saints. They rely on you, our great daughter, to be their savior in this time of peril.",
                    "Waste no time, depart now and fulfill this crucial mission."
                };
            }
        }

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
            foreach (char letter in dialog[index].ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        IEnumerator Freeze(GameObject gameObject)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            yield return new WaitForSeconds(1);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) {
                playerIsClose = true;

                if (collision.gameObject.GetComponent<PlayerControllerAnimator>().score < 5)
                {
                    first_time_in = true;
                }

                StartCoroutine(Freeze(collision.gameObject));
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
}