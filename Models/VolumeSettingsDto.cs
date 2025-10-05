// VolumeSettingsDto.cs

namespace FYP_MusicGame_Backend.Models
{
    public class VolumeSettingsDto
    {
        public float MasterVolume { get; set; } = 1.0f;
        public float EffectVolume { get; set; } = 1.0f;
        public float MusicVolume { get; set; } = 1.0f;
    }
}