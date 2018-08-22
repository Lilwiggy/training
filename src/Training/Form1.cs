using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DiscordRPC;

namespace Training
{
    public partial class Form1 : Form
    {
        private static DiscordRpcClient client;
        Dictionary<string, Character> CharDictionary = new Dictionary<string, Character>();
        TreeNode Chars = new TreeNode("Characters");
        TreeNode Level = new TreeNode("Training Level");
        string Character = "Goku";
        string TrainingLevel = "Normal";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new DiscordRpcClient("481565443707109376", true, 0);
            treeView1.Nodes.Add(Chars);
            treeView1.Nodes.Add(Level);
            String[] characterNames = new string[] {
                "Goku kid",
                "Goku",
                "Gohan teen",
                "Gohan adult",
                "Vegeta",
                "Trunks kid",
                "Trunks adult",
                "Goten",
                "Videl",
                "Piccolo",
                "Nappa",
                "Yamcha",
                "Krillin",
                "Android 18",
                "Android 17",
                "Android 16",
                "Tien",
                "Cell",
                "Kid Buu",
                "Majin Buu",
                "Frieza",
                "Raditz",
                "Goku Black",
                "Bardock",
                "Beerus",
                "Broly",
                "Bubbles",
                "Gotenks",
                "Hercule",
                "Launch good",
                "Launch bad",
                "Master Roshi",
                "Pan",
                "Supreme Kai",
                "Zamasu",
                "Zamasu fuzed",
                "Whis"
            };
        
            foreach (string character in characterNames)
            {
                switch (character)
                {
                    case "Goku":
                        CharDictionary.Add(character, new Character(character, new string[] { "Normal", "Super Saiyan", "Super Saiyan 3", "Super Saiyan God", "Super Saiyan Blue" }));
                        break;
                    case "Vegeta":
                        CharDictionary.Add(character, new Character(character, new string[] { "Normal", "Super Saiyan", "Super Saiyan Blue" }));
                        break;
                    case "Goten":
                    case "Trunks adult":
                    case "Trunks kid":
                    case "Gohan teen":
                    case "Gohan adult":
                    case "Bardock":
                    case "Broly":
                        CharDictionary.Add(character, new Character(character, new string[] { "Normal", "Super Saiyan" }));
                        break;
                    case "Goku Black":
                        CharDictionary.Add(character, new Character(character, new string[] { "Normal", "Super Saiyan", "Super Saiyan Rose" }));
                        break;
                    case "Gotenks":
                        CharDictionary.Add(character, new Character(character, new string[] { "Normal", "Super Saiyan", "Super Saiyan 3" }));
                        break;
                    default:
                        CharDictionary.Add(character, new Character(character, new string[] { "Normal" }));
                        break;
                }
                Chars.Nodes.Add(new TreeNode(character));
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode Parent = e.Node.Parent;
            if (Parent == null)
            {
                return;
            }
            switch (Parent.Text)
            {
                case "Characters":
                    TA.Text = $"Training as {e.Node.Text}";
                    Character = e.Node.Text;
                    var Levels = CharDictionary[e.Node.Text].Levels;

                    Level.Nodes.Clear();
                    foreach (string Lev in Levels)
                    {
                        Level.Nodes.Add(new TreeNode(Lev));          
                    }
                    Chars.Collapse();
                    Level.Expand();
                    break;
                case "Training Level":
                    TL.Text = $"Training Level: {e.Node.Text}";
                    TrainingLevel = e.Node.Text;
                     CharImage.Load($"./Assets/Characters/{string.Join("_", $"{Character} {TrainingLevel}".Split(null)).ToLower()}.png");
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            client.Initialize();
            string Image = string.Join("_", $"{Character} {TrainingLevel}".Split(null)).ToLower();

            RichPresence presence = new RichPresence()
            {
                Details = $"Traning as {Character}",
                State = $"Training Level: {TrainingLevel}",
                Assets = new Assets()
                {
                    LargeImageKey = Image,
                    LargeImageText = Character
                }
            };
            presence.Timestamps = new Timestamps()
            {
                Start = DateTime.UtcNow
            };
            client.SetPresence(presence);
        }

    }

}
