using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Goes_Berzerk.Game
{
    public static class PacmanImagePaths
    {

        //The image paths and resource names
        public static List<Tuple<string, string>> Images = new List<Tuple<string, string>>() {
            new Tuple<string, string> ("pacman_closed", "Assets/Images/PacmanSpriteClosed.png" ),
            new Tuple<string, string> ("pacman_halfopen_up", "Assets/Images/PacmanSpriteHalfOpenUp.png" ),
            new Tuple<string, string> ("pacman_halfopen_down", "Assets/Images/PacmanSpriteHalfOpenDown.png" ),
            new Tuple<string, string> ("pacman_halfopen_left", "Assets/Images/PacmanSpriteHalfOpenLeft.png" ),
            new Tuple<string, string> ("pacman_halfopen_right", "Assets/Images/PacmanSpriteHalfOpenRight.png" ),
            new Tuple<string, string> ("pacman_open_up", "Assets/Images/PacmanSpriteOpenUp.png" ),
            new Tuple<string, string> ("pacman_open_down", "Assets/Images/PacmanSpriteOpenDown.png" ),
            new Tuple<string, string> ("pacman_open_left", "Assets/Images/PacmanSpriteOpenLeft.png" ),
            new Tuple<string, string> ("pacman_open_right", "Assets/Images/PacmanSpriteOpenRight.png" ),
            new Tuple<string, string> ("small_pacman_closed", "Assets/Images/SmallPacmanClosed.png" ),
            new Tuple<string, string> ("small_pacman_halfopen_up", "Assets/Images/SmallPacmanHalfOpenUp.png" ),
            new Tuple<string, string> ("small_pacman_halfopen_down", "Assets/Images/SmallPacmanHalfOpenDown.png" ),
            new Tuple<string, string> ("small_pacman_halfopen_left", "Assets/Images/SmallPacmanHalfOpenLeft.png" ),
            new Tuple<string, string> ("small_pacman_halfopen_right", "Assets/Images/SmallPacmanHalfOpenRight.png" ),
            new Tuple<string, string> ("small_pacman_open_up", "Assets/Images/SmallPacmanOpenUp.png" ),
            new Tuple<string, string> ("small_pacman_open_down", "Assets/Images/SmallPacmanOpenDown.png" ),
            new Tuple<string, string> ("small_pacman_open_left", "Assets/Images/SmallPacmanOpenLeft.png" ),
            new Tuple<string, string> ("small_pacman_open_right", "Assets/Images/SmallPacmanOpenRight.png" ),
            new Tuple<string, string> ("red_ghost_up", "Assets/Images/RedGhostUpSprite.png" ),
            new Tuple<string, string> ("red_ghost_down", "Assets/Images/RedGhostDownSprite.png" ),
            new Tuple<string, string> ("red_ghost_left", "Assets/Images/RedGhostLeftSprite.png" ),
            new Tuple<string, string> ("red_ghost_right", "Assets/Images/RedGhostRightSprite.png" ),
            new Tuple<string, string> ("bullet", "Assets/Images/RedBullet.png" ),
        };
    }
}
