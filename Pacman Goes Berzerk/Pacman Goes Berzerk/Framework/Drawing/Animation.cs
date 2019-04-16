using Final_Project_Resources_2;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Framework.Drawing
{
    class Animation : IUpdateable
    {

        //The frames of the animation
        List<CanvasBitmap> frames;

        //The amount of time to allow per frame
        double frameLength;

        //The current elapsed time
        double elapsed;

        //The index of the frame that is currently selected
        int currentFrame;

        //Whether the animation should loop
        bool looping;

        //Whether the animation is playing
        bool playing;

        //Constructor
        public Animation(List<CanvasBitmap> frames, double animationLength, bool looping = true)
        {

            //If the frames list is valid
            if (frames != null && frames.Count > 0)
            {

                //Initialize the frames list
                this.frames = frames;

                //Compute the length of time per frame
                frameLength = animationLength / frames.Count;

                //Initialize elapsed time
                elapsed = 0;

                //Start on the first frame
                currentFrame = 0;

                //Set loop flag
                this.looping = looping;

                //Initialize playing flag
                playing = false;
            }
        }

        //Update the animation by the amount of time
        public void Update(double deltaTime)
        {

            //If playing
            if (playing)
            {

                //Whenever the elapsed time is above the length required to switch to the next frame
                while (elapsed > frameLength)
                {

                    //Move to the next frame
                    NextFrame();

                    //Decrease the elapsed time
                    elapsed -= frameLength;
                }
            }
        }

        //Moves the animation to the next frame
        public void NextFrame()
        {

            //If not at the end of the animation
            if (currentFrame < frames.Count)
            {

                //Increase the current frame index
                currentFrame++;
            }

            else
            {

                //If this is a looping animation
                if (looping)
                {

                    //Go back to the first frame
                    currentFrame = 0;
                }
            }
        }

        //Plays the animation
        public void Play()
        {

            //Start the animation
            playing = true;
        }

        //Pauses the animation
        public void Pause()
        {

            //Pause the animation
            playing = false;
        }

        //Restarts the animation
        public void Restart()
        {
            
            //If a valid frames list exists
            if (frames != null && frames.Count > 0)
            {

                //Go back to the first frame
                currentFrame = 0;

                //Reset elapsed time
                elapsed = 0;
            }
            
        }

        //Stops the animation
        public void Stop()
        {

            //Pause and restart
            Pause();
            Restart();
        }

        //Retrieves the current frame
        public CanvasBitmap GetCurrentFrame()
        {

            //The result
            CanvasBitmap result = null;

            //If the frames list is valid
            if (frames != null && frames.Count > 0)
            {

                //Get the current frame
                result = frames[currentFrame];
            }

            //Return the result
            return result;
        }

    }
}
