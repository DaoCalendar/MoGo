using System;
using System.Windows.Forms;
using MoGo.Optimisation;

namespace MoGo.UI
{
    public partial class ProgressForm : Form
    {
        private const int MinSecondsBetweenUpdates = 1;

        private Optimiser _optimiser;
        private bool _stop;

        private DateTime _startTime;
        private DateTime _lastUpdateTime;

        public ProgressForm()
        {
            InitializeComponent();
        }

        public void Show(Optimiser optimiser)
        {
            optimiser.IterationComplete += HandleIterationComplete;
            optimiser.Complete += HandleComplete;

            _optimiser = optimiser;

            Show();

            _startTime = DateTime.Now;
        }

        private void HandleIterationComplete(object sender, IterationCompleteEventArgs args)
        {
            // Don't update every iteration if they happen very frequently
            if ((DateTime.Now - _lastUpdateTime).TotalSeconds >= MinSecondsBetweenUpdates)
            {
                if (InvokeRequired)
                {
                    BeginInvoke((MethodInvoker) (() => HandleIterationComplete(sender, args)), null);
                }
                else
                {
                    var totalIterations = args.IterationNumber + args.IterationsRemaining;
                    var elapsedTime = DateTime.Now - _startTime;
                    var remainingTime =
                        TimeSpan.FromMilliseconds((elapsedTime.TotalMilliseconds / args.IterationNumber) *
                                                  args.IterationsRemaining);

                    progressBar1.Value = (int) (((double) args.IterationNumber / totalIterations) * 100);
                    iterationLabel.Text = string.Format("Iteration: {0} / {1}", args.IterationNumber, totalIterations);
                    timeRemainingLabel.Text = string.Format("Time remaining: {0:00}:{1:00}:{2:00}", remainingTime.Hours,
                                                            remainingTime.Minutes, remainingTime.Seconds);

                    args.Stop |= _stop;

                    _lastUpdateTime = DateTime.Now;
                }
            }
        }

        private void HandleComplete(object sender, EventArgs e)
        {
            _optimiser.IterationComplete -= HandleIterationComplete;
            _optimiser.Complete -= HandleComplete;

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //_stop = true;

            //DialogResult = DialogResult.Cancel;
        }
    }
}