﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace OSIRT.UI.AuditLog
{
    public partial class AuditTabControlPanel : UserControl
    {
        private TabbedAuditLogControl tabbedAuditLog;
        private ToolTip toolTip = new ToolTip();
        public TabControl.TabPageCollection AuditTabs => tabbedAuditLog.AuditTabs;

        public AuditTabControlPanel()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Dock = DockStyle.Fill;
            tabbedAuditLog = new TabbedAuditLogControl();
            tabbedAuditLog.TabChanged += TabbedAuditLog_TabChanged;
            tabbedAuditLog.Dock = DockStyle.Fill;
            uiGridViewPanel.Controls.Add(tabbedAuditLog);
            InitialiseTooltips();
            uiPageNumberLabel.Text = tabbedAuditLog.CurrentTab.PagesLeftDescription(); 
            uiPreviousPageButton.Enabled = false;
            uiSearchSelectionComboBox.SelectedIndex = 0;
        }


        public void AddGridToTab(OsirtGridView grid, string tabName)
        {
            tabbedAuditLog.AddGridToTab(grid, tabName);
        }

        public Dictionary<string, string> Tabs()
        {
            return tabbedAuditLog.Tabs();
        }

        private void TabbedAuditLog_TabChanged(object sender, EventArgs e)
        {
            uiPageNumberLabel.Text = tabbedAuditLog.CurrentTab.PagesLeftDescription();
            AuditTab tab = (AuditTab)sender;
            if (tab.Text != "Complete")
            {
                uiNextPageButton.Visible = true;
                uiPreviousPageButton.Visible = true;
                NextPage();
                PreviousPage();
            }
            else
            {
                uiNextPageButton.Visible = false;
                uiPreviousPageButton.Visible = false;
            }

        }

        private void InitialiseTooltips()
        {
            toolTip.SetToolTip(uiPreviousPageButton, "Go back one page");
            toolTip.SetToolTip(uiNextPageButton, "Go forward one page");
            toolTip.SetToolTip(uiFirstPageButton, "Go to first page");
            toolTip.SetToolTip(uiLastPageButton, "Go to last page");
        }

        private void NextPage()
        {
            if (tabbedAuditLog.CurrentTab.CanGoToNextPage())
            {
                tabbedAuditLog.CurrentTab.NextPage();
                uiPreviousPageButton.Enabled = true;
                if (tabbedAuditLog.CurrentTab.Page() == tabbedAuditLog.CurrentTab.NumberOfPages())
                {
                    uiNextPageButton.Enabled = false;
                }
            }
            else
            {
                uiNextPageButton.Enabled = false;
            }

            uiPageNumberLabel.Text = tabbedAuditLog.CurrentTab.PagesLeftDescription();
        }

        private void PreviousPage()
        {
            if (tabbedAuditLog.CurrentTab.CanGoToPreviousPage())
            {
                tabbedAuditLog.CurrentTab.PreviousPage();
                uiNextPageButton.Enabled = true;
                uiFirstPageButton.Enabled = true;

                if (tabbedAuditLog.CurrentTab.Page() == 1)
                {
                    uiPreviousPageButton.Enabled = false;
                    uiFirstPageButton.Enabled = false;
                }

            }
            else
            {
                uiPreviousPageButton.Enabled = false;
                uiFirstPageButton.Enabled = false;
            }

            uiPageNumberLabel.Text = tabbedAuditLog.CurrentTab.PagesLeftDescription();
        }


        private void uiNextPageButton_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        //TODO: Disable buttons when user can't go back/forward anymore.
        //Works for next and previous, but first and last needs work.
        private void uiPreviousPageButton_Click(object sender, EventArgs e)
        {
            PreviousPage();
        }

        private void uiFirstPageButton_Click(object sender, EventArgs e)
        {
            tabbedAuditLog.CurrentTab.FirstPage();
            uiPageNumberLabel.Text = tabbedAuditLog.CurrentTab.PagesLeftDescription();
        }

        private void uiLastPageButton_Click(object sender, EventArgs e)
        {

            tabbedAuditLog.CurrentTab.LastPage();
            uiPageNumberLabel.Text = tabbedAuditLog.CurrentTab.PagesLeftDescription();
        }

        private void uiGridViewPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void uiSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            //string searchText = uiSearchTextBox.Text;
            //tabbedAuditLog.CurrentTab.SearchCurrentPage(searchText);
        }

        private void uiSearchSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uiSearchButton_Click(object sender, EventArgs e)
        {

        }
    }
}
