# P1-2DPlatformer-EmilMarchand
Scripting 2 Assignment 1

Emil Marchand - 2530012

Keyboard: A = Left D = Right SPACE = Jump / Gamepad: Left Joystick = Movement, South Button = Jump

Physics approach: 
I chose to go the Rigidbody2D with velocity manipulation route because it is the one I'm most familiar with.
Since this is for an assignment, I want to do well, so it is better not to experiment too much, in my opinion.

Ground Detection:
I tried both the OverlapCircle and Raycast but had trouble with edge detection.
My jump felt weak when I was too close to the edge of a platform.
I used a Boxcast for the game I made for Salim's class but that wasn't one of the options so I opted not to use it.
I then looked into the OnCollisionEnter2D/OnCollisionStay2D with layer filtering method and while looking at the Unity API,
I found IsTouchingLayers. I tried it and it did exactly what I wanted. I know it ends up not being one of the methods listed, but it does have layer filtering... :D ?

Advanced Jump Technique:
I chose Coyote Time because I asked myself which one I'd rather have if given the choice
between it and jump buffering. I thought it would make for a better experience.

* You say in the instructions to write the comments directly on the code but also here, so I did both, just in case.
