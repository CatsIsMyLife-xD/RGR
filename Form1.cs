using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace РГР
{
    public partial class Form1 : Form
    {
        string pathd = @"C:\3 курс\5 семестр\ООП\Лабы\РГР\Данные.txt";
        string pathr = @"C:\3 курс\5 семестр\ООП\Лабы\РГР\Результаты.txt";
        public string author;
        public string name;
        class Book
        {
            public string cypher, author, name, year, place;
            public string all
            {
                get
                {
                    return cypher + " " + author + " " + name + " " + year + " " + place;
                }
            }
        }
        public string text() 
        {
            try
            {
                using (StreamReader sr = new StreamReader(pathd))
                {
                    return(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                return (e.Message);
            }

        }
        static ref Book[] action(string[] buf, ref Book[] ob) 
        {
            for (int i = 0; i < buf.Length; i++)
            {
                ob[i] = new Book();
            }
            for (int j = 0; j < buf.Length; j++)
            {
                string []a = buf[j].Split(' ');
                if (!String.IsNullOrEmpty(a[0]))
                {
                    ob[j].cypher = a[0];
                    ob[j].author = a[1];
                    ob[j].name = a[2];
                    ob[j].year = a[3];
                    ob[j].place = a[4];
                }
            }
            return ref ob;
        }
        
        public Form1()
        {
            InitializeComponent();
            string []a = text().Split('\r','\n');
            textBox6.ScrollBars = ScrollBars.Vertical;
            dataGridView1.ColumnCount = 5;
            dataGridView1.RowCount = a.Length;
            Book[] ob = new Book[a.Length];
            ob = action(a, ref ob);
            dataGridView1.Columns[0].HeaderText = "Шифр";
            dataGridView1.Columns[1].HeaderText = "Автор";
            dataGridView1.Columns[2].HeaderText = "Название";
            dataGridView1.Columns[3].HeaderText = "Год";
            dataGridView1.Columns[4].HeaderText = "Место";
            for (int j = 0; j < a.Length; j++)
            {
                if (!String.IsNullOrEmpty(ob[j].cypher) || !String.IsNullOrEmpty(ob[j].year) 
                    || !String.IsNullOrEmpty(ob[j].year) || !String.IsNullOrEmpty(ob[j].name)
                    || !String.IsNullOrEmpty(ob[j].place))
                {
                    comboBox1.Items.Add(ob[j].year);
                    comboBox2.Items.Add(ob[j].author);
                    comboBox3.Items.Add(ob[j].name);
                    dataGridView1.Rows[j].Cells[0].Value = ob[j].cypher;
                    dataGridView1.Rows[j].Cells[1].Value = ob[j].author;
                    dataGridView1.Rows[j].Cells[2].Value = ob[j].name;
                    dataGridView1.Rows[j].Cells[3].Value = ob[j].year;
                    dataGridView1.Rows[j].Cells[4].Value = ob[j].place;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 ob = new Form2();
            ob.ShowDialog();
            string author = ob.textBox1.Text, name = ob.textBox2.Text;
            textBox6.Text += "\r\nКнига автора " + author +" названия "+ name + "\t:  \r\n";
            string[] a = text().Split('\r', '\n');
            Book[] ob1 = new Book[a.Length];
            ob1 = action(a, ref ob1);
            for (int i = 0; i < ob1.Length; i++)
            {
                if (ob1[i].author == author && ob1[i].name == name)
                {
                    textBox6.Text += ob1[i].cypher + " " + ob1[i].author + " " +
                        ob1[i].name + " " + ob1[i].year + " " + ob1[i].place + "\r\n";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Book[] ob = new Book[dataGridView1.RowCount];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                ob[i] = new Book();
            }
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            string all = "";
            for (int j = 0; j < dataGridView1.RowCount; j++)
            {
                if (!string.IsNullOrEmpty((string)dataGridView1.Rows[j].Cells[0].Value)) 
                {

                    ob[j].cypher = dataGridView1.Rows[j].Cells[0].Value.ToString();
                    ob[j].author = dataGridView1.Rows[j].Cells[1].Value.ToString();
                    ob[j].name = dataGridView1.Rows[j].Cells[2].Value.ToString();
                    ob[j].year = dataGridView1.Rows[j].Cells[3].Value.ToString();
                    ob[j].place = dataGridView1.Rows[j].Cells[4].Value.ToString();
                    all += ob[j].cypher + " " + ob[j].author + " " + ob[j].name + " " +
                        ob[j].year + " " + ob[j].place + "\n";
                    comboBox1.Items.Add(ob[j].year);
                    comboBox2.Items.Add(ob[j].author);
                    comboBox3.Items.Add(ob[j].name);
                }
                
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(pathd))
                {
                    sw.Write(all);
                    sw.Close();
                }
                
            }
            catch (Exception w)
            {
                Console.WriteLine(w.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 ob = new Form3();
            ob.ShowDialog();
            string author = ob.textBox1.Text;
            textBox6.Text += "\r\nСписок книг автора "+ author+ " : ";
            string[] a = text().Split('\r', '\n');
            Book[] ob1 = new Book[a.Length];
            ob1 = action(a, ref ob1);
            for (int i = 0; i < ob1.Length; i++)
            {
                if (ob1[i].author == author)
                {
                    textBox6.Text += ob1[i].cypher + " " + ob1[i].author + " " +
                        ob1[i].name + " " + ob1[i].year + " " + ob1[i].place + "\n";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Text += "\r\nЧисло книг определенного года издания " + comboBox1.Text + " : ";
            string[] a = text().Split('\r', '\n');
            Book[] ob1 = new Book[a.Length];
            ob1 = action(a, ref ob1);
            int k = 0;
            for (int i = 0; i < ob1.Length; i++)
            {
                if (ob1[i].year == comboBox1.Text)
                {
                    k++;
                }
            }
            textBox6.Text += k;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Text += "\r\nЧисло книг определенного автора " + comboBox2.Text + " : ";
            string[] a = text().Split('\r', '\n');
            Book[] ob1 = new Book[a.Length];
            ob1 = action(a, ref ob1);
            int k = 0;
            for (int i = 0; i < ob1.Length; i++)
            {
                if (ob1[i].author == comboBox2.Text)
                {
                    k++;
                }
            }
            textBox6.Text += k;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Text += "\r\nЧисло книг определенного названия " + comboBox3.Text + " : ";
            string[] a = text().Split('\r', '\n');
            Book[] ob1 = new Book[a.Length];
            ob1 = action(a, ref ob1);
            int k = 0;
            for (int i = 0; i < ob1.Length; i++)
            {
                if (ob1[i].name == comboBox3.Text)
                {
                    k++;
                }
            }
            textBox6.Text += k;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(pathr))
                {
                    sw.Write(textBox6.Text);
                    sw.Close();
                }

            }
            catch (Exception w)
            {
                Console.WriteLine(w.Message);
            }
        }
    }
}
