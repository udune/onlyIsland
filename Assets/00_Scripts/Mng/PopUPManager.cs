// using UnityEngine;
// using UnityEngine.UI;
// using System;
// public class PopUPManager : BasePopUP
// {
//     public static PopUPManager instance = null;
//
//     public override void Awake()
//     {
//         if(instance == null)
//         {
//             instance = this;
//         }
//         transform.SetAsLastSibling();
//         base.Awake();
//     }
//     public Text Description;
//     public Button YesBtn;
//     public Button NoBtn;
//
//     public void Initalize(string temp, Action Yes, Action NO)
//     {
//         gameObject.SetActive(true);
//         Description.text = temp;
//
//         RemoveAllButtons();
//
//         YesBtn.onClick.AddListener(() => Yes());
//         NoBtn.onClick.AddListener(() => NO());
//
//         YesBtn.onClick.AddListener(() => this.gameObject.SetActive(false));
//         NoBtn.onClick.AddListener(() => this.gameObject.SetActive(false));
//     }
//
//     private void RemoveAllButtons()
//     {
//         YesBtn.onClick.RemoveAllListeners();
//         NoBtn.onClick.RemoveAllListeners();
//     }
// }

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PopUPManager : MonoBehaviour
{
    public static PopUPManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }

    public TMP_Text description;
    public Button yesBtn;
    public Button noBtn;

    public void Initialize(string temp, Action yes, Action no)
    {
        gameObject.SetActive(true);
        
        description.text = temp;
        RemoveAllButtons();
        
        yesBtn.onClick.AddListener(() => yes());
        noBtn.onClick.AddListener(() => no());
        
        yesBtn.onClick.AddListener(() => gameObject.SetActive(false));
        noBtn.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void RemoveAllButtons()
    {
        yesBtn.onClick.RemoveAllListeners();
        noBtn.onClick.RemoveAllListeners();
    }
}
