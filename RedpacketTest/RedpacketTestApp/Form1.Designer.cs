namespace RedpacketTestApp
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.txtRedPacketId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWebAPIUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClientIds = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numMaxThreadCount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxThreadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(83, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 54;
            this.label2.Text = "Token:";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(380, 525);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(135, 55);
            this.btnRun.TabIndex = 50;
            this.btnRun.Text = "执行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PN";
            this.dataGridViewTextBoxColumn1.HeaderText = "PN";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PNBarcode";
            this.dataGridViewTextBoxColumn2.HeaderText = "Barcode";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "BoxType";
            this.dataGridViewTextBoxColumn3.HeaderText = "BoxType";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Bin";
            this.dataGridViewTextBoxColumn4.HeaderText = "Bin";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "ModeType";
            this.dataGridViewTextBoxColumn5.HeaderText = "ModeType";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Qty";
            this.dataGridViewTextBoxColumn6.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 50;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Station";
            this.dataGridViewTextBoxColumn7.HeaderText = "Station";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Updated";
            this.dataGridViewTextBoxColumn8.HeaderText = "Updated";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 80;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 63;
            this.label1.Text = "WebAPIUrl:";
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(170, 34);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(600, 22);
            this.txtToken.TabIndex = 64;
            this.txtToken.Text = "dc9749db8ad82c1b286a7d83aac1ec2d";
            // 
            // txtRedPacketId
            // 
            this.txtRedPacketId.Location = new System.Drawing.Point(170, 76);
            this.txtRedPacketId.Name = "txtRedPacketId";
            this.txtRedPacketId.Size = new System.Drawing.Size(600, 22);
            this.txtRedPacketId.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 65;
            this.label3.Text = "RedPacketId:";
            // 
            // txtWebAPIUrl
            // 
            this.txtWebAPIUrl.Location = new System.Drawing.Point(170, 122);
            this.txtWebAPIUrl.Name = "txtWebAPIUrl";
            this.txtWebAPIUrl.Size = new System.Drawing.Size(600, 22);
            this.txtWebAPIUrl.TabIndex = 67;
            this.txtWebAPIUrl.Text = "http://localhost:22001/api/RedPackets/ReceiveGroupRedPacket";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(53, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 68;
            this.label4.Text = "ClientIds:";
            // 
            // txtClientIds
            // 
            this.txtClientIds.Location = new System.Drawing.Point(170, 208);
            this.txtClientIds.Multiline = true;
            this.txtClientIds.Name = "txtClientIds";
            this.txtClientIds.Size = new System.Drawing.Size(600, 210);
            this.txtClientIds.TabIndex = 69;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 20);
            this.label5.TabIndex = 70;
            this.label5.Text = "MaxThreadCount:";
            // 
            // numMaxThreadCount
            // 
            this.numMaxThreadCount.Location = new System.Drawing.Point(170, 165);
            this.numMaxThreadCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxThreadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxThreadCount.Name = "numMaxThreadCount";
            this.numMaxThreadCount.Size = new System.Drawing.Size(600, 22);
            this.numMaxThreadCount.TabIndex = 71;
            this.numMaxThreadCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(166, 432);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(353, 20);
            this.label6.TabIndex = 72;
            this.label6.Text = "多个英文逗号相隔,为空时根据红包群用户模拟";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 686);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numMaxThreadCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtClientIds);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWebAPIUrl);
            this.Controls.Add(this.txtRedPacketId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRun);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "RedpacketTestApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxThreadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.TextBox txtRedPacketId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWebAPIUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtClientIds;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numMaxThreadCount;
        private System.Windows.Forms.Label label6;
    }
}

