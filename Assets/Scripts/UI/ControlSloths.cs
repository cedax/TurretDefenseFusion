using UnityEngine;
using TMPro;

public class ControlSloths : MonoBehaviour
{
    public TextMeshProUGUI phaseDisplayText;
    private Touch theTouch;
    private float timeTouchEnend;
    private float displayTime = 0.5f;

    private void Update() {
        if(Input.touchCount > 0){
            theTouch = Input.GetTouch(0);
            if(theTouch.phase == TouchPhase.Ended) {
                phaseDisplayText.text = theTouch.phase.ToString();
                timeTouchEnend = Time.time;
            }else if(Time.time - timeTouchEnend > displayTime) {
                phaseDisplayText.text = theTouch.phase.ToString();
                timeTouchEnend = Time.time;
            }
        }else if(Time.time - timeTouchEnend > displayTime) {
            phaseDisplayText.text = "";
        }
    }
}
