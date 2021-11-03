using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public float time;
    private Material material;
    private float timePosition;
    private string lifecycle = "fadeIn"; // fadeIn render fadeOut 
    private float renderFinishTime;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        timePosition = getTimePosition();
    }

    float getTimePosition(){
        return material.GetFloat("Vector1_3AA2640F");
    }
    void setTimePosition(float timePosition){
        material.SetFloat("Vector1_3AA2640F", timePosition);
    }

    // Update is called once per frame
    void Update()
    {
        switch(lifecycle){
            case "fadeIn":
                fadeIn();
                if(getTimePosition() <= 0){
                    lifecycle = "render";
                    renderFinishTime = Time.time + time;
                }
                break;
            case "render":
                if(Time.time >= renderFinishTime){
                    lifecycle = "fadeOut";
                }
                break;
            case "fadeOut":
                fadeOut();
                break;
            default:
                break;
        }
    }
    void fade( float timePosition){
        setTimePosition(timePosition);
    }
    void fadeIn(){
        timePosition -= 0.03f;
        if(getTimePosition() >= 0) {
            fade(timePosition);
        }
    }
    void fadeOut(){
        timePosition += 0.03f;
        if(getTimePosition() <= 1) {
            fade(timePosition);
        }
    }
}
