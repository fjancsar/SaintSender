﻿namespace SaintSender.DesktopUI
{
    using System.Net.NetworkInformation;
    using System.Windows;
    using SaintSender.DesktopUI.ViewModels;
    using SaintSender.DesktopUI.Views;
    using SaintSender.Core.Services;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel;
        private bool isLoggedIn;
        private bool isNetworkAvailable;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            // set DataContext to the ViewModel object
            this.mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = this.mainWindowViewModel;
            this.InitializeComponent();
            this.isLoggedIn = false;
            this.isNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();

            if (!this.isNetworkAvailable)
            {
                MessageBox.Show("No internet connection");

                // load mails from file if authenticated user has saved before
            }
            else
            {
                MessageBox.Show("Yeyy we have internet!");
            }
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            if (this.isLoggedIn)
            {
                this.LoginState.Content = "Login";
                this.isLoggedIn = false;
                Inbox.Visibility = Visibility.Hidden;
                UserControls.Visibility = Visibility.Hidden;
                MessageBox.Show("You have logged out!");
            }
            else
            {
                Login loginWindow = new Login();
                loginWindow.ShowDialog();
                this.LoginState.Content = "Logout";
                this.isLoggedIn = true;
                List<Mail> mails = new InboxService().CreateMails(loginWindow.FullInbox);
                Inbox.Visibility = Visibility.Visible;
                UserControls.Visibility = Visibility.Visible;
                Inbox.ItemsSource = mails;
            }
        }

        private void SendMail_Click(object sender, RoutedEventArgs e)
        {
            SendMail sendMail = new SendMail();
            sendMail.ShowDialog();
        }
    }
}
