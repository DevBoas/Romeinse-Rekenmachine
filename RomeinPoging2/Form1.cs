using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace RomeinPoging2
{
    public partial class Form1 : Form
    {
        string operand1 = string.Empty;
        char operation;
        public Form1()
        {
            InitializeComponent();
        }

        string[] romanStringArr = new string[] { "I", "V", "X", "L", "C", "D", "M" };
        int[] romanNumArr = new int[] { 1, 5, 10, 50, 100, 500, 1000 };
        private int findNext(string toUp)
        {
            int next = 0;
            for (int i = 0; i < romanStringArr.Length; i++)
            {
                if (romanStringArr[i] == toUp)
                {
                    next = i + 1;
                    if (next > romanStringArr.Length)
                    {
                        next = romanStringArr.Length;
                    }
                    break;
                }
            }
            return next;
        }
        /*
        private int biggestFit(int romanNummer)
        {
            int biggest = 0;
            for (int i = 0; i < romanNumArr.Length; i++)
            {
                if (romanNumArr[i] > romanNummer)
                {
                    biggest = i - 1;
                    break;
                }
            }
            return biggest; 
        }*/
        private int biggestFit(int romanNummer)
        {
            int biggest = 0;
            for (int i = 0; i < romanNumArr.Length; i++)
            {
                if ((romanNumArr[i] >= romanNummer) || romanNummer >= 1000)
                {
                    if (romanNummer >= 1000)
                    {
                        biggest = romanNumArr.Length - 1;
                    }
                    else if (romanNumArr[i] == romanNummer)
                    {
                        biggest = i;
                    }
                    else
                        biggest = i - 1;
                    break;
                }
            }
            return biggest;
        }

        private string intToRoman(int romanNummer, string romanString, int location, int count, char last, int original)
        {
            int biggest = biggestFit(romanNummer);
            ///MessageBox.Show("Biggest nummer to fit " + romanNumArr[biggest].ToString() + " adding " + romanStringArr[biggest]);

            romanString += romanStringArr[biggest];
            Output.Text = romanString;
           location++;
            count++;
            if (last == romanStringArr[biggest][0])
            {
                ///MessageBox.Show("Duplicate count = " + count);
                if (count == 4)
                {
                    string saveString = romanString.Substring(0, location - 4);
                    string toUp = romanString[location - 1].ToString();
                    ///MessageBox.Show("Need to take action! Savestring = " + saveString);
                    //MessageBox.Show("I am going to up this one " + toUp);
                    int next = findNext(toUp);
                    ///MessageBox.Show("Next = " + next);
                    if (next != 7)
                    {
                        if (saveString.Length > 0)
                        {
                            string dictator = saveString[saveString.Length - 1].ToString();
                            ///romanString = saveString + toUp + romanStringArr[next];
                            ///MessageBox.Show("I am going to up this one " + toUp);
                            ///MessageBox.Show("Dictator = " + dictator);
                            ///MessageBox.Show("Next = " + romanStringArr[next]);
                            if (dictator == romanStringArr[next])
                            {
                                next = findNext(dictator);
                                ///MessageBox.Show("Gotta throw away one from SafeWord");
                                saveString = romanString.Substring(0, saveString.Length - 1);
                                ///
                            }
                            romanString = saveString + toUp + romanStringArr[next];
                            ///else
                            //{
                                ///romanString = saveString + toUp + romanStringArr[next];
                            ///}
                            //saveString.Length
                        }
                        else
                            romanString = saveString + toUp + romanStringArr[next];
                    }
                    Output.Text = romanString;
                    location = romanString.Length;
                }
            }
            else
            {
                count = 1;
            }
            last = romanStringArr[biggest][0];
           
            romanNummer -= romanNumArr[biggest];
            
            if (romanNummer == 0)
            {
                return romanString;
            }
          
            return intToRoman(romanNummer, romanString, location, count, last, original);
        }
        private int diffLookup(string str1 , string str2)
        {
            int diff = 1;
            int found = 0;

            for (int i = 0; i < romanStringArr.Length; i++)
            {
                if (romanStringArr[i] == str1)
                {
                    found = 1;
                }
                if ((found == 1) && (romanStringArr[i] == str2))
                {
                    //diff = i - diff;
                    break;
                }
                if (found == 1)
                    diff++;
            }

            return diff;
        }
        private int reverseLookup(string next)
        {
            int rev = 0;
            for (int i = 0; i < romanStringArr.Length; i++)
            {
                if (romanStringArr[i] == next)
                {
                    rev = i;
                    break;
                }
            }
            return rev;
        }
        private int romanToInt(string romanInput)
        {
            int romanInt = 0;
            Boolean ignore = false;
            for (int i = 0; i < romanInput.Length; i++)
            {
                if (!ignore)
                {
                    for (int y = 0; y < romanStringArr.Length; y++)
                    {
                        if (romanStringArr[y] == romanInput[i].ToString())
                        {
                            ///MessageBox.Show("Now: " + romanInput[i].ToString());
                            if ((i + 1) < romanInput.Length)
                            {
                                ///MessageBox.Show("Next: " + romanInput[i + 1].ToString());
                                int lIndex = reverseLookup(romanInput[i].ToString());
                                int rIndex = reverseLookup(romanInput[i + 1].ToString());
                                int diff = rIndex - lIndex;
                                ///MessageBox.Show("The difference = " + diff.ToString());
                                ///MessageBox.Show("The rIndex = " + rIndex.ToString());
                                if (diff > 0)
                                {
                                    ///MessageBox.Show((romanNumArr[rIndex] - romanNumArr[rIndex - diff]).ToString());
                                    ///MessageBox.Show("Work");
                                    romanInt += romanNumArr[rIndex] - romanNumArr[rIndex - diff];
                                    ignore = true;
                                }
                                else
                                    romanInt += romanNumArr[y];
                                /* if (romanStringArr[next] == romanInput[i + 1].ToString())
                                    {
                                        MessageBox.Show((romanNumArr[next] - romanNumArr[next - 1]).ToString());
                                        MessageBox.Show("Work");
                                        romanInt += (romanNumArr[next] - romanNumArr[next - 1]);
                                        ignore = true;
                                    }
                                    else
                                        romanInt += romanNumArr[y];*/
                            }
                            else
                                romanInt += romanNumArr[y];
                        }
                    }
                }
                else
                {
                    ///MessageBox.Show("Ignoring " + romanInput[i].ToString());
                    ignore = false;
                }
            }
            return romanInt;
        }
        private void Calculate_Click(object sender, EventArgs e)
        {
            /*int romanNummer = 3333;

            string romanString = intToRoman(romanNummer, "", 0, 1,'?', romanNummer);
            MessageBox.Show("Input = " + romanNummer.ToString()
            + " Output = " + romanString);
            Output.Text = romanString;*/

            /*string romanInput = "XCIX";//V
            int romanNummer = romanToInt(romanInput);
            MessageBox.Show("Input = " + romanInput
           + " Output = " + romanNummer.ToString());
            Output.Text = romanNummer.ToString(); */

            int romanNummer1 = romanToInt(operand1);
            int romanNummer2 = romanToInt(Output.Text);
            int result = 0;
            MessageBox.Show("Nummer1 = " + romanNummer1.ToString()
                + " Nummer2 = " + romanNummer2.ToString());
            if (operation == '+')
            {
                result = romanNummer1 + romanNummer2;
                
            }
            else if (operation == '-')
            {
                result = romanNummer1 - romanNummer2;
            }
            else if (operation == '*')
            {
                result = romanNummer1 * romanNummer2;
            }
            else if (operation == '*')
            {
                result = romanNummer1 * romanNummer2;
            }
            else if (operation == '/')
            {
                result = romanNummer1 / romanNummer2;
            }
            if (result > 0)
            {
                Output.Text = intToRoman(result, "", 0, 1, '?', result);
            }
        }

        private void One_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Two_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Three_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Four_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Five_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Six_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Seven_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Eight_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Nine_Click(object sender, EventArgs e)
        {
            Output.Text += (sender as Button).Text;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            operand1 = "";
            Output.Text = "";
        }

        private void Plus_Click(object sender, EventArgs e)
        {
            operand1 = Output.Text;
            operation = '+';
            Output.Text = "";
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            operand1 = Output.Text;
            operation = '-';
            Output.Text = "";
        }

        private void Multiply_Click(object sender, EventArgs e)
        {
            operand1 = Output.Text;
            operation = '*';
            Output.Text = "";
        }

        private void Divide_Click(object sender, EventArgs e)
        {
            operand1 = Output.Text;
            operation = '/';
            Output.Text = "";
        }
    }
}
