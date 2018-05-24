using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotController
{
    //参考ににすると良い
    //http://www.yoshidastyle.net/2007/10/windowswin32api.html

    class InputKeyClass
    {
        public static byte PickUp_Key(string str)
        {
            switch (str)
            {
                case "BACK":
                    return 0x08;
                case "TAB":
                    return 0x09;
                //
                case "CLEAR":
                    return 0x0C;
                case "RETURN":
                    return 0x0D;
                //     
                case "SHIFT":
                    return 0x10;
                case "CONTROL":
                    return 0x11;
                case "MENU":
                    return 0x12;
                case "PAUSE":
                    return 0x13;
                case "CAPITAL":
                    return 0x14;

                case "ESCAPE":
                    return 0x1B;
                case "CONVERT":
                    return 0x1C;
                case "NONCONVERT":
                    return 0x1D;
                case "ACCEPT":
                    return 0x1E;
                case "MODECHANGE":
                    return 0x1F;
                case "SPACE":
                    return 0x20;
                case "PRIOR":
                    return 0x21;
                case "NEXT":
                    return 0x22;
                case "END":
                    return 0x23;
                case "HOME":
                    return 0x24;
                case "LEFT":
                    return 0x25;
                case "UP":
                    return 0x26;
                case "RIGHT":
                    return 0x27;
                case "DOWN":
                    return 0x28;
                case "SELECT":
                    return 0x29;
                case "PRINT":
                    return 0x2A;
                case "EXECUTE":
                    return 0x2B;
                case "SNAPSHOT":
                    return 0x2C;
                case "INSERT":
                    return 0x2D;
                case "DELETE":
                    return 0x2E;
                case "HELP":
                    return 0x2F;

                //NUM
                case "KEY_0":
                    return 0x30;
                case "KEY_1":
                    return 0x31;
                case "KEY_2":
                    return 0x32;
                case "KEY_3":
                    return 0x33;
                case "KEY_4":
                    return 0x34;
                case "KEY_5":
                    return 0x35;
                case "KEY_6":
                    return 0x36;
                case "KEY_7":
                    return 0x37;
                case "KEY_8":
                    return 0x38;
                case "KEY_9":
                    return 0x39;
                //CHAR
                case "KEY_A":
                    return 0x41;
                case "KEY_B":
                    return 0x42;
                case "KEY_C":
                    return 0x43;
                case "KEY_D":
                    return 0x44;
                case "KEY_E":
                    return 0x45;
                case "KEY_F":
                    return 0x46;
                case "KEY_G":
                    return 0x47;
                case "KEY_H":
                    return 0x48;
                case "KEY_I":
                    return 0x49;
                case "KEY_J":
                    return 0x4A;
                case "KEY_K":
                    return 0x4B;
                case "KEY_L":
                    return 0x4C;
                case "KEY_M":
                    return 0x4D;
                case "KEY_N":
                    return 0x4E;
                case "KEY_O":
                    return 0x4F;
                case "KEY_P":
                    return 0x50;
                case "KEY_Q":
                    return 0x51;
                case "KEY_R":
                    return 0x52;
                case "KEY_S":
                    return 0x53;
                case "KEY_T":
                    return 0x54;
                case "KEY_U":
                    return 0x55;
                case "KEY_V":
                    return 0x56;
                case "KEY_W":
                    return 0x57;
                case "KEY_X":
                    return 0x58;
                case "KEY_Y":
                    return 0x59;
                case "KEY_Z":
                    return 0x5A;
                //NUMKEY
                case "NUMPAD0":
                    return 0x60;
                case "NUMPAD1":
                    return 0x61;
                case "NUMPAD2":
                    return 0x62;
                case "NUMPAD3":
                    return 0x63;
                case "NUMPAD4":
                    return 0x64;
                case "NUMPAD5":
                    return 0x65;
                case "NUMPAD6":
                    return 0x66;
                case "NUMPAD7":
                    return 0x67;
                case "NUMPAD8":
                    return 0x68;
                case "NUMPAD9":
                    return 0x69;
                case "MULTIPLY":
                    return 0x6A;
                case "ADD":
                    return 0x6B;
                case "SEPARATOR":
                    return 0x6C;
                case "SUBTRACT":
                    return 0x6D;
                case "DECIMAL":
                    return 0x6E;
                case "DIVIDE":
                    return 0x6F;

                case "F1":
                    return 0x70;
                case "F2":
                    return 0x71;
                case "F3":
                    return 0x72;
                case "F4":
                    return 0x73;
                case "F5":
                    return 0x74;
                case "F6":
                    return 0x75;
                case "F7":
                    return 0x76;
                case "F8":
                    return 0x77;
                case "F9":
                    return 0x78;
                case "F10":
                    return 0x79;
                case "F11":
                    return 0x7A;
                case "F12":
                    return 0x7B;

                //OEM
                case "OEM_1":
                    return 0xBA;
                case "OEM_PLUS":
                    return 0xBB;
                case "OEM_COMMA":
                    return 0xBC;
                case "OEM_MINUS":
                    return 0xBD;
                case "OEM_PERIOD":
                    return 0xBE;
                case "OEM_2":
                    return 0xBF;
                case "OEM_3":
                    return 0xC0;
                case "OEM_4":
                    return 0xDB;
                case "OEM_5":
                    return 0xDC;
                case "OEM_6":
                    return 0xDD;
                case "OEM_7":
                    return 0xDE;
                case "OEM_8":
                    return 0xDF;
                case "OEM_102":
                    return 0xE2;


                default:
                    return 0x00;

            }
        }
    }

}
