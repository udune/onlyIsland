using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// public class ToastPopUPManager : BasePopUP
// {
//     public static ToastPopUPManager instance;
//     public Text PopUpText;
//     Animator animator;
//     public override void Awake()
//     {
//         if (instance == null) instance = this;
//         transform.SetAsLastSibling();
//
//         base.Awake();
//         animator = GetComponent<Animator>();
//     }
//
//     public void Initalize(string temp)
//     {
//         this.gameObject.SetActive(true);
//         PopUpText.text = temp;
//         animator.Play("Toast_Open");
//     }
//     public void Deactive() => gameObject.SetActive(false);
// }

public class ToastPopUPManager : BasePopUP
{
    public static ToastPopUPManager instance;
    public TMP_Text popupText;
    private Animator animator;

    public override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void Initialize(string temp)
    {
        gameObject.SetActive(true);
        popupText.text = temp;
        animator.Play("Toast_Open");
    }
    
    public void Deactive() => gameObject.SetActive(false);
}