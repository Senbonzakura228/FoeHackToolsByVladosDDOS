using UnityEngine;

public class DialogController : MonoBehaviour
{
    public void OpenDialog(RectTransform page)
    {
        Instantiate(page, transform);
    }
}