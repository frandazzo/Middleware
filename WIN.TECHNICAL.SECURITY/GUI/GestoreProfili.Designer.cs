
namespace WIN.SECURITY.GUI
{
    partial class GestoreProfili
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
            this.uxProfileManagementControl1 = new uxProfileManagementControl();
            this.SuspendLayout();
            // 
            // uxProfileManagementControl1
            // 
            this.uxProfileManagementControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxProfileManagementControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxProfileManagementControl1.Location = new System.Drawing.Point(0, 0);
            this.uxProfileManagementControl1.Name = "uxProfileManagementControl1";
            this.uxProfileManagementControl1.Size = new System.Drawing.Size(619, 421);
            this.uxProfileManagementControl1.TabIndex = 0;
            // 
            // GestoreProfili
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 421);
            this.Controls.Add(this.uxProfileManagementControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GestoreProfili";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestione profili e permessi";
            this.Load += new System.EventHandler(this.GestoreProfili_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WIN.SECURITY.GUI.uxProfileManagementControl uxProfileManagementControl1;
    }
}