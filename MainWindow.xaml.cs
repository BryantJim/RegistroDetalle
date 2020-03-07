using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegistroDetalle.Entidades;
using RegistroDetalle.BLL;
using RegistroDetalle.DAL;

namespace RegistroDetalle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Personas personas = new Personas();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = personas;
            PersonaIdTextBox.Text = "0";
        }

        private void Limpiar()
        {
            PersonaIdTextBox.Text = "0";
            NombreTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty;
            FechaNacDatePicker.SelectedDate = DateTime.Now;

            DetalleDataGrid.ItemsSource = new List<TelefonosDetalle>();
            CargarGrid();
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(PersonaIdTextBox.Text))
            {
                MessageBox.Show("No puede dejar el campo de Persona Id vacio...");
                PersonaIdTextBox.Focus();
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(NombreTextBox.Text))
            {
                MessageBox.Show("No puede dejar el campo de Nombre vacio...");
                NombreTextBox.Focus();
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(DireccionTextBox.Text))
            {
                MessageBox.Show("No puede dejar el campo de Teléfono vacio...");
                DireccionTextBox.Focus();
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text))
            {
                MessageBox.Show("No puede dejar el campo de Cédula vacio...");
                CedulaTextBox.Focus();
                paso = false;
            }

            if(DetalleDataGrid.DataContext == null)
            {
                MessageBox.Show("No puede dejar la persona sin un telefono ni un tipo");
                TelefonoTextBox.Focus();
                paso = false;
            }
            return paso;
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            personas.Telefonos.Add(new TelefonosDetalle
                (
                    TipoTextBox.Text,
                    TelefonoTextBox.Text
                )
                ); 

            CargarGrid();
            TelefonoTextBox.Clear();
            TipoTextBox.Clear();
            TelefonoTextBox.Focus();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if(DetalleDataGrid.Items.Count > 1 && DetalleDataGrid.SelectedIndex < DetalleDataGrid.Items.Count -1)
            {
                personas.Telefonos.RemoveAt(DetalleDataGrid.SelectedIndex);

                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            this.DataContext = null;
            this.DataContext = personas;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool existeEnLaBasedeDatos()
        {
            Personas persona = PersonasBLL.Buscar(personas.PersonaId);
            return (persona != null);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
                return;


            if (Convert.ToInt32(PersonaIdTextBox.Text) == 0)
                paso = PersonasBLL.Guardar(personas);
            else
            {
                if (!existeEnLaBasedeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }else
                    paso = PersonasBLL.Modificar(personas);
            }
            if(paso)
                Limpiar();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
           
            Personas personaAnterior = PersonasBLL.Buscar(Convert.ToInt32(PersonaIdTextBox.Text));
            if (personaAnterior != null)
            {
                personas = personaAnterior;
                CargarGrid();
            }
            else
            {
                MessageBox.Show("persona no encontrada");
                Limpiar();
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

            if (PersonasBLL.Eliminar(Convert.ToInt32(PersonaIdTextBox.Text)))
            {
                MessageBox.Show("Persona eliminada");
                Limpiar();
            }
            else
            {
                MessageBox.Show("no se puede eliminar una Persona que no existe");
            }
        }
    }
}
