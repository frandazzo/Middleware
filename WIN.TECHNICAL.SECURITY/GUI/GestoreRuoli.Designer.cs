
namespace WIN.SECURITY.GUI
{
    partial class GestoreRuoli
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxRoleManagementControl1 = new WIN.SECURITY.GUI.uxRoleManagementControl();
            this.SuspendLayout();
            // 
            // uxRoleManagementControl1
            // 
            this.uxRoleManagementControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxRoleManagementControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxRoleManagementControl1.Location = new System.Drawing.Point(0, 0);
            this.uxRoleManagementControl1.Name = "uxRoleManagementControl1";
            this.uxRoleManagementControl1.Size = new System.Drawing.Size(598, 396);
            this.uxRoleManagementControl1.TabIndex = 0;
            // 
            // GestoreRuoli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 396);
            this.Controls.Add(this.uxRoleManagementControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GestoreRuoli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestione ruoli";
            this.Load += new System.EventHandler(this.GestoreRuoli_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WIN.SECURITY.GUI.uxRoleManagementControl uxRoleManagementControl1;
    }
}