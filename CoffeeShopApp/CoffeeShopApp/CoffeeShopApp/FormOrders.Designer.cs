namespace CoffeeShopApp
{
    partial class FormOrders
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
            this.lblCust = new System.Windows.Forms.Label();
            this.cbCustomer = new System.Windows.Forms.ComboBox();
            this.lblBarista = new System.Windows.Forms.Label();
            this.cbBarista = new System.Windows.Forms.ComboBox();
            this.lblShop = new System.Windows.Forms.Label();
            this.cbShop = new System.Windows.Forms.ComboBox();
            this.dgvOrderItems = new System.Windows.Forms.DataGridView();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.lblTotalDisplay = new System.Windows.Forms.Label();
            this.btnCalculateTotal = new System.Windows.Forms.Button();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.panelOrderDetails = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).BeginInit();
            this.panelOrderDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCust
            // 
            this.lblCust.AutoSize = true;
            this.lblCust.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCust.Location = new System.Drawing.Point(12, 13);
            this.lblCust.Name = "lblCust";
            this.lblCust.Size = new System.Drawing.Size(52, 17);
            this.lblCust.TabIndex = 0;
            this.lblCust.Text = "Клієнт:";
            // 
            // cbCustomer
            // 
            this.cbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cbCustomer.FormattingEnabled = true;
            this.cbCustomer.Location = new System.Drawing.Point(70, 10);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Size = new System.Drawing.Size(200, 25);
            this.cbCustomer.TabIndex = 1;
            // 
            // lblBarista
            // 
            this.lblBarista.AutoSize = true;
            this.lblBarista.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblBarista.Location = new System.Drawing.Point(286, 13);
            this.lblBarista.Name = "lblBarista";
            this.lblBarista.Size = new System.Drawing.Size(58, 17);
            this.lblBarista.TabIndex = 2;
            this.lblBarista.Text = "Бариста:";
            // 
            // cbBarista
            // 
            this.cbBarista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBarista.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cbBarista.FormattingEnabled = true;
            this.cbBarista.Location = new System.Drawing.Point(350, 10);
            this.cbBarista.Name = "cbBarista";
            this.cbBarista.Size = new System.Drawing.Size(180, 25);
            this.cbBarista.TabIndex = 3;
            // 
            // lblShop
            // 
            this.lblShop.AutoSize = true;
            this.lblShop.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblShop.Location = new System.Drawing.Point(546, 13);
            this.lblShop.Name = "lblShop";
            this.lblShop.Size = new System.Drawing.Size(51, 17);
            this.lblShop.TabIndex = 4;
            this.lblShop.Text = "Філіал:";
            // 
            // cbShop
            // 
            this.cbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShop.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cbShop.FormattingEnabled = true;
            this.cbShop.Location = new System.Drawing.Point(603, 10);
            this.cbShop.Name = "cbShop";
            this.cbShop.Size = new System.Drawing.Size(169, 25);
            this.cbShop.TabIndex = 5;
            // 
            // dgvOrderItems
            // 
            this.dgvOrderItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrderItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderItems.Location = new System.Drawing.Point(12, 65);
            this.dgvOrderItems.Name = "dgvOrderItems";
            this.dgvOrderItems.Size = new System.Drawing.Size(760, 260);
            this.dgvOrderItems.TabIndex = 6;
            this.dgvOrderItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderItems_CellValueChanged);
            this.dgvOrderItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvOrderItems_DataError);
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalTitle.Location = new System.Drawing.Point(12, 345);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(130, 25);
            this.lblTotalTitle.TabIndex = 7;
            this.lblTotalTitle.Text = "Сума замовл:";
            // 
            // lblTotalDisplay
            // 
            this.lblTotalDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalDisplay.AutoSize = true;
            this.lblTotalDisplay.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(90)))), ((int)(((byte)(43)))));
            this.lblTotalDisplay.Location = new System.Drawing.Point(148, 342);
            this.lblTotalDisplay.Name = "lblTotalDisplay";
            this.lblTotalDisplay.Size = new System.Drawing.Size(102, 30);
            this.lblTotalDisplay.TabIndex = 8;
            this.lblTotalDisplay.Text = "0.00 грн";
            // 
            // btnCalculateTotal
            // 
            this.btnCalculateTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculateTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnCalculateTotal.FlatAppearance.BorderSize = 0;
            this.btnCalculateTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculateTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalculateTotal.ForeColor = System.Drawing.Color.White;
            this.btnCalculateTotal.Location = new System.Drawing.Point(330, 342);
            this.btnCalculateTotal.Name = "btnCalculateTotal";
            this.btnCalculateTotal.Size = new System.Drawing.Size(130, 35);
            this.btnCalculateTotal.TabIndex = 9;
            this.btnCalculateTotal.Text = "🧮 Розрахувати";
            this.btnCalculateTotal.UseVisualStyleBackColor = false;
            this.btnCalculateTotal.Click += new System.EventHandler(this.btnCalculateTotal_Click);
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(90)))), ((int)(((byte)(43)))));
            this.btnSaveOrder.FlatAppearance.BorderSize = 0;
            this.btnSaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveOrder.ForeColor = System.Drawing.Color.White;
            this.btnSaveOrder.Location = new System.Drawing.Point(620, 342);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(152, 35);
            this.btnSaveOrder.TabIndex = 10;
            this.btnSaveOrder.Text = "✔️ Провести замовл.";
            this.btnSaveOrder.UseVisualStyleBackColor = false;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCreateOrder.FlatAppearance.BorderSize = 0;
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreateOrder.ForeColor = System.Drawing.Color.White;
            this.btnCreateOrder.Location = new System.Drawing.Point(475, 342);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(130, 35);
            this.btnCreateOrder.TabIndex = 11;
            this.btnCreateOrder.Text = "🗑️ Очистити";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // panelOrderDetails
            // 
            this.panelOrderDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOrderDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(243)))), ((int)(((byte)(238)))));
            this.panelOrderDetails.Controls.Add(this.lblCust);
            this.panelOrderDetails.Controls.Add(this.cbCustomer);
            this.panelOrderDetails.Controls.Add(this.lblBarista);
            this.panelOrderDetails.Controls.Add(this.cbBarista);
            this.panelOrderDetails.Controls.Add(this.lblShop);
            this.panelOrderDetails.Controls.Add(this.cbShop);
            this.panelOrderDetails.Location = new System.Drawing.Point(0, 0);
            this.panelOrderDetails.Name = "panelOrderDetails";
            this.panelOrderDetails.Size = new System.Drawing.Size(784, 50);
            this.panelOrderDetails.TabIndex = 12;
            // 
            // FormOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(784, 400);
            this.Controls.Add(this.panelOrderDetails);
            this.Controls.Add(this.btnCreateOrder);
            this.Controls.Add(this.btnSaveOrder);
            this.Controls.Add(this.btnCalculateTotal);
            this.Controls.Add(this.lblTotalDisplay);
            this.Controls.Add(this.lblTotalTitle);
            this.Controls.Add(this.dgvOrderItems);
            this.Name = "FormOrders";
            this.Text = "Оформлення та облік замовлень";
            this.Load += new System.EventHandler(this.FormOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).EndInit();
            this.panelOrderDetails.ResumeLayout(false);
            this.panelOrderDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCust;
        private System.Windows.Forms.ComboBox cbCustomer;
        private System.Windows.Forms.Label lblBarista;
        private System.Windows.Forms.ComboBox cbBarista;
        private System.Windows.Forms.Label lblShop;
        private System.Windows.Forms.ComboBox cbShop;
        private System.Windows.Forms.DataGridView dgvOrderItems;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalDisplay;
        private System.Windows.Forms.Button btnCalculateTotal;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Panel panelOrderDetails;
    }
}
