using cMainatoS7.Models;

namespace cMainatoS7.Views;

public partial class vActualizarEliminar : ContentPage
{
    private string url = "http://127.0.0.1/moviles/post.php";

    public vActualizarEliminar(Estudiante datos)
    {
        InitializeComponent();

        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre.ToString();
        txtApellido.Text = datos.apellido.ToString();
        txtEdad.Text = datos.edad.ToString();
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        Actualizar();
    }

    public async void Actualizar()
    {
        try
        {
            var cliente = new HttpClient();

            var url = $"http://127.0.0.1/moviles/post.php?codigo={txtCodigo.Text}&nombre={txtNombre.Text}&apellido={txtApellido.Text}&edad={txtEdad.Text}";

            var respuesta = await cliente.PutAsync(url, null);  // Enviar PUT sin cuerpo (sin contenido en el body)

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "OK");
                await Navigation.PushAsync(new vEstudiante());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el estudiante", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        Eliminar();
    }

    public async void Eliminar()
    {
        try
        {
            var cliente = new HttpClient();

            var deleteUrl = $"http://127.0.0.1/moviles/post.php?codigo={txtCodigo.Text}";

            var respuesta = await cliente.DeleteAsync(deleteUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");
                await Navigation.PushAsync(new vEstudiante());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo eliminar el estudiante", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }
}