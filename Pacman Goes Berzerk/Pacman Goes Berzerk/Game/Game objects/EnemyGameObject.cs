using Final_Project_Resources_2;
using Final_Project_Resources_2.Framework.Systems;
using Microsoft.Graphics.Canvas;
using Pacman_Goes_Berzerk.Framework;
using Pacman_Goes_Berzerk.Framework.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game.Game_objects
{
    class EnemyGameObject : SpriteGameObject
    {

        //The size of the enemy collider
        private static Vector2 ENEMY_SIZE = new Vector2(50, 50);

        //The length of the animations (in milliseconds)
        private static double ANIMATION_LENGTH = 400.0;

        //The AI's random number generator
        Random randomNumbers;

        //The enemy's animations
        Animation upAnimation;
        Animation rightAnimation;
        Animation downAnimation;
        Animation leftAnimation;

        //Animations collected into an array
        Animation[] animations;

        //The current animation
        Animation currentAnimation;

        //The direction of the enemy
        CardinalDirection direction;
        CardinalDirection Direction
        {
            get { return direction; }
            set
            {
                if (direction != value)
                {
                    direction = value;
                    OnDirectionChange();
                }
            }
        }

        //Constructor
        public EnemyGameObject(Vector2 position, GameObjectIndex objectIndex, Random randomNumbers) : base(position, new BoxCollider(position, ENEMY_SIZE), objectIndex)
        {

            //Set the random number generator
            this.randomNumbers = randomNumbers;

            //Initialize animation frames
            List<CanvasBitmap> upAnimationFrames = new List<CanvasBitmap>()
            {
                ImageManager.getImageByName("small_pacman_closed"),
                ImageManager.getImageByName("small_pacman_halfopen_up"),
                ImageManager.getImageByName("small_pacman_open_up"),
                ImageManager.getImageByName("small_pacman_halfopen_up")
            };
            List<CanvasBitmap> rightAnimationFrames = new List<CanvasBitmap>()
            {
                ImageManager.getImageByName("small_pacman_closed"),
                ImageManager.getImageByName("small_pacman_halfopen_right"),
                ImageManager.getImageByName("small_pacman_open_right"),
                ImageManager.getImageByName("small_pacman_halfopen_right")
            };
            List<CanvasBitmap> downAnimationFrames = new List<CanvasBitmap>()
            {
                ImageManager.getImageByName("small_pacman_closed"),
                ImageManager.getImageByName("small_pacman_halfopen_down"),
                ImageManager.getImageByName("small_pacman_open_down"),
                ImageManager.getImageByName("small_pacman_halfopen_down")
            };
            List<CanvasBitmap> leftAnimationFrames = new List<CanvasBitmap>()
            {
                ImageManager.getImageByName("small_pacman_closed"),
                ImageManager.getImageByName("small_pacman_halfopen_left"),
                ImageManager.getImageByName("small_pacman_open_left"),
                ImageManager.getImageByName("small_pacman_halfopen_left")
            };

            //Initialize animations
            upAnimation = new Animation(upAnimationFrames, ANIMATION_LENGTH);
            rightAnimation = new Animation(rightAnimationFrames, ANIMATION_LENGTH);
            downAnimation = new Animation(downAnimationFrames, ANIMATION_LENGTH);
            leftAnimation = new Animation(leftAnimationFrames, ANIMATION_LENGTH);
            PlayAnimations();


            //Setup animations array
            animations = new Animation[4] { upAnimation, rightAnimation, downAnimation, leftAnimation};

            //Initialize to a random direction
            direction = new CardinalDirection(randomNumbers.Next(0,4));

            //Pick the matching animation
            OnDirectionChange();
        }

        //Each game update
        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //Update the animations
            upAnimation.Update(deltaTime);
            downAnimation.Update(deltaTime);
            leftAnimation.Update(deltaTime);
            rightAnimation.Update(deltaTime);

            //Update the image
            Image = currentAnimation.GetCurrentFrame();

            if (randomNumbers.Next(1, 101) > 99)
            {
                Direction = new CardinalDirection(randomNumbers.Next(0, 4));
            }
        }

        //When the direction changes
        private void OnDirectionChange()
        {

            //Change animations to the current direction
            currentAnimation = animations[direction.ToInt()];
        }


        //Plays the animations
        private void PlayAnimations()
        {

            upAnimation.Play();
            rightAnimation.Play();
            downAnimation.Play();
            leftAnimation.Play();
        }

        //Pauses the animations
        private void PauseAnimations()
        {

            upAnimation.Pause();
            rightAnimation.Pause();
            downAnimation.Pause();
            leftAnimation.Pause();
        }

        //On collision
        public override void OnCollision(ICollisionEventHandler otherCollisionHandler, ICollider otherCollider)
        {

            //Attempt to match other object as a wall
            WallGameObject wall = otherCollisionHandler as WallGameObject;

            //Attempt to match other object as a wall
            BulletGameObject bullet = otherCollisionHandler as BulletGameObject;

            //If the other object is a wall
            if (wall != null)
            {

                //Get collider
                BoxCollider wallBoxCollider = otherCollider as BoxCollider;

                //Move out of it's range
                GameObjectHelper.resolveRectangularCollision(this, wallBoxCollider);
            }

            //If the other object is a bullet
            if (bullet != null)
            {

                //If the other object is a player bullet
                if (bullet.Name == "player_bullet")
                {

                    //Destroy the bullet
                    bullet.Destroy();

                    //Destroy this object
                    Destroy();
                }
            }
        }
    }
}
