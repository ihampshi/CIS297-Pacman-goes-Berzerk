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

    //A simple enemy object
    public class EnemyGameObject : SpriteGameObject
    {

        
        //The possible enemy states
        public enum ENEMY_STATE
        {
            IDLE = 0,
            MOVING_UP = 1,
            MOVING_RIGHT = 2,
            MOVING_DOWN = 3,
            MOVING_LEFT = 4,
            DYING = 5
        }
        
        //The size of the enemy collider
        private static Vector2 ENEMY_SIZE = new Vector2(25, 25);

        //The length of the animations (in milliseconds)
        private static double ANIMATION_LENGTH = 400.0;

        //The speed that the enemy can move at (in pixels per second)
        private static double MOVEMENT_SPEED = 80;

        //The percent chance of performing a random action
        private static int RANDOM_ACTION_PERCENT = 5;

        //The number of milliseconds that the AI can perform a given action
        private static int MINIMUM_ACTION_TIME = 1000;
        private static int MAXIMUM_ACTION_TIME = 2000;

        //The object to target
        public SpriteGameObject Target { get; set; }

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

        //The enemy state
        ENEMY_STATE state;

        //The random action timer
        double actionTimer;

        //Constructor
        public EnemyGameObject(Vector2 position, GameObjectIndex objectIndex, Random randomNumbers) : base(position, new BoxCollider(position, ENEMY_SIZE), objectIndex)
        {

            //Set the player name
            Name = "enemy";

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

            //Initialize state
            state = ENEMY_STATE.IDLE;

            //Set the action timer
            ResetActionTimer();
        }

        //Each game update
        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            //Check reference
            CheckTargetReference();

            //Decrease action timer
            actionTimer -= deltaTime;

            //If the timer has ended
            if (actionTimer <= 0.0)
            {

                //Pick the next action
                PickAction();
            }

            //Update the animations
            upAnimation.Update(deltaTime);
            downAnimation.Update(deltaTime);
            leftAnimation.Update(deltaTime);
            rightAnimation.Update(deltaTime);

            //Update the image
            Image = currentAnimation.GetCurrentFrame();
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

        //Picks an action, sometimes random, sometimes directed
        private void PickAction()
        {

            //Determine whether to perform a random action or a directed movement
            if (Target != null && randomNumbers.Next(1, 101) > RANDOM_ACTION_PERCENT)
            {

                //Get the direction towards the target
                CardinalDirection targetDirection = GetTargetDirection();

                //Move towards the target
                Move(targetDirection);

                //Restart the action timer
                ResetActionTimer();

            } else
            {

                //Pick a random action
                PickRandomAction();
            }
        }

        //Picks a random action
        private void PickRandomAction()
        {

            //Pick a random action (moving or idling)
            state = (ENEMY_STATE)randomNumbers.Next(0, 5);

            //Perform action operations
            switch (state)
            {

                case ENEMY_STATE.IDLE:

                    Idle();
                    break;
                case ENEMY_STATE.MOVING_UP:

                    Move(CardinalDirection.NORTH);
                    break;
                case ENEMY_STATE.MOVING_RIGHT:

                    Move(CardinalDirection.EAST);
                    break;
                case ENEMY_STATE.MOVING_DOWN:

                    Move(CardinalDirection.SOUTH);
                    break;
                case ENEMY_STATE.MOVING_LEFT:

                    Move(CardinalDirection.WEST);
                    break;
            }

            //Reset the timer
            ResetActionTimer();
        }

        //Switches to the idle state
        private void Idle()
        {

            //Stop moving
            Velocity = Vector2.Zero;

            //Pause animation
            PauseAnimations();
        }

        //Switches to a movement state
        private void Move(CardinalDirection direction)
        {

            //Switch to the specified direction
            Direction = direction;

            //Set velocity in direction
            Velocity = Direction.ToVector2() * MOVEMENT_SPEED;

            //Play the animations
            PlayAnimations();
        }

        //Resets the action timer
        private void ResetActionTimer()
        {

            //Set the action timer to a random value within the seconds range
            actionTimer = randomNumbers.Next(MINIMUM_ACTION_TIME, MAXIMUM_ACTION_TIME + 1);
        }

        //Resets the target reference if the target is destroyed
        private void CheckTargetReference()
        {

            //If target is destroyed
            if (Target != null && !Target.Alive)
            {

                //Remove the target reference
                Target = null;
            }
        }

        //Returns the direction towards the target, if the target exists
        private CardinalDirection GetTargetDirection()
        {

            //The direction towards the target
            CardinalDirection targetDirection = null;

            //If the target is not null
            if (Target != null)
            {

                //Get the difference in position between this object and the target object
                Vector2 difference = Target.Position - Position;

                //If the horizontal axis has a larger distance
                if (Math.Abs(difference.x) > Math.Abs(difference.y))
                {

                    //If the horizontal difference is non-negative
                    if (difference.x >= 0)
                    {

                        //Mark direction as east
                        targetDirection = CardinalDirection.EAST;
                    } else
                    {

                        //Mark direction as west
                        targetDirection = CardinalDirection.WEST;
                    }
                } else
                {

                    //If the vertical difference is non-negative
                    if (difference.y >= 0)
                    {

                        //Mark the direction as south
                        targetDirection = CardinalDirection.SOUTH;
                    } else
                    {

                        //Mark the direction as north
                        targetDirection = CardinalDirection.NORTH;
                    }
                }
            }

            //Return the direction towards the target
            return targetDirection;
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

                //Choose another action
                PickRandomAction();
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
