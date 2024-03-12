namespace Client.UserControls
{
	partial class CardsRow
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			cardsList = new FlowLayoutPanel();
			SuspendLayout();
			// 
			// cardsList
			// 
			cardsList.AutoScroll = true;
			cardsList.AutoSize = true;
			cardsList.Dock = DockStyle.Fill;
			cardsList.Location = new Point(0, 0);
			cardsList.Name = "cardsList";
			cardsList.Size = new Size(150, 150);
			cardsList.TabIndex = 1;
			cardsList.WrapContents = false;
			// 
			// LocalPlayerCardsRow
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(cardsList);
			Name = "LocalPlayerCardsRow";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private FlowLayoutPanel cardsList;
	}
}
