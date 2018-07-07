using AutoIt;
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
        int j = 0, i = 1;
        object s;
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


            //Текстовое поле учатника
            TextBox text = new TextBox
            {
                Width = 200,
                Location = new Point(flowLayoutPanel1.Location.X + 60, flowLayoutPanel1.Location.Y + 60 + 50 + j)
                //Location = new Point(b.Location.X + 60, b.Location.Y + 50 + j)
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

            
            flowLayoutPanel1.Controls.Add(text);
            flowLayoutPanel1.Controls.Add(label);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(text2);
            flowLayoutPanel1.Controls.Add(lstButton[0]);
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

            while (lstRassilka.Count == 0)
            {
                lstRassilka = Random();
            }
            
            //отправляем письма всем участникам
            for (int i = 0; i < lstEmails.Count; i++)
            {
                SendMail("smtp.gmail.com", textBox1.Text, textBox2.Text, lstEmails[i], "Santa", "You New Year target is: " + lstPlayersList[lstRassilka[i]]);
                MessageBox.Show("Почта для участника " + lstPlayersList[i] + " отправлена успешно!");
            }

            #region DraftMessageDeleteAutotest
            browse = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(browse, TimeSpan.FromSeconds(8));
            IJavaScriptExecutor js = (IJavaScriptExecutor)browse;
            browse.Manage().Window.Maximize();
            browse.Navigate().GoToUrl("https://accounts.google.com");
            IWebElement login = browse.FindElement(By.Id("identifierId"));
            IWebElement btnNext = browse.FindElement(By.Id("identifierNext"));

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
                
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //RandomizeAlgorithm
        private List<int> Random ()
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

                    switch (names[i])
                    {
                        case "Сашка":
                            if (res != names.IndexOf("Полина") && res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;

                        case "Полина":
                            if (res != names.IndexOf("Сашка") && res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;

                        case "Оля":
                            if (res != names.IndexOf("Богдан") && res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;

                        case "Богдан":
                            if (res != names.IndexOf("Оля") && res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;

                        case "Машка Басан":
                            if (res != names.IndexOf("Разак") && res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;

                        case "Разак":
                            if (res != names.IndexOf("Машка Басан") && res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;

                        case "Димон":
                            if (res != names.IndexOf("Машка Вовченко") && res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;

                        case "Машка Вовченко":
                            if (res != names.IndexOf("Димон") && res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;

                        default:
                            if (res != i)
                            {
                                Digits.Remove(res);
                                ResDigits.Add(res);
                                b = true;
                                break;
                            }
                            break;
                    }
                }
            }

            //возвращаем список с индексами тех кому дарят
            return ResDigits;
        }
    }
}
