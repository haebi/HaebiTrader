namespace HaebiTrader
{
    partial class HaebiMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HaebiMain));
            this.axKHOpenAPI1 = new AxKHOpenAPILib.AxKHOpenAPI();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAccountOverall = new System.Windows.Forms.DataGridView();
            this.btn2 = new System.Windows.Forms.Button();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudOrderPrice = new System.Windows.Forms.NumericUpDown();
            this.nudOrderQuantity = new System.Windows.Forms.NumericUpDown();
            this.txtOriOrderNo = new System.Windows.Forms.TextBox();
            this.txtStockCode = new System.Windows.Forms.TextBox();
            this.cboTradeType = new System.Windows.Forms.ComboBox();
            this.cboOrderType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOrder = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountOverall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrderPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrderQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(999, 12);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(100, 50);
            this.axKHOpenAPI1.TabIndex = 0;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(12, 12);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(119, 34);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "로그인";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.Button_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 482);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(855, 149);
            this.txtLog.TabIndex = 2;
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(139, 12);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(119, 34);
            this.btn1.TabIndex = 1;
            this.btn1.Text = "계좌잔고조회";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.Button_Click);
            // 
            // cboAccount
            // 
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(47, 64);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(121, 20);
            this.cboAccount.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "계좌";
            // 
            // dgvAccountOverall
            // 
            this.dgvAccountOverall.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountOverall.Location = new System.Drawing.Point(12, 99);
            this.dgvAccountOverall.Name = "dgvAccountOverall";
            this.dgvAccountOverall.RowTemplate.Height = 23;
            this.dgvAccountOverall.Size = new System.Drawing.Size(853, 49);
            this.dgvAccountOverall.TabIndex = 5;
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(264, 12);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(119, 34);
            this.btn2.TabIndex = 1;
            this.btn2.Text = "계좌잔고조회(종목)";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.Button_Click);
            // 
            // dgvAccount
            // 
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccount.Location = new System.Drawing.Point(12, 154);
            this.dgvAccount.Name = "dgvAccount";
            this.dgvAccount.RowTemplate.Height = 23;
            this.dgvAccount.Size = new System.Drawing.Size(853, 322);
            this.dgvAccount.TabIndex = 6;
            this.dgvAccount.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvAccount_SortCompare);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOrder);
            this.groupBox1.Controls.Add(this.nudOrderPrice);
            this.groupBox1.Controls.Add(this.nudOrderQuantity);
            this.groupBox1.Controls.Add(this.txtOriOrderNo);
            this.groupBox1.Controls.Add(this.txtStockCode);
            this.groupBox1.Controls.Add(this.cboTradeType);
            this.groupBox1.Controls.Add(this.cboOrderType);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(871, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 532);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // nudOrderPrice
            // 
            this.nudOrderPrice.Location = new System.Drawing.Point(86, 94);
            this.nudOrderPrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudOrderPrice.Name = "nudOrderPrice";
            this.nudOrderPrice.Size = new System.Drawing.Size(120, 21);
            this.nudOrderPrice.TabIndex = 7;
            // 
            // nudOrderQuantity
            // 
            this.nudOrderQuantity.Location = new System.Drawing.Point(86, 67);
            this.nudOrderQuantity.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudOrderQuantity.Name = "nudOrderQuantity";
            this.nudOrderQuantity.Size = new System.Drawing.Size(120, 21);
            this.nudOrderQuantity.TabIndex = 7;
            // 
            // txtOriOrderNo
            // 
            this.txtOriOrderNo.Location = new System.Drawing.Point(86, 147);
            this.txtOriOrderNo.Name = "txtOriOrderNo";
            this.txtOriOrderNo.Size = new System.Drawing.Size(100, 21);
            this.txtOriOrderNo.TabIndex = 6;
            // 
            // txtStockCode
            // 
            this.txtStockCode.Location = new System.Drawing.Point(86, 14);
            this.txtStockCode.Name = "txtStockCode";
            this.txtStockCode.Size = new System.Drawing.Size(100, 21);
            this.txtStockCode.TabIndex = 6;
            this.txtStockCode.Text = "005930";
            // 
            // cboTradeType
            // 
            this.cboTradeType.FormattingEnabled = true;
            this.cboTradeType.Location = new System.Drawing.Point(86, 121);
            this.cboTradeType.Name = "cboTradeType";
            this.cboTradeType.Size = new System.Drawing.Size(121, 20);
            this.cboTradeType.TabIndex = 5;
            // 
            // cboOrderType
            // 
            this.cboOrderType.FormattingEnabled = true;
            this.cboOrderType.Location = new System.Drawing.Point(86, 41);
            this.cboOrderType.Name = "cboOrderType";
            this.cboOrderType.Size = new System.Drawing.Size(121, 20);
            this.cboOrderType.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "원주문번호";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "거래유형";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "주문가격";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "주문수량";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "주문유형";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "종목코드";
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(14, 195);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(192, 32);
            this.btnOrder.TabIndex = 8;
            this.btnOrder.Text = "주문";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.Button_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 251);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "* 삼성전자(005930)";
            // 
            // HaebiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 643);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvAccount);
            this.Controls.Add(this.dgvAccountOverall);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboAccount);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Name = "HaebiMain";
            this.Text = "해비트레이더 키움증권";
            this.Load += new System.EventHandler(this.HaebiMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountOverall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrderPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrderQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.ComboBox cboAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAccountOverall;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudOrderPrice;
        private System.Windows.Forms.NumericUpDown nudOrderQuantity;
        private System.Windows.Forms.TextBox txtOriOrderNo;
        private System.Windows.Forms.TextBox txtStockCode;
        private System.Windows.Forms.ComboBox cboTradeType;
        private System.Windows.Forms.ComboBox cboOrderType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Label label8;
    }
}

