namespace Client.UserControls
{
	partial class LobbySelector
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
			lobbiesList = new Panel();
			makeLobbyButton = new Button();
			lobbyNameLabel = new Label();
			maxPlayersField = new TextBox();
			nameField = new TextBox();
			maxPlayersLabel = new Label();
			SuspendLayout();
			// 
			// lobbiesList
			// 
			lobbiesList.AutoScroll = true;
			lobbiesList.Location = new Point(3, 3);
			lobbiesList.Name = "lobbiesList";
			lobbiesList.Size = new Size(802, 356);
			lobbiesList.TabIndex = 9;
			// 
			// makeLobbyButton
			// 
			makeLobbyButton.Location = new Point(215, 380);
			makeLobbyButton.Name = "makeLobbyButton";
			makeLobbyButton.Size = new Size(81, 23);
			makeLobbyButton.TabIndex = 17;
			makeLobbyButton.Text = "Make Lobby";
			makeLobbyButton.UseVisualStyleBackColor = true;
			// 
			// lobbyNameLabel
			// 
			lobbyNameLabel.AutoSize = true;
			lobbyNameLabel.Location = new Point(3, 362);
			lobbyNameLabel.Name = "lobbyNameLabel";
			lobbyNameLabel.Size = new Size(42, 15);
			lobbyNameLabel.TabIndex = 13;
			lobbyNameLabel.Text = "Name:";
			// 
			// maxPlayersField
			// 
			maxPlayersField.Location = new Point(109, 380);
			maxPlayersField.Name = "maxPlayersField";
			maxPlayersField.Size = new Size(100, 23);
			maxPlayersField.TabIndex = 16;
			// 
			// nameField
			// 
			nameField.Location = new Point(3, 380);
			nameField.Name = "nameField";
			nameField.Size = new Size(100, 23);
			nameField.TabIndex = 14;
			// 
			// maxPlayersLabel
			// 
			maxPlayersLabel.AutoSize = true;
			maxPlayersLabel.Location = new Point(109, 363);
			maxPlayersLabel.Name = "maxPlayersLabel";
			maxPlayersLabel.Size = new Size(73, 15);
			maxPlayersLabel.TabIndex = 15;
			maxPlayersLabel.Text = "Max Players:";
			// 
			// LobbySelector
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(makeLobbyButton);
			Controls.Add(lobbiesList);
			Controls.Add(lobbyNameLabel);
			Controls.Add(nameField);
			Controls.Add(maxPlayersField);
			Controls.Add(maxPlayersLabel);
			Name = "LobbySelector";
			Size = new Size(808, 443);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Panel lobbiesList;
		private Button makeLobbyButton;
		private Label lobbyNameLabel;
		private TextBox maxPlayersField;
		private TextBox nameField;
		private Label maxPlayersLabel;
	}
}
