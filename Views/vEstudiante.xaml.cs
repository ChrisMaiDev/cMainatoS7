using cMainatoS7.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;

namespace cMainatoS7.Views;

public partial class vEstudiante : ContentPage
{
    private string url = "http://127.0.0.1:8081/moviles/post.php";
    private readonly HttpClient httpClient = new HttpClient();
    private ObservableCollection<Estudiante> estudiantes = new ObservableCollection<Estudiante>();

    public vEstudiante()
    {
        InitializeComponent();

        Listar();
    }

    public async void Listar()
    {
        var content = await httpClient.GetStringAsync(url);

        List<Estudiante> lstEstudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(content);

        estudiantes = new ObservableCollection<Estudiante>(lstEstudiantes);

        lvEstudiantes.ItemsSource = estudiantes;
    }

    private void btnAbrir_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vInsertarEstudiante());

    }

    private void lvEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objEstudiante = (Estudiante)e.SelectedItem;

        Navigation.PushAsync(new vActualizarEliminar(objEstudiante));

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var button = (Button)sender;
            var codigo = button.CommandParameter.ToString(); // Obtiene el código del estudiante

            using (WebClient cliente = new WebClient())
            {
                string urlEliminar = $"http://127.0.0.1:8081/moviles/post.php?codigo={codigo}";
                cliente.UploadValues(urlEliminar, "DELETE", new NameValueCollection());

                await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");

                // Recargar la lista de estudiantes
                CargarEstudiantes();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }

    private void CargarEstudiantes()
    {
        try
        {
            using (WebClient cliente = new WebClient())
            {
                string url = "http://127.0.0.1:8081/moviles/post.php";
                var json = cliente.DownloadString(url);
                var estudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(json);
                lvEstudiantes.ItemsSource = estudiantes;
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var estudiante = (dynamic)button.CommandParameter; // Obtiene el objeto estudiante

        // Navegar a la vista de actualización con los datos del estudiante
        await Navigation.PushAsync(new vActualizarEstudiante(
            estudiante.codigo.ToString(),
            estudiante.nombre.ToString(),
            estudiante.apellido.ToString(),
            estudiante.edad.ToString()));
    }
}