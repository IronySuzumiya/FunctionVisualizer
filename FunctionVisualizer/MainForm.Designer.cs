namespace FunctionVisualizer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtFunctionInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBeginRender = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "f(x, y) =";
            // 
            // txtFunctionInput
            // 
            this.txtFunctionInput.Location = new System.Drawing.Point(70, 13);
            this.txtFunctionInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFunctionInput.Name = "txtFunctionInput";
            this.txtFunctionInput.Size = new System.Drawing.Size(505, 23);
            this.txtFunctionInput.TabIndex = 1;
            this.txtFunctionInput.TextChanged += new System.EventHandler(this.txtFunctionInput_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(583, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "= 0";
            // 
            // btnBeginRender
            // 
            this.btnBeginRender.Location = new System.Drawing.Point(15, 43);
            this.btnBeginRender.Name = "btnBeginRender";
            this.btnBeginRender.Size = new System.Drawing.Size(116, 28);
            this.btnBeginRender.TabIndex = 3;
            this.btnBeginRender.Text = "Begin Render";
            this.btnBeginRender.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(137, 49);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 17);
            this.lblMessage.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 370);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnBeginRender);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFunctionInput);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Function Visualizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFunctionInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBeginRender;
        private System.Windows.Forms.Label lblMessage;
    }
}

