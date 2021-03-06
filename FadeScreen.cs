﻿using UnityEngine;
using System.Collections;

public class FadeScreen : MonoBehaviour {

    /************************************************************************************************
     * Code provided by Brackeys
     * Please check out his youtube vide https://www.youtube.com/watch?v=0HwZQt94uHQ on how he did it
     * Or check out his channel in general for basics of Unity at
     * https://www.youtube.com/user/Brackeys/
     * 
     * Note I did not write this
     ************************************************************************************************
     */

    public Texture2D fadeOutTexture;	// the texture that will overlay the screen. This can be a black image or a loading graphic
    public float fadeSpeed = 0.8f;		// the fading speed

    private int drawDepth = -1000;		// the texture's order in the draw hierarchy: a low number means it renders on top
    private float alpha = 1.0f;			// the texture's alpha value between 0 and 1
    private int fadeDir = -1;			// the direction to fade: in = -1 or out = 1

    void OnGUI()
    {
        // fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // force (clamp) the number to be between 0 and 1 because GUI.color uses Alpha values between 0 and 1
        alpha = Mathf.Clamp01(alpha);

        // set color of our GUI (in this case our texture). All color values remain the same & the Alpha is set to the alpha variable
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;																// make the black texture render on top (drawn last)
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);		// draw the texture to fit the entire screen area
    }

    // sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    public void SetFadeSpeed(float _fadeSpeed)
    {
        fadeSpeed = _fadeSpeed;
    }

    // OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a parameter so you can limit the fade in to certain scenes.
    void OnLevelWasLoaded()
    {
        // alpha = 1;		// use this if the alpha is not set to 1 by default
        BeginFade(-1);		// call the fade in function
    }
}
