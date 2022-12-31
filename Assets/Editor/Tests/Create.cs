using UnityEngine;

namespace Editor.Tests
{
    public static class Create
    {
        public static AudioSource AudioSource() => new GameObject().AddComponent<AudioSource>();

        public static AudioClip AudioClip() => Resources.Load<AudioClip>("Audio/Click");
    }
}