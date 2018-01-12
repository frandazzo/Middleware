
namespace WIN.SECURITY.GUI
{
    partial class uxLogonForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.uxUserTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.uxLogonPictureBox = new System.Windows.Forms.PictureBox();
            this.uxLogonButton = new System.Windows.Forms.Button();
            this.uxCancelButton = new System.Windows.Forms.Button();
            this.uxErrorLabel = new System.Windows.Forms.Label();
            this.uxPassTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uxLogonPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome utente:";
            // 
            // uxUserTextBox
            // 
            this.uxUserTextBox.Location = new System.Drawing.Point(101, 110);
            this.uxUserTextBox.Name = "uxUserTextBox";
            this.uxUserTextBox.Size = new System.Drawing.Size(199, 21);
            this.uxUserTextBox.TabIndex = 1;
            this.uxUserTextBox.Enter += new System.EventHandler(this.uxUserTextBox_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // uxLogonPictureBox
            // 
            this.uxLogonPictureBox.BackColor = System.Drawing.Color.White;
            this.uxLogonPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxLogonPictureBox.Location = new System.Drawing.Point(0, 0);
            this.uxLogonPictureBox.Name = "uxLogonPictureBox";
            this.uxLogonPictureBox.Size = new System.Drawing.Size(312, 85);
            this.uxLogonPictureBox.TabIndex = 6;
            this.uxLogonPictureBox.TabStop = false;
            // 
            // uxLogonButton
            // 
            this.uxLogonButton.Location = new System.Drawing.Point(124, 212);
            this.uxLogonButton.Name = "uxLogonButton";
            this.uxLogonButton.Size = new System.Drawing.Size(85, 48);
            this.uxLogonButton.TabIndex = 3;
            this.uxLogonButton.Text = "Login";
            this.uxLogonButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.uxLogonButton.UseVisualStyleBackColor = true;
            this.uxLogonButton.Click += new System.EventHandler(this.uxLogonButton_Click);
            // 
            // uxCancelButton
            // 
            this.uxCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxCancelButton.Location = new System.Drawing.Point(215, 212);
            this.uxCancelButton.Name = "uxCancelButton";
            this.uxCancelButton.Size = new System.Drawing.Size(85, 48);
            this.uxCancelButton.TabIndex = 4;
            this.uxCancelButton.Text = "Annulla";
            this.uxCancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.uxCancelButton.UseVisualStyleBackColor = true;
            // 
            // uxErrorLabel
            // 
            this.uxErrorLabel.AutoSize = true;
            this.uxErrorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.uxErrorLabel.Location = new System.Drawing.Point(12, 183);
            this.uxErrorLabel.Name = "uxErrorLabel";
            this.uxErrorLabel.Size = new System.Drawing.Size(182, 13);
            this.uxErrorLabel.TabIndex = 7;
            this.uxErrorLabel.Text = "Nome utente o password errati";
            this.uxErrorLabel.Visible = false;
            // 
            // uxPassTextBox
            // 
            this.uxPassTextBox.Location = new System.Drawing.Point(101, 148);
            this.uxPassTextBox.Name = "uxPassTextBox";
            this.uxPassTextBox.PasswordChar = '*';
            this.uxPassTextBox.Size = new System.Drawing.Size(199, 21);
            this.uxPassTextBox.TabIndex = 2;
            this.uxPassTextBox.Enter += new System.EventHandler(this.uxPassTextBox_Enter);
            // 
            // uxLogonForm
            // 
            this.AcceptButton = this.uxLogonButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uxCancelButton;
            this.ClientSize = new System.Drawing.Size(312, 272);
            this.Controls.Add(this.uxPassTextBox);
            this.Controls.Add(this.uxErrorLabel);
            this.Controls.Add(this.uxLogonPictureBox);
            this.Controls.Add(this.uxLogonButton);
            this.Controls.Add(this.uxCancelButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uxUserTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "uxLogonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.uxLogonPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uxUserTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button uxCancelButton;
        private System.Windows.Forms.Button uxLogonButton;
        private System.Windows.Forms.PictureBox uxLogonPictureBox;
        private System.Windows.Forms.Label uxErrorLabel;
        private System.Windows.Forms.TextBox uxPassTextBox;

    }
}