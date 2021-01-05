using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_InputField))]
[RequireComponent(typeof(Animator))]
public class TextEdit : MonoBehaviour
{
    //Input field
    private GameObject inputFieldObj;
    private TMP_InputField inputField;
    private Animator inputFieldAnim;

    //Text object to change
    private TextMeshProUGUI textToEdit;
    private string prefix;

    private void Start()
    {
        inputFieldObj = gameObject;
        inputField = inputFieldObj.GetComponent<TMP_InputField>();
        inputFieldAnim = inputFieldObj.GetComponent<Animator>();
    }

    //newText naming convention "NameOfTextObject|Prefix(IfApplicable)"
    public void LoadTextEdit(string newText)
    {
        //determine affected text object and prefix
        string[] newTexts = newText.Split('|');
        string textObjName = newTexts[0];

        if (newTexts.Length > 1)
        {
            prefix = newTexts[1];
        }
        else
        {
            prefix = "";
        }

        //show the text editor
        textToEdit = GameObject.Find(textObjName).GetComponent<TextMeshProUGUI>();
        if (textToEdit.text.ToLower().EndsWith("tap here to edit"))
        {
            inputField.text = prefix + "";
        }
        else
        {
            inputField.text = textToEdit.text;
        }
        inputFieldAnim.SetTrigger("Load");

        //automatically select the text area
        EventSystem.current.SetSelectedGameObject(inputFieldObj, null);
        inputField.OnPointerClick(null);
    }

    public void CloseTextEdit()
    {
        if (inputField.text.Length > 0)
        {
            textToEdit.text = inputField.text.ToLower();
        }
        else
        {
            if (prefix.Length > 0)
                textToEdit.text = prefix + "...\n" + "tap here to edit";
            else
                textToEdit.text = "tap here to edit";
        }
        inputFieldAnim.SetTrigger("Close");
    }
}
