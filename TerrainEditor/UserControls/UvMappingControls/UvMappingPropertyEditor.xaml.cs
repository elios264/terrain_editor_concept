﻿using System;
using System.Windows;
using System.Windows.Controls;
using TerrainEditor.Core.Services;
using TerrainEditor.Utilities;
using TerrainEditor.Viewmodels.Terrains;

namespace TerrainEditor.UserControls.UvMappingControls
{
    public partial class UvMappingPropertyEditor : UserControl
    {
        private SelectMappingDialog m_mappingDialog;

        public UvMappingPropertyEditor()
        {
            InitializeComponent();
        }
        private void SelectNewMapping(object sender, RoutedEventArgs e)
        {
            if (m_mappingDialog != null)
            {
                m_mappingDialog.Activate();
                return;
            }

            m_mappingDialog = new SelectMappingDialog();
            m_mappingDialog.Closed += MappingDialogOnClosingFinished;

            ServiceLocator
                .Get<IDialogBoxService>()
                .ShowCustomDialog(m_mappingDialog);
        }
        private void MappingDialogOnClosingFinished(object sender, EventArgs eventArgs)
        {
            DataContext = m_mappingDialog.SelectedMapping ?? DataContext;

            m_mappingDialog.Closed -= MappingDialogOnClosingFinished;
            m_mappingDialog = null;
        }
        private void OnDropMapping(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(UvMapping)))
                DataContext = e.Data.GetData(typeof(UvMapping));
        }
        private void OnEditMapping(object sender, RoutedEventArgs e)
        {
            var info = ServiceLocator.Get<IResourceProviderService>().InfoForResource(DataContext);

            new UvMappingResourceProvider().ShowEditor(DataContext, info);
        }
        private void OnShowInExplorer(object sender, RoutedEventArgs e)
        {
            var resourceProvider = ServiceLocator.Get<IResourceProviderService>() as ResourceExplorer;
            var info = resourceProvider?.InfoForResource(DataContext);

            resourceProvider?.ShowInExplorer(info.RelativePath());
        }
    }
}
