using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace SantaRandom
{
    public partial class Form1 : Form
    {
        int j = 0, i = 1;
        object s;
        List<Button> lstButton = new List<Button>();
        List<TextBox> lstEmailsTextBoxes = new List<TextBox>();
        List<String> lstEmails = new List<String>();                //sorry :(
        List<TextBox> lstPlayers = new List<TextBox>();
        List<String> lstPlayersList = new List<string>();
        List<int> lstRassilka = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //нажатие на кнопку добавления туловища в движуху
        private void Button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            

            //Текстовое поле учатника
            TextBox text = new TextBox
            {
                Width = 200,
                Location = new Point(b.Location.X + 60, b.Location.Y + 50 + j)
            };
            lstPlayers.Add(text);
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
            lstEmailsTextBoxes.Add(text2);

            //Кнопка запуска рандома
            Button randomButt = new Button
            {
                Text = "Santa fucking randoooom",
                AutoSize = true,
                Location = new Point(label.Location.X + 300, label2.Location.Y + 40)
            };
            randomButt.Click += new EventHandler(RandomButtClick);
            lstButton.Add(randomButt);

            //перерисовка кнопки рандома
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

        //нажатие на кнопку рандома
        private void RandomButtClick(object sender, EventArgs e)
        {
            //записываем имена участников в список
            for (int i = 0; i < lstPlayers.Count; i++)
            {
                lstPlayersList.Add(lstPlayers[i].Text);
            }

            //записываем все ящики в список
            for (int i = 0; i < lstEmailsTextBoxes.Count; i++)
            {
                lstEmails.Add(lstEmailsTextBoxes[i].Text);
            }

            lstRassilka = Random();

            //отправляем письма всем участникам
            for (int i = 0; i < lstEmails.Count; i++)
            {
                SendMail("smtp.gmail.com", "email", "password", lstEmails[i], "Santa", "You New Year target is:" + lstPlayersList[lstRassilka[i]]);
            }
        }

        //отправка ёбаной почты
        public static void SendMail(string smtpServer, string from, string password,string mailto, string caption, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;

                SmtpClient client = new SmtpClient
                {
                    Host = smtpServer,
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(from.Split('@')[0], password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                client.Send(mail);
                mail.Dispose();
                MessageBox.Show("Почта отправлена успешно!");
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        //неебический алгоритм рандома
        private List<int> Random ()
        {
            List<String> names = new List<String>();
            List<int> Digits = new List<int>();
            List<int> ResDigits = new List<int>(); ;
            Boolean b = true;

            int res;
            Random rand = new Random();

            //заполняем список с именами и индексами участников
            for (int i = 0; i < lstPlayersList.Count; i++)
            {
                names.Add(lstPlayersList[i]);
                Digits.Add(i);
            }

            //рандомим
            for (int i = 0; i < names.Count; i++)
            {
                while (true)
                {
                    res = rand.Next(0, Digits.Count);
                    while (true)
                    {
                        if (ResDigits.IndexOf(res) != -1)   //Нельзя дарить подарок тому кому уже дарят
                        {
                            res = rand.Next(0, names.Count);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (res != i)       //нельзя дарить подарок себе
                    {
                        Digits.Remove(res);
                        b = false;
                        Console.WriteLine(names[i] + " randomed " + names[res]);
                        ResDigits.Add(res);
                        break;
                    }
                }
            }

            //возвращаем список с индексами тех кому дарят
            return ResDigits;
        }
    }
}
