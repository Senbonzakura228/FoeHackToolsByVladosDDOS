using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public void ChangePage(RectTransform page)
    {
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(page, transform);
    }
}