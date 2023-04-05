namespace TextBoxLoggerProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _ = new TextBoxLogger(this.textBoxLog);

            TextBoxLogger.Log("テスト");
        }

        private void buttonInputText_Click(object sender, EventArgs e)
        {
            MessageWrite();

        }

        private void MessageWrite()
        {
            string? input = textBoxInput.Text;
            if (string.IsNullOrWhiteSpace(input)) { return; }
            TextBoxLogger.Log("「{0}」が入力されました", input);
        }

        public async void buttonInputAsync_Click(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {

                await Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    MessageWrite();
                });
            });

        }
    }
}