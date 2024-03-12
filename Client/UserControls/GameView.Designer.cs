namespace Client.UserControls
{
	partial class GameView
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
			seePlayersCardsButton = new Button();
			playerReadyCountLabel = new Label();
			readyButton = new Button();
			yourHandLabel = new Label();
			foldButton = new Button();
			hitButton = new Button();
			scoreLabel = new Label();
			dealerScoreLabel = new Label();
			leaveToLobbySelectionButton = new Button();
			playAgainButton = new Button();
			SuspendLayout();
			// 
			// seePlayersCardsButton
			// 
			seePlayersCardsButton.Location = new Point(846, 217);
			seePlayersCardsButton.Name = "seePlayersCardsButton";
			seePlayersCardsButton.Size = new Size(100, 52);
			seePlayersCardsButton.TabIndex = 0;
			seePlayersCardsButton.Text = "See player's cards";
			seePlayersCardsButton.UseVisualStyleBackColor = true;
			// 
			// playerReadyCountLabel
			// 
			playerReadyCountLabel.AutoSize = true;
			playerReadyCountLabel.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
			playerReadyCountLabel.Location = new Point(431, 14);
			playerReadyCountLabel.Name = "playerReadyCountLabel";
			playerReadyCountLabel.Size = new Size(0, 28);
			playerReadyCountLabel.TabIndex = 1;
			// 
			// readyButton
			// 
			readyButton.Location = new Point(857, 188);
			readyButton.Name = "readyButton";
			readyButton.Size = new Size(75, 23);
			readyButton.TabIndex = 2;
			readyButton.Text = "Ready";
			readyButton.UseVisualStyleBackColor = true;
			// 
			// yourHandLabel
			// 
			yourHandLabel.AutoSize = true;
			yourHandLabel.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
			yourHandLabel.Location = new Point(3, 421);
			yourHandLabel.Name = "yourHandLabel";
			yourHandLabel.Size = new Size(143, 37);
			yourHandLabel.TabIndex = 3;
			yourHandLabel.Text = "Your hand:";
			// 
			// foldButton
			// 
			foldButton.Location = new Point(765, 217);
			foldButton.Name = "foldButton";
			foldButton.Size = new Size(75, 23);
			foldButton.TabIndex = 4;
			foldButton.Text = "Fold";
			foldButton.UseVisualStyleBackColor = true;
			// 
			// hitButton
			// 
			hitButton.Location = new Point(765, 246);
			hitButton.Name = "hitButton";
			hitButton.Size = new Size(75, 23);
			hitButton.TabIndex = 5;
			hitButton.Text = "Hit";
			hitButton.UseVisualStyleBackColor = true;
			// 
			// scoreLabel
			// 
			scoreLabel.AutoSize = true;
			scoreLabel.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			scoreLabel.Location = new Point(3, 472);
			scoreLabel.Name = "scoreLabel";
			scoreLabel.Size = new Size(129, 32);
			scoreLabel.TabIndex = 6;
			scoreLabel.Text = "Your score:";
			// 
			// dealerScoreLabel
			// 
			dealerScoreLabel.AutoSize = true;
			dealerScoreLabel.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
			dealerScoreLabel.Location = new Point(3, 28);
			dealerScoreLabel.Name = "dealerScoreLabel";
			dealerScoreLabel.Size = new Size(151, 32);
			dealerScoreLabel.TabIndex = 7;
			dealerScoreLabel.Text = "Dealer score:";
			// 
			// leaveToLobbySelectionButton
			// 
			leaveToLobbySelectionButton.Location = new Point(846, 464);
			leaveToLobbySelectionButton.Name = "leaveToLobbySelectionButton";
			leaveToLobbySelectionButton.Size = new Size(100, 48);
			leaveToLobbySelectionButton.TabIndex = 8;
			leaveToLobbySelectionButton.Text = "Leave to lobby selection";
			leaveToLobbySelectionButton.UseVisualStyleBackColor = true;
			// 
			// playAgainButton
			// 
			playAgainButton.Location = new Point(846, 435);
			playAgainButton.Name = "playAgainButton";
			playAgainButton.Size = new Size(100, 23);
			playAgainButton.TabIndex = 9;
			playAgainButton.Text = "Play again";
			playAgainButton.UseVisualStyleBackColor = true;
			// 
			// GameView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = Properties.Resources.BlackjackTableBackground;
			BackgroundImageLayout = ImageLayout.Stretch;
			Controls.Add(playAgainButton);
			Controls.Add(leaveToLobbySelectionButton);
			Controls.Add(dealerScoreLabel);
			Controls.Add(scoreLabel);
			Controls.Add(hitButton);
			Controls.Add(foldButton);
			Controls.Add(yourHandLabel);
			Controls.Add(readyButton);
			Controls.Add(playerReadyCountLabel);
			Controls.Add(seePlayersCardsButton);
			Name = "GameView";
			Size = new Size(960, 540);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button seePlayersCardsButton;
		private Label playerReadyCountLabel;
		private Button readyButton;
		private Label yourHandLabel;
		private Button foldButton;
		private Button hitButton;
		private Label scoreLabel;
		private Label dealerScoreLabel;
		private Button leaveToLobbySelectionButton;
		private Button playAgainButton;
	}
}
