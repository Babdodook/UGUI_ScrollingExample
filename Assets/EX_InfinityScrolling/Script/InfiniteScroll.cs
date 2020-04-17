using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{

    private Sprite[] sprites;
    public bool isLoop;

    public enum Direction
    {
        Vertical,
        Horizontal,
    }

    // 아이템들
    private LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

    // 스크롤 방향
    [SerializeField]
    private Direction m_Direction;
    public Direction GetDirection
    {
        get { return m_Direction; }
        set { m_Direction = value; }
    }

    // 아이템 갯수
    int ItemCount = 10;
    // 아이템 배치 간격
    float itemPosY;

    // 베이스가 되는 아이템
    [SerializeField]
    private RectTransform itemPrototype = null;
    Vector2 prototypePos;

    float standardValue = 0;

    [SerializeField]
    int itemIndex;
    int LoopMaxCount = 10;
    Vector2 ContentPos;

    private void Awake()
    {
        sprites = Resources.LoadAll<Sprite>("texture");

        itemPosY = itemPrototype.sizeDelta.y;
        prototypePos = itemPrototype.GetComponent<RectTransform>().anchoredPosition;
        itemIndex = ItemCount;
        InstantiateItems();

        isLoop = true;
    }

    private void Update()
    {
        UpdateItemList();
    }

    // 처음 아이템 생성
    void InstantiateItems()
    {
        // 복제할 아이템 안보이게
        itemPrototype.gameObject.SetActive(false);

        // 아이템 생성
        for (int i = 0; i < ItemCount; i++)
        {
            var item = GameObject.Instantiate(itemPrototype) as RectTransform;
            item.SetParent(transform, false);
            item.name = i.ToString();
            item.anchoredPosition = new Vector2(0, prototypePos.y + (i * -itemPosY));
            itemList.AddLast(item);

            // 스코어 셋팅
            item.GetComponent<ItemEliment>().ScoreVal = i;
            // 리소스 이미지 랜덤으로 넣어주기
            item.GetComponent<ItemEliment>().UpdateInfo(sprites[Random.Range(0, sprites.Length)]);
            item.gameObject.SetActive(true);
        }
    }

    void UpdateItemList()
    {

        ContentPos = GetComponentInParent<RectTransform>().anchoredPosition;

        // 위로 올라갈 때
        // 콘텐트창의 y값 - 기준값 이 아이템의 heigt값보다 크면 위로 올라가는 스크롤임
        if (ContentPos.y - standardValue > itemPosY)
        {
            // 위치바꾸기
            // 첫번째꺼 마지막으로
            var item = itemList.First.Value;
            itemList.RemoveFirst();
            itemList.AddLast(item);
            itemList.Last.Value.anchoredPosition = new Vector2(0, prototypePos.y + (itemIndex * -itemPosY));

            itemIndex++;

            standardValue += itemPosY;
            //standardValue = ContentPos.y;


            if (!isLoop)
            {
                itemList.Last.Value.GetComponent<ItemEliment>().ScoreVal =
                    itemList.Last.Previous.Value.GetComponent<ItemEliment>().ScoreVal + 1;
            }
            else
            {
                item.GetComponent<ItemEliment>().ScoreVal = ((itemIndex % LoopMaxCount) + LoopMaxCount) % LoopMaxCount;
            }
        }
        else if (ContentPos.y - standardValue < 0)   // 아래로 내려갈 때
                                                     // 콘텐트창의 y값 - 기준값 이 0보다 작으면 아래로 내려가는 스크롤임
        {
            // 마지막꺼 첫번째로
            var item = itemList.Last.Value;
            itemList.RemoveLast();
            itemList.AddFirst(item);
            itemList.First.Value.anchoredPosition = new Vector2(0, prototypePos.y + (((itemIndex - 1) - ItemCount) * -itemPosY));

            itemIndex--;

            standardValue -= itemPosY;
            //standardValue = Mathf.Abs(ContentPos.y) + itemPosY;


            if (!isLoop)
            {
                itemList.First.Value.GetComponent<ItemEliment>().ScoreVal =
                    itemList.First.Next.Value.GetComponent<ItemEliment>().ScoreVal - 1;
            }
            else
            {
                item.GetComponent<ItemEliment>().ScoreVal = ((itemIndex % LoopMaxCount) + LoopMaxCount) % LoopMaxCount;
            }
        }
    }
}
