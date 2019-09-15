using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SantaRandom
{
    public partial class ExceptionsDialog : Form
    {
        public ExceptionsDialog()
        {
            InitializeComponent();
        }

        int i = 1;
        List<string> pairsList = new List<string>();
        List<TextBox> addedTextBoxes = new List<TextBox>();

        private void Button1_Click(object sender, EventArgs e)
        {
            Label label = new Label
            {
                Text = "Парочка №" + i,
                Dock = DockStyle.Fill,
                Width = 90
            };

            TextBox text = new TextBox
            {
                Width = 200,
                Anchor = AnchorStyles.Left
            };
            

            TextBox text2 = new TextBox
            {
                Width = 200,
                Anchor = AnchorStyles.Left
            };
            addedTextBoxes.AddRange(new List<TextBox>(){text, text2});
            flowLayoutPanel1.Controls.AddRange(new Control[] {label, text,text2 }); //adding to layout control
            

            i++;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < addedTextBoxes.Count;i +=2)
            {
                pairsList.Add(addedTextBoxes[i].Text  + "," + addedTextBoxes[i + 1].Text);
            }
            StaticDataStorage.PairsList = pairsList;
        }
    }
}
