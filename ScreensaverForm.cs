﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using Microsoft.Web.WebView2;
using Microsoft.Web.WebView2.Core;
using System.IO;

namespace Miceli.Web_Page_Screensaver
{
    public partial class ScreensaverForm : Form
    {
        private DateTime StartTime;
        private Timer timer;
        private int currentSiteIndex = -1;
        private GlobalUserEventHandler userEventHandler;
        private bool shuffleOrder;
        private List<string> urls;
        private bool closing = false;

        private PreferencesManager prefsManager = new PreferencesManager();

        private int screenNum;

        [ThreadStatic]
        private static Random random;

        async void InitializeWebView()
        {
            // Specify temporary folder for web view
            string UserDataFolder = Path.Combine(Path.GetTempPath(), "Web-Page-Screensaver");
            var env = await CoreWebView2Environment.CreateAsync(userDataFolder: UserDataFolder);
            await webView1.EnsureCoreWebView2Async(env);
        }

        public ScreensaverForm(int? screenNumber = null)
        {
            userEventHandler = new GlobalUserEventHandler();
            userEventHandler.Event += new GlobalUserEventHandler.UserEvent(HandleUserActivity);

            if (screenNumber == null) screenNum = prefsManager.EffectiveScreensList.FindIndex(s => s.IsPrimary);
            else screenNum = (int)screenNumber;

            InitializeComponent();

            InitializeWebView();

            Cursor.Hide();
        }

        public List<string> Urls
        {
            get
            {
                if (urls == null)
                {
                    urls = prefsManager.GetUrlsByScreen(screenNum);
                }

                return urls;
            }
            set => urls = value;
        }

        private void ScreensaverForm_Load(object sender, EventArgs e)
        {
            if (Urls.Any())
            {
                if (Urls.Count > 1)
                {
                    // Shuffle the URLs if necessary
                    shuffleOrder = prefsManager.GetRandomizeFlagByScreen(screenNum);
                    if (shuffleOrder)
                    {
                        random = new Random();

                        int n = urls.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = random.Next(n + 1);
                            var value = urls[k];
                            urls[k] = urls[n];
                            urls[n] = value;
                        }
                    }

                    // Set up timer to rotate to the next URL
                    timer = new Timer();
                    timer.Interval = prefsManager.GetRotationIntervalByScreen(screenNum) * 1000;
                    timer.Tick += (s, ee) => RotateSite();
                    timer.Start();
                }
               

                // Display the first site in the list
                RotateSite();

                StartTime = DateTime.Now;
            }
        }

        private void BrowseTo(string url)
        {
            if (closing)
                return;

            // Disable the user event handler while navigating
            Application.RemoveMessageFilter(userEventHandler);

            if (string.IsNullOrWhiteSpace(url))
            {
                webView1.Visible = false;
            }
            else
            {
                webView1.Visible = true;
                try
                {
                    Debug.WriteLine($"Navigating: {url}");
                    webView1.Source = new System.Uri(url);
                }
                catch
                {
                    // This can happen if IE pops up a window that isn't closed before the next call to Navigate()
                }
            }
            Application.AddMessageFilter(userEventHandler);
        }

        private void RotateSite()
        {
            currentSiteIndex++;

            if (currentSiteIndex >= Urls.Count)
            {
                currentSiteIndex = 0;
            }

            var url = Urls[currentSiteIndex];

            BrowseTo(url);
        }

        private void HandleUserActivity()
        {
            if (StartTime.AddSeconds(5) > DateTime.Now) return;

            if (prefsManager.CloseOnActivity)
            {
                closing = true;
                Close();
            }
            else
            {
                closeButton.Visible = true;
                Cursor.Show();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class GlobalUserEventHandler : IMessageFilter
    {
        public delegate void UserEvent();

        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_TIMER = 0x0113;
        private const int WM_USER = 0x400;

        // screensavers and especially multi-window apps can get spurrious WM_MOUSEMOVE events
        // that don't actually involve any movement (cursor chnages and some mouse driver software
        // can generate them, for example) - so we record the actual mouse position and compare against it for actual movement.
        private Point? lastMousePos;

        public event UserEvent Event;

        public bool PreFilterMessage(ref Message m)
        {
            if ((m.Msg == WM_MOUSEMOVE) && (this.lastMousePos == null))
            {
                this.lastMousePos = Cursor.Position;
            }

            if (m.Msg >= WM_MOUSEMOVE && m.Msg <= WM_MBUTTONDBLCLK)
            {
                if (((m.Msg == WM_MOUSEMOVE) && (Cursor.Position != this.lastMousePos))
                    || (m.Msg > WM_MOUSEMOVE && m.Msg <= WM_MBUTTONDBLCLK) || m.Msg == WM_KEYDOWN || m.Msg == WM_KEYUP)
                {
                    Event?.Invoke();
                }
            }
            else if (m.Msg != WM_TIMER && m.Msg < WM_USER)
            {
                Event?.Invoke();
            }

            return false;
        }
    }
}