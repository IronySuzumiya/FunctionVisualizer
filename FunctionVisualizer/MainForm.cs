using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunctionAnalyzer;
using FunctionAnalyzer.Expressions;

namespace FunctionVisualizer
{
    public partial class MainForm : Form
    {
        private RawExpression function;

        public MainForm()
        {
            InitializeComponent();
        }

        private void txtFunctionInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                function = ExpressionParser.Parse(txtFunctionInput.Text);
                lblMessage.Text = "Ready for: " + function + " = 0";
                btnBeginRender.Enabled = true;
            }
            catch(ParsingException ex)
            {
                lblMessage.Text = ex.Message;
                btnBeginRender.Enabled = false;
            }
        }
    }
}
