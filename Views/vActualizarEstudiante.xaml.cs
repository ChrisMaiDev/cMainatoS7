using System.Collections.Specialized;
using System.Net;

namespace cMainatoS7.Views;

public partial class vActualizarEstudiante : ContentPage
{
    private string codigo;

    public vActualizarEstudiante(string codigo, string nombre, string apellido, string edad)
    {
        InitializeComponent();

        // Rellenar los campos con los datos del estudiante
        this.codigo = codigo;
        txtNombre.Text = nombre;
        txtApellido.Text = apellido;
        txtEdad.Text = edad;
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (WebClient cliente = new WebClient())
            {
                var parametros = new NameValueCollection();
                parametros.Add("codigo", codigo);
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApellido.Text);
                parametros.Add("edad", txtEdad.Text);

                string urlActualizar = $"http://127.0.0.1:8081/moviles/post.php?codigo={codigo}";
                cliente.UploadValues(urlActualizar, "PUT", parametros);

                await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "OK");
                await Navigation.PopAsync(); // Volver a la lista
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }
}