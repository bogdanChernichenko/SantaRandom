﻿using AutoIt;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;

namespace SantaRandom
{
    public partial class Form1 : Form
    {
        int i = 1;
        IWebDriver browse;
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

            //Текстовое поле участника
            TextBox text = new TextBox
            {
                Width = 200,
                Anchor = AnchorStyles.Left
            };
            lstPlayers.Add(text);

            //Участник№
            Label label = new Label
            {
                Text = "Участник №" + i,
                Dock = DockStyle.Fill,
                Width = 90
            };

            //label email
            Label label2 = new Label
            {
                Text = "E-mail" + i,
                Dock = DockStyle.Fill,
                Width = 60
            };

            //Текстовое поле для мыла
            TextBox text2 = new TextBox
            {
                Width = 200,
                Anchor = AnchorStyles.Left
            };
            lstEmailsTextBoxes.Add(text2);

            //Кнопка запуска рандома
            Button randomButt = new Button
            {
                Text = "Santa randoooom",
                AutoSize = true,
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

            flowLayoutPanel1.Controls.AddRange(new Control[] { label, text }); //adding to layout control
            flowLayoutPanel2.Controls.AddRange(new Control[] { label2, text2 }); //adding to layout control
            flowLayoutPanel3.Controls.Add(lstButton[0]);
            i++;
        }

        //нажатие на кнопку рандома
        private void RandomButtClick(object sender, EventArgs e)
        {
            if (lstPlayers.Count == 1)
            {
                MessageBox.Show("В игре не может участвовать только 1 участник!", "Danger");
                return;
            }
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

            while (lstRassilka.Count == 0)
            {
                lstRassilka = Random();
            }

            //отправляем письма всем участникам
            for (int i = 0; i < lstEmails.Count; i++)
            {
                SendMail("smtp.gmail.com", textBox1.Text, textBox2.Text, lstEmails[i], "Santa", "Your New Year target is: " + lstPlayersList[lstRassilka[i]]);
                MessageBox.Show("Почта для участника " + lstPlayersList[i] + " отправлена успешно!");
            }

            #region DraftMessageDeleteAutotest
            browse = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(browse, TimeSpan.FromSeconds(8));
            browse.Manage().Window.Maximize();
            browse.Navigate().GoToUrl("https://accounts.google.com");
            IWebElement login = browse.FindElement(By.Id("identifierId"));
            IWebElement btnNext = browse.FindElement(By.Id("identifierNext"));

            //Thread.Sleep(2000);
            login.SendKeys(textBox1.Text);
            btnNext.Click();

            Thread.Sleep(1000);
            IWebElement password = browse.FindElement(By.Name("password"));
            btnNext = browse.FindElement(By.Id("passwordNext"));
            password.SendKeys(textBox2.Text);
            btnNext.Click();

            Thread.Sleep(2000);
            browse.Navigate().GoToUrl("https://gmail.com");
            browse.Navigate().GoToUrl("https://mail.google.com/mail/u/0/#trash");

            Thread.Sleep(2000);
            IWebElement deleteAllLettersButton = browse.FindElement(By.XPath("//span[@class='x2']"));

            deleteAllLettersButton.Click();

            AutoItX.Send("{Enter}");
            Thread.Sleep(4000);

            browse.Quit();
            #endregion

        }

        //Email send
        public static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message)
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(from)
                };
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

            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        //закрытие формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //Синхронизация скроллбаров по первому
        private void FlowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            FlowLayoutPanel2_Scroll(sender, e);
        }
        private void FlowLayoutPanel2_Scroll(object sender, ScrollEventArgs e)
        {
            flowLayoutPanel2.VerticalScroll.Value = flowLayoutPanel1.VerticalScroll.Value;
        }

        //кнопка исключений рандома для парочек
        private void Button2_Click(object sender, EventArgs e)
        {
            ExceptionsDialog exceptionsDialog = new ExceptionsDialog();
            exceptionsDialog.Show();
        }

        //RandomizeAlgorithm
        private List<int> Random()
        {
            List<String> names = new List<String>();
            List<int> Digits = new List<int>();
            List<int> ResDigits = new List<int>(); ;
            bool b = false;
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
                b = false;
                while (!b)
                {
                    if (i == names.Count - 1)
                    {
                        if (names[i] == names[Digits[0]])
                        {
                            ResDigits.Clear();
                            return ResDigits;
                        }
                        else
                        {
                            ResDigits.Add(names.IndexOf(names[Digits[0]]));
                            Digits.Remove(names.IndexOf(names[Digits[0]]));
                            return ResDigits;
                        }
                    }
                    res = rand.Next(0, Digits.Count);
                    while (true)
                    {
                        if (ResDigits.IndexOf(res) != -1)           //Нельзя дарить подарок тому кому уже дарят
                        {
                            res = rand.Next(0, names.Count);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (res != i && !IsPair(names[i], names[res]))      //Последняя проверка, проверяем, чтобы цель не дарила своей парочке 
                    {
                        Digits.Remove(res);
                        ResDigits.Add(res);
                        b = true;
                        break;
                    }
                }
            }

            //возвращаем список с индексами тех кому дарят
            return ResDigits;
        }

        private bool IsPair(string firstName, string secondName)
        {

            bool res = false;

            if (StaticDataStorage.PairsList.Find(x => x.Contains(firstName)) != null) //значит участник есть в списке парочек 
            {
                if (StaticDataStorage.PairsList.Exists(x => x.Contains(firstName + "," + secondName)) || StaticDataStorage.PairsList.Exists(x => x.Contains(secondName + "," + firstName))) res = true;
            }

            return res;
        }
    }
}
