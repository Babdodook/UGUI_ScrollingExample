using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControlInfinity : MonoBehaviour
{
    private void Start()
    {
        ScrollRectSetting();
    }

    public void ScrollRectSetting()
    {
        // Unrestricted로 설정 // 무한대로 이동가능
        GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;

        // 방향이 세로
        if (GetComponent<InfiniteScroll>().GetDirection == InfiniteScroll.Direction.Vertical)
        {
            GameObject.Destroy(GetComponentInParent<ScrollRect>().horizontalScrollbar.gameObject);
            GetComponentInParent<ScrollRect>().horizontalScrollbar = null;
            GetComponentInParent<ScrollRect>().horizontal = false;

        }
        else // 가로
        {
            GameObject.Destroy(GetComponentInParent<ScrollRect>().verticalScrollbar.gameObject);
            GetComponentInParent<ScrollRect>().verticalScrollbar = null;
            GetComponentInParent<ScrollRect>().vertical = false;
        }
    }
}
