using Master_of_Emails.Components;
using Master_of_Emails.ViewModels;
using static System.Net.Mime.MediaTypeNames;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class FiberAlertPage : ContentPage
{
    readonly SharedComponents SharedComponents = new();

    public FiberAlertPage(FiberAlertPageViewModel fiberAlertPageViewModel)
    {
        InitializeComponent();
        BindingContext = fiberAlertPageViewModel;
    }

    private void FiberAlertEmailButton_Pressed(object sender, EventArgs e)
    {

        bool CheckFiberAlertInputs = SharedComponents.CheckFiberAlertInputs(selectRegion.SelectedItem, selectMilePost.Text, selectAffectedArea.Text, selectWho.Text,
            selectAmountCut.Text, selectFiberTechnician.Text, selectReportedBy.Text, selectPhoneNumber.Text);

        if (CheckFiberAlertInputs==true)
        {
            SharedComponents.Region = selectRegion.SelectedItem.ToString();
            SharedComponents.MilePost = selectMilePost.Text;
            SharedComponents.AffectedArea = selectAffectedArea.Text;
            SharedComponents.Who = selectWho.Text;
            SharedComponents.AmountCut = selectAmountCut.Text;
            SharedComponents.FiberTechnician = selectFiberTechnician.Text;
            SharedComponents.ReportedBy = selectReportedBy.Text;
            SharedComponents.PhoneNumber = selectPhoneNumber.Text;
            SharedComponents.StartDate = selectStartDate.Text;
            SharedComponents.EndDate = selectEndDate.Text;

            try
            {
                SharedComponents.Subject = "SunWatch Fiber Alert";
                SharedComponents.Body = "<font size=5>" + "<b>" + "****SunWatch Fiber Alert****" + "</b>" + "</font>" + "<br>" + "<br>" +
               "<font size=4>" + "<b>" + "Region: " + "</b>" + SharedComponents.Region + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "Mile Post: " + "</b>" + SharedComponents.MilePost + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "Affected Area: " + "</b>" + SharedComponents.AffectedArea + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "Who: " + "</b>" + SharedComponents.Who + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "Amount Cut: " + "</b>" + SharedComponents.AmountCut + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "Fiber Technician: " + "</b>" + SharedComponents.FiberTechnician + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "Reported By: " + "</b>" + SharedComponents.ReportedBy + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "Phone Number: " + "</b>" + SharedComponents.PhoneNumber + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "Start Date: " + "</b>" + SharedComponents.StartDate + "</font>" + "<br>" +
               "<font size=4>" + "<b>" + "End Date: " + "</b>" + SharedComponents.EndDate + "</font>" + "<br>";

                SharedComponents.Mail = (Outlook.MailItem)SharedComponents.ObjApp.
                CreateItemFromTemplate(SharedComponents.Template);
                SharedComponents.Mail.To = SharedComponents.To;
                SharedComponents.Mail.CC = SharedComponents.Cc;
                SharedComponents.Mail.Subject = SharedComponents.Subject;
                SharedComponents.Mail.HTMLBody = SharedComponents.Body;
                SharedComponents.Mail.Display();
                SharedComponents.Mail = null;
                SharedComponents.Lane = null;
            }

            catch (Exception ex)
            {
                DisplayAlert("Alert", "Close MOE, make sure Outlook is running, and try again. " + ex.Message, "close");
            }
        }

        else
        {
            DisplayAlert("Alert", "One or more inputs are empty", "Close");
        }
    }
}