namespace Training
{
    class Character
    {
        public Character(string name, string[] levels)
        {
            Name = name;
            Levels = levels;
        }
        public string Name { get; }
        public string[] Levels { get; }
    }
}