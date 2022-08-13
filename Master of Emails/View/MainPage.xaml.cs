namespace Master_of_Emails;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    public string Text { get; set; }

    void plaza_search(object sender, EventArgs e)
    {
        //plaza_search_result_phone_label.Text = plaza_search_result_phone_label.Text + plaza_search_bar.Text;

    }

    void person_search(object sender, EventArgs e)
    {
        //person_search_result_phone_label.Text = person_search_result_phone_label.Text + person_search_bar.Text;

    }

    void organization_search(object sender, EventArgs e)
    {
        //organization_search_result_phone_label.Text = organization_search_result_phone_label.Text + organization_search_bar.Text;

    }

    private void OnPriorityOneMafClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("PriorityOneMafPage");
    }

    private void OnInconAlertClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("InconAlertPage");
    }

    private void OnZfoClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("ZfoPage");
    }

    private void OnDuressAlarmClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("DuressAlarmPage");
    }

    private void OnScadaClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("ScadaPage");
    }

    private void OnFiberAlertClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("FiberAlertPage");
    }

}

