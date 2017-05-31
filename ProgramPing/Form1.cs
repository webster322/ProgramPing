using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace ProgramPing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
                if (textBox2.Text.Trim().Length > 0)
                {
                    listBox2.Items.Add(textBox2.Text);
                    textBox1.Clear();
                }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }

        private string WyslijPing(string adres, int timeout, byte[] bufor, PingOptions opcje)
        {
            Ping ping = new Ping();
            try
            {
                PingReply odpowiedz = ping.Send(adres, timeout, bufor, opcje);
                if (odpowiedz.Status == IPStatus.Success)
                    return "Odpowiedź z " + adres + " " + " czas=" + odpowiedz.RoundtripTime + "ms TTL=" + odpowiedz.Options.Ttl;
                else
                    return "Błąd:" + adres + " " + odpowiedz.Status.ToString();
            }
            catch (Exception ex)
            {
                return "Błąd:" + adres + " " + ex.Message;
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text !="" || listBox2.Items.Count > 0)
            {
                PingOptions opcje = new PingOptions();
                opcje.Ttl = (int)numericUpDown1.Value;
                opcje.DontFragment = true;
                string dane = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] bufor = Encoding.ASCII.GetBytes(dane);
                int timeout = 120;
                if (textBox1.Text != "")
                {
                    for (int i = 0; i < (int)numericUpDown1.Value; i++)
                        listBox1.Items.Add(this.WyslijPing(textBox1.Text, timeout, bufor, opcje));
                    listBox1.Items.Add("----------------");
                }
                if (listBox2.Items.Count > 0)
                {
                    foreach (string host in listBox2.Items)
                    {
                        for (int i = 0; i < (int)numericUpDown1.Value; i++)
                            listBox1.Items.Add(this.WyslijPing(host, timeout, bufor, opcje));
                        listBox1.Items.Add("----------------");
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie wprowadzono żadnych adresów", "Błąd");
            }
        }
    }
}
