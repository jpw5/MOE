using Master_of_Emails.Table_Repositories;
using Master_of_Emails.Tables;
using Master_of_Emails.ViewModels;
using SQLite;
using System.Security.Claims;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace practice.Pages;

public partial class FiberAlertPage : ContentPage
{
    public Outlook.Application objApp = new();
    public Outlook.MailItem mail = null;
    public string Template = Path.Combine(FileSystem.AppDataDirectory, "Template.msg");

    public string Region;
    public string MilePost;
    public string AffectedArea;
    public string Who;
    public string AmountCut;
    public string FiberTechnician;
    public string ReportedBy;
    public string PhoneNumber;
    public string StartDate;
    public string EndDate;  

	public FiberAlertPage(FiberAlertPageViewModel fiberAlertPageViewModel)
	{
		InitializeComponent();
		BindingContext = fiberAlertPageViewModel;
	}

	private void FiberAlertEmailButton_Pressed(object sender, EventArgs e)
	{
        if(selectRegion.SelectedItem==null)
        {
            DisplayAlert("Alert", "Choose a Region", "Close");
            return;
        }
        else if(selectMilePost.Text=="")
        {
            DisplayAlert("Alert", "Enter Mile Post", "Close");
            return;
        }
        else if (selectAffectedArea.Text == "")
        {
            DisplayAlert("Alert", "Enter Affected Area", "Close");
            return;
        }
        else if (selectWho.Text == "")
        {
            DisplayAlert("Alert", "Enter Who", "Close");
            return;
        }
        else if (selectAmountCut.Text == "")
        {
            DisplayAlert("Alert", "Enter Amount Cut", "Close");
            return;
        }
        else if (selectFiberTechnician.Text == "")
        {
            DisplayAlert("Alert", "Enter FiberTechnician", "Close");
            return;
        }
        else if (selectReportedBy.Text == "")
        {
            DisplayAlert("Alert", "Enter Reported By", "Close");
            return;
        }
        else if (selectPhoneNumber.Text == "")
        {
            DisplayAlert("Alert", "Enter Phone Number", "Close");
            return;
        }
        Region= selectRegion.SelectedItem.ToString();
        MilePost = selectMilePost.Text;
        AffectedArea = selectAffectedArea.Text;
        Who = selectWho.Text;
        AmountCut = selectAmountCut.Text;
        FiberTechnician = selectFiberTechnician.Text;
        ReportedBy = selectReportedBy.Text;
        PhoneNumber = selectPhoneNumber.Text;
        StartDate = selectStartDate.Text;
        EndDate= selectEndDate.Text;

        string To = "ali.shakoor2249@gmail.com";
        string Subject = "SunWatch Fiber Alert";

        string Body = "<font size=5>" + "<b>" + "****SunWatch Fiber Alert****" + "</b>" + "</font>" + "<br>" + "<br>" +
       "<font size=4>" + "<b>" + "Region: " + "</b>" + Region + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "Mile Post: " + "</b>" + MilePost + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "Affected Area: " + "</b>" + AffectedArea + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "Who: " + "</b>" + Who + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "Amount Cut: " + "</b>" + AmountCut + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "Fiber Technician: " + "</b>" + FiberTechnician + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "Reported By: " + "</b>" + ReportedBy + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "Phone Number: " + "</b>" + PhoneNumber + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "Start Date: " + "</b>" + StartDate + "</font>" + "<br>" +
       "<font size=4>" + "<b>" + "End Date: " + "</b>" + EndDate + "</font>" + "<br>";

        mail = (Outlook.MailItem)objApp.CreateItemFromTemplate(Template);
        mail.To = To;
        mail.Subject = Subject;
        mail.HTMLBody = Body;
        mail.Display();
        mail = null;
    }
}