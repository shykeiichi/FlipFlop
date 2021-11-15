using System;
using System.Linq;
using Fjord;
using Fjord.Modules.Debug;
using Fjord.Modules.Game;
using Fjord.Modules.Graphics;
using Fjord.Modules.Input;
using Fjord.Modules.Mathf;
using Fjord.Modules.Sound;
using static SDL2.SDL;

using Game.Scenes;

namespace Game {
    public class main : scene
    {
        public override void on_load()
        {
            game.set_render_resolution(game.renderer, 1920, 1080);

            if(!scene_handler.get_scene("game-template")) {

                // Add all scenes
                scene_handler.add_scene("game-template", new main());
                scene_handler.add_scene("draw", new draw_scene());

                // Load the first scene this can later be called in any file as for example a win condition to switch scene.
                scene_handler.load_scene("draw");
            }
        }

        // Update method
        // This is where all your gamelogic is

        public override void update()
        {

        }
        
        // Render method 
        // This is where all your rendering is

        public override void render()
        {
     
        }
    }

    // Main Class

    class Program 
    {
        public static void Main(string[] args) 
        {
            // Function that starts game
            // The parameter should be your start scene
            game.set_resource_folder("resources");
            game.run(new main());
        }
    }
}