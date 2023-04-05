namespace TextBoxLoggerProject
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxLog = new TextBox();
            buttonInputText = new Button();
            textBoxInput = new TextBox();
            buttonInputAsync = new Button();
            SuspendLayout();
            // 
            // textBoxLog
            // 
            textBoxLog.Location = new Point(12, 57);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Both;
            textBoxLog.Size = new Size(776, 380);
            textBoxLog.TabIndex = 0;
            // 
            // buttonInputText
            // 
            buttonInputText.Location = new Point(593, 7);
            buttonInputText.Margin = new Padding(2, 2, 2, 2);
            buttonInputText.Name = "buttonInputText";
            buttonInputText.Size = new Size(78, 25);
            buttonInputText.TabIndex = 1;
            buttonInputText.Text = "Text Input";
            buttonInputText.UseVisualStyleBackColor = true;
            buttonInputText.Click += buttonInputText_Click;
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(12, 9);
            textBoxInput.Margin = new Padding(2, 2, 2, 2);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(577, 23);
            textBoxInput.TabIndex = 2;
            textBoxInput.Text = "Input Text Here";
            // 
            // buttonInputAsync
            // 
            buttonInputAsync.Location = new Point(675, 7);
            buttonInputAsync.Margin = new Padding(2, 2, 2, 2);
            buttonInputAsync.Name = "buttonInputAsync";
            buttonInputAsync.Size = new Size(114, 25);
            buttonInputAsync.TabIndex = 3;
            buttonInputAsync.Text = " Text Input Async";
            buttonInputAsync.UseVisualStyleBackColor = true;
            buttonInputAsync.Click += buttonInputAsync_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonInputAsync);
            Controls.Add(textBoxInput);
            Controls.Add(buttonInputText);
            Controls.Add(textBoxLog);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox textBoxLog;
        private Button buttonInputText;
        private TextBox textBoxInput;
        private Button buttonInputAsync;
    }
}