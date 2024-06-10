using System.IO;
using StudentTestPicker.Models;

namespace StudentTestPicker.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class ClassStudents : ContentPage
{

    public string ItemId
    {
        set { LoadStudents(value); }
    }
    public ClassStudents()
    {
        InitializeComponent();
    }


    public void LoadStudents(string classNumber)
    {
        BindingContext = new Models.AllStudents(classNumber);

    }

    private void Add_Student(object sender, EventArgs e)
    {
        ((Models.AllStudents)BindingContext).AddStudent(((Models.AllStudents)BindingContext).getClassNumber(), StudentName.Text, StudentSurname.Text);
        ((Models.AllStudents)BindingContext).LoadStudents(((Models.AllStudents)BindingContext).getClassNumber());
    }

    private async void ReturnButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    private async void DrawStudent(object sender, EventArgs e)
    {
        Models.AllStudents allStudents = (Models.AllStudents)BindingContext;
        if (allStudents.Students.Count == 0)
        {
            await DisplayAlert("Brak uczni�w", "Nie ma dost�pnych uczni�w do losowania.", "OK");
        }
        else
        {
            Models.Student s = allStudents.DrawStudent(allStudents.getClassNumber());
            if (string.IsNullOrEmpty(s.Name))
            {
                await DisplayAlert("Brak ucznia", "Nie ma dost�pnych uczni�w do losowania.", "OK");
            }
            else
            {
                await DisplayAlert("Wylosowany ucze�", $"Dzisiaj pytany b�dzie {s.Name} {s.Surname}", "OK");
            }
        }
    }

    private void DeleteStudent(object sender, EventArgs e)
    {
        ((Models.AllStudents)BindingContext).DeleteStudent(((Models.AllStudents)BindingContext).getClassNumber(), StudentNameD.Text, StudentSurnameD.Text);
        ((Models.AllStudents)BindingContext).LoadStudents(((Models.AllStudents)BindingContext).getClassNumber());
    }


}