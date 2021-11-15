using System;
using System.Collections.Generic;
using System.Linq;
using Fjord;
using Fjord.Modules.Debug;
using Fjord.Modules.Game;
using Fjord.Modules.Graphics;
using Fjord.Modules.Input;
using Fjord.Modules.Mathf;
using Fjord.Modules.Sound;
using static SDL2.SDL;

using Game.Gui;

namespace Game.Scenes {
    public class draw_scene : scene
    {
        List<List<V4>> pixel_array = new List<List<V4>>();

        int draw_size = 1;

        bool show_draw_size_meter = false;
        bool erase = false;
        bool color_view = false;

        IntPtr triangle = texture_handler.load_texture("triangle.png");

        int color_r = 0;
        int color_g = 0;
        int color_b = 0;
        int color_a = 255;

        V4 on_color = new V4(192, 175, 250, 255);
        V4 off_color = new V4(250, 247, 255, 255);
        V4 text_color = new V4(0, 0, 0, 255);

        public draw_scene() {
            game.set_render_background(255, 255, 255, 255);
            
            for(var i = 0; i < 320; i++) {
                pixel_array.Add(new List<V4>());
                for(var j = 0; j < 240; j++) {
                    pixel_array[i].Add(new V4(-1, -1, -1, -1));
                }
            }

            game.set_render_background(236, 233, 247, 255);
        }

        public override void on_load()
        {
            
        }

        // Update method
        // This is where all your gamelogic is

        public override void update()
        {
            color_r = Math.Clamp(color_r, 0, 255);
            color_g = Math.Clamp(color_g, 0, 255);
            color_b = Math.Clamp(color_b, 0, 255);
            color_a = Math.Clamp(color_a, 0, 255);

            if(mouse.button_pressed(0)) {
                if(((mouse.x - 322) / 4 > 0 && (mouse.x - 322) / 4 < pixel_array.Count) && ((mouse.y - 62) / 4 > 0 && (mouse.y - 62) / 4 < pixel_array[0].Count)) {
                    if(draw_size == 1)
                        pixel_array[(mouse.x - 322) / 4][(mouse.y - 62) / 4] = erase ? new V4(-1, -1, -1, -1) : new V4(color_r, color_g, color_b, color_a);
                    else {
                        int draw_size_fixed = draw_size - 1;
                        for (int w = 0; w < draw_size_fixed * 2; w++)
                        {
                            for (int h = 0; h < draw_size_fixed * 2; h++)
                            {
                                int dx = draw_size_fixed - w; // horizontal offset
                                int dy = draw_size_fixed - h; // vertical offset
                                if ((dx*dx + dy*dy) <= (draw_size_fixed * draw_size_fixed))
                                {
                                    if(((mouse.x - 322) / 4 + dx > 0 && (mouse.x - 322) / 4 + dx < pixel_array.Count) && ((mouse.y - 62) / 4 + dy > 0 && (mouse.y - 62) / 4 + dy < pixel_array[0].Count))
                                        pixel_array[(mouse.x - 322) / 4 + dx][(mouse.y - 62) / 4 + dy] = erase ? new V4(-1, -1, -1, -1) : new V4(color_r, color_g, color_b, color_a);
                                }
                            }
                        }
                    }
                }
            }

            if(input.get_key_just_pressed(input.key_e))
                erase = !erase;

            if(mouse.wheel_up) {
                draw_size++;
            }
            if(mouse.wheel_down) {
                draw_size--;
                if(draw_size < 1)
                    draw_size = 1;
            }
        }

        // Render method
        // This is where all your rendering is

        public override void render()
        {
            draw.round_rect(new SDL_Rect(346, 107, 1276, 956), 40, 40, 40, 50, 10, true);
            draw.round_rect(new SDL_Rect(326, 87, 1276, 956), 255, 255, 255, 255, 10, true);

            draw.rect(new SDL_Rect(0, 0, 1920, 64), 40, 40, 40, 50, true);
            draw.rect(new SDL_Rect(0, 0, 1920, 54), 255, 255, 255, 255, true);

            for(var i = 0; i < pixel_array.Count; i++) {
                for(var j = 0; j < pixel_array[0].Count; j++) {
                    if(pixel_array[i][j].x == -1) 
                        continue;

                    draw.rect(new SDL_Rect(322 + i * 4, 82 + j * 4, 4, 4), (byte)pixel_array[i][j].x, (byte)pixel_array[i][j].y, (byte)pixel_array[i][j].z, (byte)pixel_array[i][j].w, true, false);
                }
            }     

            gui.button(new SDL_Rect(10, 80, 64, 32), ref show_draw_size_meter, "default", "draw", off_color, on_color, text_color);
            if(show_draw_size_meter) {
                gui.slider(new SDL_Rect(82, 80, 200, 32), ref draw_size, 20, off_color, on_color);
            }

            gui.button(new SDL_Rect(10, 122, 80, 32), ref erase, "default", "erase", off_color, on_color, text_color);

            gui.button(new SDL_Rect(1800, 80, 32, 32), ref color_view, "default", "C", off_color, on_color, text_color);

            if(color_view) {
                draw.round_rect(new SDL_Rect(1632, 157, 300, 300), 40, 40, 40, 50, 10, true);
                draw.round_rect(new SDL_Rect(1612, 137, 300, 300), 255, 255, 255, 255, 10, true);
                draw.texture_ext(triangle, 1790, 100, 0, 0.5, 0.5);

                gui.num_input_box(new SDL_Rect(1622, 147, 96, 32), ref color_r, "color_r", "default", off_color, on_color, text_color);   
                gui.num_input_box(new SDL_Rect(1622, 189, 96, 32), ref color_g, "color_g", "default", off_color, on_color, text_color);   
                gui.num_input_box(new SDL_Rect(1622, 231, 96, 32), ref color_b, "color_b", "default", off_color, on_color, text_color);   
                gui.num_input_box(new SDL_Rect(1622, 273, 96, 32), ref color_a, "color_a", "default", off_color, on_color, text_color);   

                gui.slider(new SDL_Rect(1728, 147, 172, 32), ref color_r, 255, off_color, on_color);   
                gui.slider(new SDL_Rect(1728, 189, 172, 32), ref color_g, 255, off_color, on_color);   
                gui.slider(new SDL_Rect(1728, 231, 172, 32), ref color_b, 255, off_color, on_color);   
                gui.slider(new SDL_Rect(1728, 273, 172, 32), ref color_a, 255, off_color, on_color);   

                draw.rect(new SDL_Rect(1622, 315, 280, 110), (byte)color_r, (byte)color_g, (byte)color_b, (byte)color_a, true);
            }
        }
    }
}