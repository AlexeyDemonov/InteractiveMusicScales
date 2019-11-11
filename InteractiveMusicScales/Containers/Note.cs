namespace InteractiveMusicScales
{
    public class Note
    {
        public Sound Sound { get; }

        public bool IsChecked { get; set; } = false;

        public bool IsKeynote { get; set; } = false;

        public Note(Sound sound)
        {
            this.Sound = sound;
        }

        public override int GetHashCode()
        {
            return (int)Sound;
        }

        public override bool Equals(object obj)
        {
            if (obj is Note)
            {
                return this.Sound == ((Note)obj).Sound;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return Sound.ToString();
        }
    }
}