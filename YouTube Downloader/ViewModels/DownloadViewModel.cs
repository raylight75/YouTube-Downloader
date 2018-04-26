﻿namespace YouTube.Downloader.ViewModels
{
    using System;

    using YouTube.Downloader.Helpers;
    using YouTube.Downloader.Models;
    using YouTube.Downloader.ViewModels.Interfaces;

    internal class DownloadViewModel : ViewModelBase, IDownloadViewModel
    {
        public event EventHandler DownloadCompleted;

        public IVideoViewModel VideoViewModel { get; private set; }

        private DownloadState _downloadState = DownloadState.Queued;
        public DownloadState DownloadState
        {
            get => _downloadState;

            set
            {
                if (_downloadState == value) return;

                _downloadState = value;
                NotifyOfPropertyChange(() => DownloadState);

                if (_downloadState == DownloadState.Completed)
                {
                    DownloadCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private DownloadProgress _downloadProgress;
        public DownloadProgress DownloadProgress
        {
            get => _downloadProgress;

            set
            {
                if (_downloadProgress == value) return;

                _downloadProgress = value;
                NotifyOfPropertyChange(() => DownloadProgress);
            }
        }

        public Download Download { get; set; }

        public void Initialise(IVideoViewModel videoViewModel)
        {
            VideoViewModel = videoViewModel;
        }

        private bool isPaused;

        public void TogglePause()
        {
            if (isPaused)
            {
                Download.Resume();
            }
            else
            {
                Download.Pause();
            }

            isPaused = !isPaused;
        }
    }
}