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
    public partial class Form1 : Form
    {
        int j = 0, i = 1;
        object s;
        List<Button> lstButton = new List<Button>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            


            //Текстовое поле учатника
            TextBox text = new TextBox
            {
                Width = 200,
                Location = new Point(b.Location.X + 60, b.Location.Y + 50 + j)
            };

            //Участник№
            Label label = new Label
            {
                Location = new Point(text.Location.X - 75, text.Location.Y),
                Text = "Участник №" + i
            };

            //label email
            Label label2 = new Label
            {
                Location = new Point(text.Location.X + 275, text.Location.Y),
                Text = "E-mail" + i
            };

            //Текстовое поле для мыла
            TextBox text2 = new TextBox
            {
                Width = 200,
                Location = new Point(label2.Location.X + 100, label2.Location.Y)
            };

           //Кнопка запуска рандома
            Button randomButt = new Button
            {
                Text = "Santa fucking randoooom",
                AutoSize = true,
                Location = new Point(label.Location.X + 300, label2.Location.Y + 40)
            };
            lstButton.Add(randomButt);

            if (lstButton.Count > 1)
            {
                this.Controls.Remove(lstButton[0]);
                lstButton[0].Dispose();
                lstButton.RemoveAt(0);
            }


            this.Controls.Add(text);
            this.Controls.Add(label);
            this.Controls.Add(label2);
            this.Controls.Add(text2);
            this.Controls.Add(lstButton[0]);
            j += 30;
            i++;
        }
    }
}
