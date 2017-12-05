using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//This allows for checking which click instead of simply clicked.
public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
	public Actor rightClick;
	public Actor leftClick;
	public Actor middleClick;

	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {
			leftClick ();
		} else if (eventData.button == PointerEventData.InputButton.Middle) {
			middleClick ();
		} else if (eventData.button == PointerEventData.InputButton.Right) {
			rightClick ();
		}
	}
}