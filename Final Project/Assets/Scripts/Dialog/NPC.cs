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
        public GameObject button;

        private int index = 0;
        private bool first_time_in = true;
        private float wordSpeed = 0.06f;
        private bool playerIsClose;
        private float lastTimeAccess;

        private void Start()
        {
            if (this.gameObject.name == "Boss1")
            {
                dialog = new string[] {
                    "Who is this little girl? You can't go further no more!!!"
                };
            }

            else if (this.gameObject.name == "Boss2")
            {
                dialog = new string[] {
                    "You can't go behind this point. To think of how much effort you put... towards the inevitable goal... of digging your OWN GRAVE!"
                };
            }

            else if (this.gameObject.name == "FinalEnding")
            {
                dialog = new string[] {
                    "Oh! It's our dear daughter K.K. Are you here to save me?", 
                    "How sweet. I'm too tired right now, would you please take me back to our home, I need to take a rest and restore some energy."
                };
            }

            else if (this.gameObject.name == "Elk")
            {
                dialog = new string[] {
                    "Thank you K.K. our daughter. Would you please take it back to our home, I need to rest a little bit more.",
                    "Now you need to go back and find Fox Patron Saint from the eastern island and bring her back.",
                    "Good luck, may you remain safe throughout. Don't forget to grab 5 orbs."
                };
            }

            else if (this.gameObject.name == "Deer")
            {
                dialog = new string[] {
                    "Our noble daughter, the embodiment of Earth, the guardian of all patron Saints. " +
                    "We beseech your aid, for the Earth languishes in the grip of great disaster caused by human folly",
                    "Pollution ravages our land, afflicting the very patrons we hold dear. Venture forth beyond the forest's edge, " +
                    "armed with your righteous weapon, and cleanse the malevolent creatures born from this foul pollution.",
                    "Utilize your enchanted bag to gather the purity orbs, potent orbs that possess the power to heal our wounded God " +
                    "and preserve the sacred order of the Earth's patron Saints. They rely on you, our great daughter, to be their savior in this time of peril.",
                    "Waste no time, depart now and fulfill this crucial mission."
                };
            }

            else if (this.gameObject.name == "Crow")
            {
                dialog = new string[] {
                    "You need to collect 5 orbs to start the fight\n" +
                    "A: left, D: right, Space: jump, J&K: attack."
                    
                };
            }

            lastTimeAccess = Time.time;
        }

        void Update()
        {
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

                if ((collision.gameObject.GetComponent<PlayerControllerAnimator>().score < 5) && Time.time - lastTimeAccess >= 5 && this.gameObject.name == "Deer")
                {
                    first_time_in = true;
                }
                else
                {
                    if (this.gameObject.name == "Deer")
                    {
                        dialog = new string[] {
                            "Now, you are fully prepared for your journey. Go find the Crow it will teleport you to the place that need your help.",
                            "After you find the other Patron Saints, bring them back please.",
                            "May luck be your constant companion, and may you remain safe throughout, our courageous daughter. Take care."
                        };
                        first_time_in = true;
                    }
                }

                lastTimeAccess = Time.time;

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


/*
 * Layers:
 *      Player: Little girl, Deer, PickUp, Crow [Main Scene]
 *      
 * 
 * 
 */