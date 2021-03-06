﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace uk.ac.dundee.arpond.longRoadHome.View.Controls
{
    public class SimpleMessageBox
    {
        public static MessageBoxResult Show(string title, Window owner)
        {
            return Show(title, string.Empty, MessageBoxButton.OK, owner);
        }

        public static MessageBoxResult Show(string title, string text, Window owner)
        {
            return Show(title, text, MessageBoxButton.OK, owner);
        }


        public static MessageBoxResult Show(string title, string text, MessageBoxButton buttons, Window owner)
        {
            MessageBoxResult result = MessageBoxResult.None;
            SimpleMessageBoxView simpleMessageBox = new SimpleMessageBoxView();

            simpleMessageBox.title.Text = title;
            simpleMessageBox.mainText.Text = text;
            simpleMessageBox.Buttons = buttons;
            simpleMessageBox.Owner = owner;
            simpleMessageBox.SetButtonVisibility();

            simpleMessageBox.ShowDialog();
            result = simpleMessageBox.Result;

            return result;
        }

        public static MessageBoxResult Show(String title, String text, MessageBoxButton buttons, List<String> buttonsText, Window owner)
        {
            MessageBoxResult result = MessageBoxResult.None;
            SimpleMessageBoxView simpleMessageBox = new SimpleMessageBoxView();

            simpleMessageBox.title.Text = title;
            simpleMessageBox.mainText.Text = text;
            simpleMessageBox.Buttons = buttons;
            simpleMessageBox.Owner = owner;
            simpleMessageBox.SetButtonVisibility();

            switch (buttons)
            {
                case MessageBoxButton.OK:
                    if (buttonsText.Count >= 1)
                    {
                        simpleMessageBox.btnOk.Content = buttonsText[0];
                    }
                    break;
                case MessageBoxButton.OKCancel:
                    if (buttonsText.Count >= 4)
                    {
                        simpleMessageBox.btnYes.Content = buttonsText[0];
                        simpleMessageBox.btnNo.Content = buttonsText[1];
                        simpleMessageBox.btnOk.Content = buttonsText[2];
                        simpleMessageBox.btnCancel.Content = buttonsText[3];
                    }
                    break;
                case MessageBoxButton.YesNo:
                    if (buttonsText.Count >= 2)
                    {
                        simpleMessageBox.btnYes.Content = buttonsText[0];
                        simpleMessageBox.btnNo.Content = buttonsText[1];
                    }
                    break;
                case MessageBoxButton.YesNoCancel:
                    if (buttonsText.Count >= 3)
                    {
                        simpleMessageBox.btnYes.Content = buttonsText[0];
                        simpleMessageBox.btnNo.Content = buttonsText[1];
                        simpleMessageBox.btnCancel.Content = buttonsText[2];
                    }
                    break;
            }

            simpleMessageBox.ShowDialog();
            result = simpleMessageBox.Result;

            return result;
        }
    }
}
