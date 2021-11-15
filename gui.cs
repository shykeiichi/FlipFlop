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
using Game;

namespace Game.Gui { 
    public static class gui {
        private static string selected_input = "";

        private static V4 on_color = new V4(192, 175, 250, 255);
        private static V4 off_color = new V4(255, 255, 255, 255);
        private static V4 text_color = new V4(0, 0, 0, 255);

        public static void slider(SDL_Rect rect, ref int value, int max, V4 off, V4 on) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_pressed(0))) {
		        value = (int)((mouse.x - rect.x) / ((float)rect.w / max));
            }

            value = Math.Clamp(value, 1, max);

            draw.rect(rect, (byte)off.x, (byte)off.y, (byte)off.z, (byte)off.w, true);
            rect.w = (int)(value * ((float)rect.w / (float)max)); 
            draw.rect(rect, (byte)on.x, (byte)on.y, (byte)on.z, (byte)on.w, true);
        }

        public static void slider(SDL_Rect rect, ref int value, int max) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_pressed(0))) {
		        value = (int)((mouse.x - rect.x) / ((float)rect.w / max));
            }

            value = Math.Clamp(value, 1, max);

            draw.rect(rect, (byte)off_color.x, (byte)off_color.y, (byte)off_color.z, (byte)off_color.w, true);
            rect.w = (int)(value * ((float)rect.w / (float)max)); 
            draw.rect(rect, (byte)on_color.x, (byte)on_color.y, (byte)on_color.z, (byte)on_color.w, true);
        }

        public static void button(SDL_Rect rect, ref bool value, string font, string text, V4 off, V4 on, V4 text_color) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(0))) 
                value = !value;

            if(!value)
                draw.rect(rect, (byte)off.x, (byte)off.y, (byte)off.z, (byte)off.w, true);
            else 
                draw.rect(rect, (byte)on.x, (byte)on.y, (byte)on.z, (byte)on.w, true);

            draw.text(rect.x + 5, rect.y + 5, font, rect.h - 10, text, (byte)text_color.x, (byte)text_color.y, (byte)text_color.z, (byte)text_color.w);
        }

        public static void button(SDL_Rect rect, ref bool value, string font, string text) {
            
            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(0))) 
                value = !value;

            if(!value)
                draw.rect(rect, (byte)off_color.x, (byte)off_color.y, (byte)off_color.z, (byte)off_color.w, true);
            else 
                draw.rect(rect, (byte)on_color.x, (byte)on_color.y, (byte)on_color.z, (byte)on_color.w, true);

            draw.text(rect.x + 5, rect.y + 5, font, rect.h - 10, text, (byte)text_color.x, (byte)text_color.y, (byte)text_color.z, (byte)text_color.w);
        }

        public static void num_input_box(SDL_Rect rect, ref int value, string id, string font, V4 off, V4 on, V4 text_color) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(0))) 
                selected_input = selected_input == id ? "" : id;

            if(selected_input == id) {
                switch(input.get_any_key_just_pressed()) {
                    case input.key_backspace:
                        Int32.TryParse(value.ToString().Length > 0 ? value.ToString().Substring(0, value.ToString().Length - 1) : "0", out value);
                        break;
                    case input.key_0:
                        Int32.TryParse(value.ToString() + "0", out value);
                        break;
                    case input.key_1:
                        Int32.TryParse(value.ToString() + "1", out value);
                        break;
                    case input.key_2:
                        Int32.TryParse(value.ToString() + "2", out value);
                        break;
                    case input.key_3:
                        Int32.TryParse(value.ToString() + "3", out value);
                        break;
                    case input.key_4:
                        Int32.TryParse(value.ToString() + "4", out value);
                        break;
                    case input.key_5:
                        Int32.TryParse(value.ToString() + "5", out value);
                        break;
                    case input.key_6:
                        Int32.TryParse(value.ToString() + "6", out value);
                        break;
                    case input.key_7:
                        Int32.TryParse(value.ToString() + "7", out value);
                        break;
                    case input.key_8:
                        Int32.TryParse(value.ToString() + "8", out value);
                        break;
                    case input.key_9:
                        Int32.TryParse(value.ToString() + "9", out value);
                        break;
                }
            }

            if(selected_input != id)
                draw.rect(rect, (byte)off.x, (byte)off.y, (byte)off.z, (byte)off.w, true);
            else 
                draw.rect(rect, (byte)on.x, (byte)on.y, (byte)on.z, (byte)on.w, true);

            draw.text(rect.x + 5, rect.y + 5, font, rect.h - 10, value.ToString(), (byte)text_color.x, (byte)text_color.y, (byte)text_color.z, (byte)text_color.w);
        }

        public static void num_input_box(SDL_Rect rect, ref int value, string id, string font) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(0))) 
                selected_input = selected_input == id ? "" : id;

            if(selected_input == id) {
                switch(input.get_any_key_just_pressed()) {
                    case input.key_backspace:
                        Int32.TryParse(value.ToString().Length > 0 ? value.ToString().Substring(0, value.ToString().Length - 1) : "0", out value);
                        break;
                    case input.key_0:
                        Int32.TryParse(value.ToString() + "0", out value);
                        break;
                    case input.key_1:
                        Int32.TryParse(value.ToString() + "1", out value);
                        break;
                    case input.key_2:
                        Int32.TryParse(value.ToString() + "2", out value);
                        break;
                    case input.key_3:
                        Int32.TryParse(value.ToString() + "3", out value);
                        break;
                    case input.key_4:
                        Int32.TryParse(value.ToString() + "4", out value);
                        break;
                    case input.key_5:
                        Int32.TryParse(value.ToString() + "5", out value);
                        break;
                    case input.key_6:
                        Int32.TryParse(value.ToString() + "6", out value);
                        break;
                    case input.key_7:
                        Int32.TryParse(value.ToString() + "7", out value);
                        break;
                    case input.key_8:
                        Int32.TryParse(value.ToString() + "8", out value);
                        break;
                    case input.key_9:
                        Int32.TryParse(value.ToString() + "9", out value);
                        break;
                }
            }

            if(selected_input != id)
                draw.rect(rect, (byte)off_color.x, (byte)off_color.y, (byte)off_color.z, (byte)off_color.w, true);
            else 
                draw.rect(rect, (byte)on_color.x, (byte)on_color.y, (byte)on_color.z, (byte)on_color.w, true);

            draw.text(rect.x + 5, rect.y + 5, font, rect.h - 10, value.ToString(), (byte)text_color.x, (byte)text_color.y, (byte)text_color.z, (byte)text_color.w);
        }
    }
}