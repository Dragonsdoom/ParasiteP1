using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using ParasiteP1.Utility;

namespace ParasiteP1
{
    public static class AudioManager
    {
        static List<SoundEffect> soundList = new List<SoundEffect>();
        static Dictionary<string, SoundEffectInstance> loopedSoundDict = new Dictionary<string, SoundEffectInstance>();

        public static void QueueSound(string name, bool isLoop)
        {
            if (!isLoop)
            {
                soundList.Add(ContentStorageManager.Get<SoundEffect>(name));
            }
            else
            {
                loopedSoundDict.Add(name, ContentStorageManager.Get<SoundEffect>(name).CreateInstance());
                loopedSoundDict[name].IsLooped = true;
                loopedSoundDict[name].Play();
            }
        }

        public static void PlaySounds()
        {
            foreach (SoundEffect sf in soundList)
            {
                sf.Play();
            }
            soundList.Clear();
        }

        public static void StopLoopedSound(string key)
        {
            loopedSoundDict[key].Stop(true);
            loopedSoundDict.Remove(key);
        }
    }
}