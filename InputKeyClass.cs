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
            return VkEnum<RamGecTools.KeyboardHook.VKeys>(str);
        }


        //enumをジェネリックで使いたい
        /// <summary>
        /// 仮想キーコードを返す
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte VkEnum<TEnum>(string str) where TEnum : struct
        {
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                var byteValue = Convert.ToByte(value);
                var strValue = value.ToString();
                
                if(str == strValue)
                {
                    return byteValue;
                }
            }
            return 0x00;
        }
    }

}
