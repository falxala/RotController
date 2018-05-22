using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAudioApi;

namespace RotController
{
    class Volume
    {
        static public void SetVolume(int value)
        {
            // via http://moguriblogg.blogspot.jp/2017/01/c25.html
            //音量を変更
            MMDevice device;
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)value / 100.0f);
        }

        static public void VolumeUP()
        {

            MMDevice device;
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);

            float i = device.AudioEndpointVolume.MasterVolumeLevelScalar + (float)0.01;
            if (i >= 1) i = 1;
            device.AudioEndpointVolume.MasterVolumeLevelScalar = i;
        }

        static public void VolumeDown()
        {
            MMDevice device;
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);

            float i = device.AudioEndpointVolume.MasterVolumeLevelScalar - (float)0.01;
            if (i <= 0) i = 0;
            device.AudioEndpointVolume.MasterVolumeLevelScalar = i;
        }
    }
}
