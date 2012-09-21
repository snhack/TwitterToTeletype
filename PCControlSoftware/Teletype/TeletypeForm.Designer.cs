namespace Teletype
{
	partial class TeletypeForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeletypeForm));
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSendMessage = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtByte = new System.Windows.Forms.TextBox();
			this.btnSendByte = new System.Windows.Forms.Button();
			this.btnConnectToTeletype = new System.Windows.Forms.Button();
			this.txtDebug = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.grpManualTransmission = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnTeletypeOn = new System.Windows.Forms.Button();
			this.btnTeletypeOff = new System.Windows.Forms.Button();
			this.btnBs = new System.Windows.Forms.Button();
			this.btnBell = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.btnLF = new System.Windows.Forms.Button();
			this.btnCR = new System.Windows.Forms.Button();
			this.btnTweet = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.grpTwitter = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTwitterQuery = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.grpManualTransmission.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.grpTwitter.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.Location = new System.Drawing.Point(145, 50);
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(241, 20);
			this.txtMessage.TabIndex = 1;
			this.txtMessage.Text = "abcdefghijklmnopqrstuvwxyz";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(133, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Text Message (Translated)";
			// 
			// btnSendMessage
			// 
			this.btnSendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendMessage.Location = new System.Drawing.Point(392, 48);
			this.btnSendMessage.Name = "btnSendMessage";
			this.btnSendMessage.Size = new System.Drawing.Size(75, 46);
			this.btnSendMessage.TabIndex = 2;
			this.btnSendMessage.Text = "Send";
			this.btnSendMessage.UseVisualStyleBackColor = true;
			this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(75, 105);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Byte (0-255)";
			// 
			// txtByte
			// 
			this.txtByte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtByte.Location = new System.Drawing.Point(145, 102);
			this.txtByte.Name = "txtByte";
			this.txtByte.Size = new System.Drawing.Size(241, 20);
			this.txtByte.TabIndex = 4;
			// 
			// btnSendByte
			// 
			this.btnSendByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendByte.Location = new System.Drawing.Point(392, 100);
			this.btnSendByte.Name = "btnSendByte";
			this.btnSendByte.Size = new System.Drawing.Size(75, 23);
			this.btnSendByte.TabIndex = 5;
			this.btnSendByte.Text = "Send";
			this.btnSendByte.UseVisualStyleBackColor = true;
			this.btnSendByte.Click += new System.EventHandler(this.btnSendByte_Click);
			// 
			// btnConnectToTeletype
			// 
			this.btnConnectToTeletype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConnectToTeletype.Location = new System.Drawing.Point(6, 19);
			this.btnConnectToTeletype.Name = "btnConnectToTeletype";
			this.btnConnectToTeletype.Size = new System.Drawing.Size(461, 23);
			this.btnConnectToTeletype.TabIndex = 0;
			this.btnConnectToTeletype.Text = "Connect to Teletype";
			this.btnConnectToTeletype.UseVisualStyleBackColor = true;
			this.btnConnectToTeletype.Click += new System.EventHandler(this.btnConnectToTeletype_Click);
			// 
			// txtDebug
			// 
			this.txtDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDebug.Location = new System.Drawing.Point(6, 19);
			this.txtDebug.Multiline = true;
			this.txtDebug.Name = "txtDebug";
			this.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDebug.Size = new System.Drawing.Size(461, 135);
			this.txtDebug.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.btnConnectToTeletype);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(473, 48);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Setup";
			// 
			// grpManualTransmission
			// 
			this.grpManualTransmission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpManualTransmission.Controls.Add(this.label5);
			this.grpManualTransmission.Controls.Add(this.btnTeletypeOn);
			this.grpManualTransmission.Controls.Add(this.btnTeletypeOff);
			this.grpManualTransmission.Controls.Add(this.btnBs);
			this.grpManualTransmission.Controls.Add(this.btnBell);
			this.grpManualTransmission.Controls.Add(this.label4);
			this.grpManualTransmission.Controls.Add(this.btnLF);
			this.grpManualTransmission.Controls.Add(this.btnCR);
			this.grpManualTransmission.Controls.Add(this.label1);
			this.grpManualTransmission.Controls.Add(this.txtMessage);
			this.grpManualTransmission.Controls.Add(this.btnSendMessage);
			this.grpManualTransmission.Controls.Add(this.btnSendByte);
			this.grpManualTransmission.Controls.Add(this.label2);
			this.grpManualTransmission.Controls.Add(this.txtByte);
			this.grpManualTransmission.Enabled = false;
			this.grpManualTransmission.Location = new System.Drawing.Point(12, 66);
			this.grpManualTransmission.Name = "grpManualTransmission";
			this.grpManualTransmission.Size = new System.Drawing.Size(473, 157);
			this.grpManualTransmission.TabIndex = 1;
			this.grpManualTransmission.TabStop = false;
			this.grpManualTransmission.Text = "Manual Transmission";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(58, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(81, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Teletype Power";
			// 
			// btnTeletypeOn
			// 
			this.btnTeletypeOn.Location = new System.Drawing.Point(147, 19);
			this.btnTeletypeOn.Name = "btnTeletypeOn";
			this.btnTeletypeOn.Size = new System.Drawing.Size(44, 23);
			this.btnTeletypeOn.TabIndex = 1;
			this.btnTeletypeOn.Text = "On";
			this.btnTeletypeOn.UseVisualStyleBackColor = true;
			this.btnTeletypeOn.Click += new System.EventHandler(this.btnTeletypeOn_Click);
			// 
			// btnTeletypeOff
			// 
			this.btnTeletypeOff.Location = new System.Drawing.Point(197, 19);
			this.btnTeletypeOff.Name = "btnTeletypeOff";
			this.btnTeletypeOff.Size = new System.Drawing.Size(44, 23);
			this.btnTeletypeOff.TabIndex = 2;
			this.btnTeletypeOff.Text = "Off";
			this.btnTeletypeOff.UseVisualStyleBackColor = true;
			this.btnTeletypeOff.Click += new System.EventHandler(this.btnTeletypeOff_Click);
			// 
			// btnBs
			// 
			this.btnBs.Location = new System.Drawing.Point(247, 128);
			this.btnBs.Name = "btnBs";
			this.btnBs.Size = new System.Drawing.Size(44, 23);
			this.btnBs.TabIndex = 9;
			this.btnBs.Text = "BS";
			this.btnBs.UseVisualStyleBackColor = true;
			this.btnBs.Click += new System.EventHandler(this.btnBs_Click);
			// 
			// btnBell
			// 
			this.btnBell.Location = new System.Drawing.Point(297, 128);
			this.btnBell.Name = "btnBell";
			this.btnBell.Size = new System.Drawing.Size(44, 23);
			this.btnBell.TabIndex = 10;
			this.btnBell.Text = "BELL";
			this.btnBell.UseVisualStyleBackColor = true;
			this.btnBell.Click += new System.EventHandler(this.btnBell_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(97, 133);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(42, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Special";
			// 
			// btnLF
			// 
			this.btnLF.Location = new System.Drawing.Point(197, 128);
			this.btnLF.Name = "btnLF";
			this.btnLF.Size = new System.Drawing.Size(44, 23);
			this.btnLF.TabIndex = 8;
			this.btnLF.Text = "LF";
			this.btnLF.UseVisualStyleBackColor = true;
			this.btnLF.Click += new System.EventHandler(this.btnLF_Click);
			// 
			// btnCR
			// 
			this.btnCR.Location = new System.Drawing.Point(147, 128);
			this.btnCR.Name = "btnCR";
			this.btnCR.Size = new System.Drawing.Size(44, 23);
			this.btnCR.TabIndex = 7;
			this.btnCR.Text = "CR";
			this.btnCR.UseVisualStyleBackColor = true;
			this.btnCR.Click += new System.EventHandler(this.btnCR_Click);
			// 
			// btnTweet
			// 
			this.btnTweet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTweet.Location = new System.Drawing.Point(392, 19);
			this.btnTweet.Name = "btnTweet";
			this.btnTweet.Size = new System.Drawing.Size(75, 23);
			this.btnTweet.TabIndex = 2;
			this.btnTweet.Text = "Start";
			this.btnTweet.UseVisualStyleBackColor = true;
			this.btnTweet.Click += new System.EventHandler(this.btnTweet_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.txtDebug);
			this.groupBox3.Location = new System.Drawing.Point(12, 287);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(473, 160);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Debug";
			// 
			// grpTwitter
			// 
			this.grpTwitter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpTwitter.Controls.Add(this.label3);
			this.grpTwitter.Controls.Add(this.txtTwitterQuery);
			this.grpTwitter.Controls.Add(this.btnTweet);
			this.grpTwitter.Enabled = false;
			this.grpTwitter.Location = new System.Drawing.Point(12, 229);
			this.grpTwitter.Name = "grpTwitter";
			this.grpTwitter.Size = new System.Drawing.Size(473, 52);
			this.grpTwitter.TabIndex = 2;
			this.grpTwitter.TabStop = false;
			this.grpTwitter.Text = "Twitter";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(38, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(103, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Twitter search query";
			// 
			// txtTwitterQuery
			// 
			this.txtTwitterQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTwitterQuery.Location = new System.Drawing.Point(147, 19);
			this.txtTwitterQuery.Name = "txtTwitterQuery";
			this.txtTwitterQuery.Size = new System.Drawing.Size(239, 20);
			this.txtTwitterQuery.TabIndex = 1;
			this.txtTwitterQuery.Text = "hakmoc";
			// 
			// TeletypeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(497, 459);
			this.Controls.Add(this.grpTwitter);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.grpManualTransmission);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TeletypeForm";
			this.Text = "Teletype Transmitter";
			this.groupBox1.ResumeLayout(false);
			this.grpManualTransmission.ResumeLayout(false);
			this.grpManualTransmission.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.grpTwitter.ResumeLayout(false);
			this.grpTwitter.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSendMessage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtByte;
		private System.Windows.Forms.Button btnSendByte;
		private System.Windows.Forms.Button btnConnectToTeletype;
		private System.Windows.Forms.TextBox txtDebug;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox grpManualTransmission;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnBell;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnLF;
		private System.Windows.Forms.Button btnCR;
		private System.Windows.Forms.Button btnTweet;
		private System.Windows.Forms.Button btnBs;
		private System.Windows.Forms.GroupBox grpTwitter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtTwitterQuery;
		private System.Windows.Forms.Button btnTeletypeOff;
		private System.Windows.Forms.Button btnTeletypeOn;
		private System.Windows.Forms.Label label5;
	}
}