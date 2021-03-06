﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 16:21:56 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.Net.HomeContent;
using AutumnBox.GUI.Util.UI;
using AutumnBox.Logging;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.MVVM
{
    class ViewModelBase : NotificationObject
    {
        public ICommand ShowWindowDialog
        {
            get => _showWindowDialog; set
            {
                _showWindowDialog = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _showWindowDialog;

        public ICommand ShowWindow
        {
            get => _showWindow; set
            {
                _showWindow = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _showWindow;

        public ICommand OpenUrl
        {
            get
            {
                return _openUrl;
            }
            set
            {
                _openUrl = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _openUrl;

        public ICommand OpenGoUrl
        {
            get
            {
                return _openGoUrl;
            }
            set
            {
                _openGoUrl = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _openGoUrl;

        public ICommand RefreshHomeContent
        {
            get
            {
                return _refreshHomeContent;
            }
            set
            {
                _refreshHomeContent = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _refreshHomeContent;

        public string Sentence => Sentences.Next();

        public ViewModelBase()
        {
            OpenUrl = _OpenUrlCommand;
            OpenGoUrl = _OpenGoUrlCommand;
            ShowWindowDialog = _ShowWindowDialogCommnand;
            ShowWindow = _ShowWindowCommand;
            RefreshHomeContent = new MVVMCommand((p) =>
            {
                Task.Run(() =>
                {
                    HomeContentProvider.Do();
                });
            });
        }
        protected virtual bool RaisePropertyChangedOnDispatcher { get; set; } = false;
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (RaisePropertyChangedOnDispatcher)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    base.RaisePropertyChanged(propertyName);
                });
            }
            else
            {
                base.RaisePropertyChanged(propertyName);
            }
        }



        private static readonly ICommand _OpenGoUrlCommand;
        private static readonly ICommand _OpenUrlCommand;
        private static readonly ICommand _ShowWindowDialogCommnand;
        private static readonly ICommand _ShowWindowCommand;
        static ViewModelBase()
        {
            _OpenUrlCommand = new MVVMCommand(_OpenUrl);
            _OpenGoUrlCommand = new MVVMCommand(_OpenGoUrl);
            _ShowWindowDialogCommnand = new MVVMCommand(_ShowWindowDialog);
            _ShowWindowCommand = new MVVMCommand(_ShowWindow);
        }
        private static void _OpenGoUrl(object para)
        {
            var goPre = App.Current.Resources["UrlGoPrefix"] as string;
            try
            {
                Process.Start(goPre + para);
            }
            catch (Exception e)
            {
                SLogger<ViewModelBase>.Warn($"can not open url {para}", e);
            }
        }
        private static void _OpenUrl(object para)
        {
            try
            {
                Process.Start(para as string);
            }
            catch (Exception e)
            {
                SLogger<ViewModelBase>.Warn($"can not open url {para}", e);
            }
        }
        private static void _ShowWindowDialog(object para)
        {
            WinM.X(para.ToString());
        }
        private static void _ShowWindow(object para)
        {
            WinM.D(para.ToString());
        }
    }
}
