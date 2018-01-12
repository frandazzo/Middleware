
namespace WIN.SECURITY.GUI
{
    partial class uxUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxInfoLabel = new System.Windows.Forms.Label();
            this.uxOKButton = new System.Windows.Forms.Button();
            this.uxCancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            
            this.label6 = new System.Windows.Forms.Label();
            this.uxMailTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.uxUsernameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uxSurnameTextBox = new System.Windows.Forms.TextBox();
            this.uxNameTextBox = new System.Windows.Forms.TextBox();
            this.uxPasswordTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
          
            this.SuspendLayout();
            // 
            // uxInfoLabel
            // 
            this.uxInfoLabel.AutoSize = true;
            this.uxInfoLabel.ForeColor = System.Drawing.Color.Blue;
            this.uxInfoLabel.Location = new System.Drawing.Point(12, 14);
            this.uxInfoLabel.MaximumSize = new System.Drawing.Size(250, 0);
            this.uxInfoLabel.Name = "uxInfoLabel";
            this.uxInfoLabel.Size = new System.Drawing.Size(168, 13);
            this.uxInfoLabel.TabIndex = 6;
            this.uxInfoLabel.Text = "L\'utente appartiene al gruppo {0}";
            // 
            // uxOKButton
            // 
            this.uxOKButton.Location = new System.Drawing.Point(15, 289);
            this.uxOKButton.Name = "uxOKButton";
            this.uxOKButton.Size = new System.Drawing.Size(52, 34);
            this.uxOKButton.TabIndex = 0;
            this.uxOKButton.Text = "OK";
            this.uxOKButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.uxOKButton.UseVisualStyleBackColor = true;
            this.uxOKButton.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // uxCancelButton
            // 
            this.uxCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxCancelButton.Location = new System.Drawing.Point(233, 289);
            this.uxCancelButton.Name = "uxCancelButton";
            this.uxCancelButton.Size = new System.Drawing.Size(65, 34);
            this.uxCancelButton.TabIndex = 1;
            this.uxCancelButton.Text = "Annulla";
            this.uxCancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.uxCancelButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.uxInfoLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 331);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 42);
            this.panel1.TabIndex = 25;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // uxNiceGroupBox1
            // 
           
            this.Controls.Add(this.label6);
            this.Controls.Add(this.uxMailTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uxUsernameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uxSurnameTextBox);
            this.Controls.Add(this.uxNameTextBox);
            this.Controls.Add(this.uxPasswordTextBox);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(45, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Mail:";
            // 
            // uxMailTextBox
            // 
            this.uxMailTextBox.Location = new System.Drawing.Point(48, 211);
            this.uxMailTextBox.Name = "uxMailTextBox";
            this.uxMailTextBox.Size = new System.Drawing.Size(196, 21);
            this.uxMailTextBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(45, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(45, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(45, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Cognome:";
            // 
            // uxUsernameTextBox
            // 
            this.uxUsernameTextBox.Location = new System.Drawing.Point(48, 51);
            this.uxUsernameTextBox.Name = "uxUsernameTextBox";
            this.uxUsernameTextBox.Size = new System.Drawing.Size(196, 21);
            this.uxUsernameTextBox.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(45, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nome:";
            // 
            // uxSurnameTextBox
            // 
            this.uxSurnameTextBox.Location = new System.Drawing.Point(48, 171);
            this.uxSurnameTextBox.Name = "uxSurnameTextBox";
            this.uxSurnameTextBox.Size = new System.Drawing.Size(196, 21);
            this.uxSurnameTextBox.TabIndex = 3;
            // 
            // uxNameTextBox
            // 
            this.uxNameTextBox.Location = new System.Drawing.Point(48, 131);
            this.uxNameTextBox.Name = "uxNameTextBox";
            this.uxNameTextBox.Size = new System.Drawing.Size(196, 21);
            this.uxNameTextBox.TabIndex = 2;
            // 
            // uxPasswordTextBox
            // 
            this.uxPasswordTextBox.Location = new System.Drawing.Point(48, 91);
            this.uxPasswordTextBox.Name = "uxPasswordTextBox";
            this.uxPasswordTextBox.PasswordChar = '*';
            this.uxPasswordTextBox.Size = new System.Drawing.Size(196, 21);
            this.uxPasswordTextBox.TabIndex = 1;
            // 
            // uxUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 373);
            this.Controls.Add(this.uxCancelButton);
            this.Controls.Add(this.uxOKButton);
            this.Controls.Add(this.panel1);
           
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "uxUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestione utente";
            this.Load += new System.EventHandler(this.NewUserForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
    
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uxUsernameTextBox;
        private System.Windows.Forms.TextBox uxPasswordTextBox;
        private System.Windows.Forms.Label uxInfoLabel;
        private System.Windows.Forms.TextBox uxSurnameTextBox;
        private System.Windows.Forms.TextBox uxNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button uxOKButton;
        private System.Windows.Forms.Button uxCancelButton;
        
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox uxMailTextBox;
    }
}