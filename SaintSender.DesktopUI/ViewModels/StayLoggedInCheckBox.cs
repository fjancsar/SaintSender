﻿namespace SaintSender.DesktopUI.ViewModels
{
    using SaintSender.Core.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Xml;
    using System.Xml.Linq;

    class StayLoggedInCheckBox
    {
        public void StayLoggedIn(CheckBox loggedInCheckBox, User user)
        {
            MessageBox.Show("You will stay logged in!");
            loggedInCheckBox.Foreground = Brushes.Red;

            SaveLoggedInUser(user);
        }

        private void SaveLoggedInUser(User user)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.NewLineOnAttributes = true;
            using (XmlWriter xmlWriter = XmlWriter.Create("loggedInUser.xml", xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Users");
                xmlWriter.WriteStartElement("User");
                xmlWriter.WriteElementString("Email", user.EmailAdress);
                xmlWriter.WriteElementString("Password", user.Password);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }
        }

        public void LoggedOff(CheckBox loggedInCheckBox, User user)
        {
            loggedInCheckBox.Foreground = Brushes.Green;
            loggedInCheckBox.IsChecked = false;
        }

        public bool IsUserSaved()
        {
            return File.Exists("loggedInUser.xml");
        }

        public User ReadUserDataFromFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("loggedInUser.xml");
            string email = doc.DocumentElement.FirstChild.SelectSingleNode("Email").InnerText;
            string password = doc.DocumentElement.FirstChild.SelectSingleNode("Password").InnerText;
            return new User(email, password);
        }

        public void RemoveUserData()
        {
            File.Delete("loggedInUser.xml");
        }
    }
}