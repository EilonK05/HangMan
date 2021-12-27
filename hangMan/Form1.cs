using hangMan.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hangMan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetImageArray();
        }
        private int m_CurrentLabelLetter = 1;
        private string m_WordToGuess;
        private bool m_IsFirstPlayer = true;
        private Image[] m_Images = new Image[7];
        private int m_CountError = 0;
        private int m_CountGuess = 0;
        public void SetImageArray()
        {
            m_Images[0] = Resources.p0;
            m_Images[1] = Resources.p1;
            m_Images[2] = Resources.p2;
            m_Images[3] = Resources.p3;
            m_Images[4] = Resources.p4;
            m_Images[5] = Resources.p5;
            m_Images[6] = Resources.p6;
        }
        private void button_Click(object sender, EventArgs e)
        {
            string buttonText = (sender as Button).Text;
            if(m_IsFirstPlayer)
            {
                groupBox1.Controls["label_Letter" + m_CurrentLabelLetter].Text = buttonText;
                m_CurrentLabelLetter++;
                if (m_CurrentLabelLetter == 6)
                    button28.Enabled = true;
            }
            else
            {
                if (m_CountError > 5)
                {
                    GameOver();
                }
                else if (CheckWin())
                {
                    Win();
                }
                else
                {
                    if (m_WordToGuess.Contains(buttonText))
                    {
                        for (int i = 0; i < m_WordToGuess.Length; i++)
                        {
                            if (m_WordToGuess[i].ToString() == buttonText)
                                groupBox1.Controls["label_Letter" + (i + 1)].Text = buttonText;
                        }
                        m_CountGuess++;
                        if (CheckWin())
                        {
                            Win();
                        }
                    }
                    else
                    {
                        m_CountError++;
                        pictureBox1.Image = m_Images[m_CountError];
                    }
                }
                (sender as Button).Enabled = false;
            }
            
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (m_CurrentLabelLetter > 1)
            {
                groupBox1.Controls["label_Letter" + (m_CurrentLabelLetter-1)].Text = "_";
                m_CurrentLabelLetter--;
            }
            button28.Enabled = false;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Label curLabel;
            for (int i = 1; i <= 5; i++)
            {
                curLabel = groupBox1.Controls["label_Letter" + i] as Label;
                m_WordToGuess += curLabel.Text;
                curLabel.Text = "_";
            }
            button27.Visible = false;
            button28.Visible = false;
            m_IsFirstPlayer = false;
        }

        private void GameOver()
        {
            groupBox2.Visible = false;
            groupBox1.Visible = false;
            gameOverLabel.Visible = true;
            resetGameButton.Visible = true;
            pictureBox1.Visible = false;
        }

        private void Win()
        {
            groupBox2.Visible = false;
            groupBox1.Visible = false;
            winLabel.Visible = true;
            pictureBoxWin.Visible = true;
            pictureBox1.Visible = false;
        }

        private bool CheckWin()
        {
            bool isWin = true;
            for (int i = 1; i <= 5; i++)
            {
                if (groupBox1.Controls["label_Letter" + i].Text == "_")
                {
                    isWin = false;
                }
            }
            return isWin;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ResetGameButton_Click(object sender, EventArgs e)
        {
            gameOverLabel.Visible = false;
            resetGameButton.Visible = false;
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            m_IsFirstPlayer = true;
            m_CountError = 0;
            m_CountGuess = 0;
            button27.Visible = true;
            button28.Visible = true;
            m_CurrentLabelLetter = 1;
            pictureBox1.Image = null;
            pictureBox1.Visible = true;
            
            for(int i = 1; i <= 26; i++)
            {
                groupBox2.Controls["button" + i].Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

