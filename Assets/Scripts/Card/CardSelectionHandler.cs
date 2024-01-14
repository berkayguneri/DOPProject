using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class CardSelectionHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,ISelectHandler,IDeselectHandler/* IPointerClickHandler*/
{
    [SerializeField] private float verticalMoveAmount = 30f;
    [SerializeField] private float moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float scaleAmonut = 1.1f;

    private Vector3 startPos;
    private Vector3 startScale;

    
    private void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
    }

    private IEnumerator MoveCard(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;
        while (elapsedTime<moveTime)
        {
            elapsedTime += Time.deltaTime;

            if (startingAnimation)
            {
                endPosition = startPos + new Vector3(0f, verticalMoveAmount, 0f);
                endScale = startScale * scaleAmonut;
            }
            else
            {
                endPosition = startPos;
                endScale = startScale;
            }

            Vector3 lerpedPos = Vector3.Lerp(transform.position, endPosition, (elapsedTime / moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));

            transform.position = lerpedPos;
            transform.localScale = lerpedScale;

            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(true));

        Debug.Log("Clicked");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(false));
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    StartCoroutine(MoveCard(true));
    //    Debug.Log("Clicked");
    //}

    //private CardData.CardType DetermineCardType()
    //{
    //    Array cardTypes = Enum.GetValues(typeof(CardData.CardType));
    //    CardData.CardType randomCardType = (CardData.CardType)cardTypes.GetValue(UnityEngine.Random.Range(0, cardTypes.Length));

    //    return randomCardType;
    //}


}
