namespace CoffeeShopApp
{
    partial class FormCustomers
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.dgvCards = new System.Windows.Forms.DataGridView();
            this.btnSaveCustomer = new System.Windows.Forms.Button();
            this.btnSaveCards = new System.Windows.Forms.Button();
            this.btnCreateCard = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblCustomersTitle = new System.Windows.Forms.Label();
            this.lblCardsTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Location = new System.Drawing.Point(20, 105);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.Size = new System.Drawing.Size(390, 270);
            this.dgvCustomers.TabIndex = 0;
            // 
            // dgvCards
            // 
            this.dgvCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCards.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCards.BackgroundColor = System.Drawing.Color.White;
            this.dgvCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCards.Location = new System.Drawing.Point(430, 105);
            this.dgvCards.Name = "dgvCards";
            this.dgvCards.Size = new System.Drawing.Size(380, 270);
            this.dgvCards.TabIndex = 1;
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(90)))), ((int)(((byte)(43)))));
            this.btnSaveCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveCustomer.FlatAppearance.BorderSize = 0;
            this.btnSaveCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCustomer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveCustomer.ForeColor = System.Drawing.Color.White;
            this.btnSaveCustomer.Location = new System.Drawing.Point(20, 390);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(180, 35);
            this.btnSaveCustomer.TabIndex = 2;
            this.btnSaveCustomer.Text = "💾 Зберегти клієнтів";
            this.btnSaveCustomer.UseVisualStyleBackColor = false;
            this.btnSaveCustomer.Click += new System.EventHandler(this.btnSaveCustomer_Click);
            // 
            // btnSaveCards
            // 
            this.btnSaveCards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(90)))), ((int)(((byte)(43)))));
            this.btnSaveCards.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveCards.FlatAppearance.BorderSize = 0;
            this.btnSaveCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCards.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSaveCards.ForeColor = System.Drawing.Color.White;
            this.btnSaveCards.Location = new System.Drawing.Point(430, 390);
            this.btnSaveCards.Name = "btnSaveCards";
            this.btnSaveCards.Size = new System.Drawing.Size(170, 35);
            this.btnSaveCards.TabIndex = 3;
            this.btnSaveCards.Text = "💾 Зберегти картки";
            this.btnSaveCards.UseVisualStyleBackColor = false;
            this.btnSaveCards.Click += new System.EventHandler(this.btnSaveCards_Click);
            // 
            // btnCreateCard
            // 
            this.btnCreateCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.btnCreateCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateCard.FlatAppearance.BorderSize = 0;
            this.btnCreateCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateCard.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCreateCard.ForeColor = System.Drawing.Color.White;
            this.btnCreateCard.Location = new System.Drawing.Point(610, 390);
            this.btnCreateCard.Name = "btnCreateCard";
            this.btnCreateCard.Size = new System.Drawing.Size(200, 35);
            this.btnCreateCard.TabIndex = 4;
            this.btnCreateCard.Text = "💳 Видати бонусну картку";
            this.btnCreateCard.UseVisualStyleBackColor = false;
            this.btnCreateCard.Click += new System.EventHandler(this.btnCreateCard_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(243)))), ((int)(((byte)(238)))));
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(834, 60);
            this.panelHeader.TabIndex = 5;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(51)))), ((int)(((byte)(23)))));
            this.lblHeader.Location = new System.Drawing.Point(15, 17);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(437, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "👥 Керування клієнтами та картками лояльності";
            // 
            // lblCustomersTitle
            // 
            this.lblCustomersTitle.AutoSize = true;
            this.lblCustomersTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(51)))), ((int)(((byte)(23)))));
            this.lblCustomersTitle.Location = new System.Drawing.Point(20, 78);
            this.lblCustomersTitle.Name = "lblCustomersTitle";
            this.lblCustomersTitle.Size = new System.Drawing.Size(134, 19);
            this.lblCustomersTitle.TabIndex = 6;
            this.lblCustomersTitle.Text = "Клієнти кофемережі";
            // 
            // lblCardsTitle
            // 
            this.lblCardsTitle.AutoSize = true;
            this.lblCardsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblCardsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(51)))), ((int)(((byte)(23)))));
            this.lblCardsTitle.Location = new System.Drawing.Point(430, 78);
            this.lblCardsTitle.Name = "lblCardsTitle";
            this.lblCardsTitle.Size = new System.Drawing.Size(167, 19);
            this.lblCardsTitle.TabIndex = 7;
            this.lblCardsTitle.Text = "Бонусні картки клієнтів";
            // 
            // FormCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(834, 450);
            this.Controls.Add(this.lblCardsTitle);
            this.Controls.Add(this.lblCustomersTitle);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.btnCreateCard);
            this.Controls.Add(this.btnSaveCards);
            this.Controls.Add(this.btnSaveCustomer);
            this.Controls.Add(this.dgvCards);
            this.Controls.Add(this.dgvCustomers);
            this.Name = "FormCustomers";
            this.Text = "Керування клієнтами та картками лояльності";
            this.Load += new System.EventHandler(this.FormCustomers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.DataGridView dgvCards;
        private System.Windows.Forms.Button btnSaveCustomer;
        private System.Windows.Forms.Button btnSaveCards;
        private System.Windows.Forms.Button btnCreateCard;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCustomersTitle;
        private System.Windows.Forms.Label lblCardsTitle;
    }
}