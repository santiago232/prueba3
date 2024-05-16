using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Final2
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<MovimientoCaja> movimientosCaja = new ObservableCollection<MovimientoCaja>();
        private ObservableCollection<Transaccion> transacciones = new ObservableCollection<Transaccion>();
        private ObservableCollection<Producto> inventario = new ObservableCollection<Producto>();

        public MainWindow()
        {
            InitializeComponent();

            dgTransacciones.ItemsSource = transacciones;
            dgInventario.ItemsSource = inventario;
            dgMovimientos.ItemsSource = movimientosCaja;
        }

        private void cmbTipoTransaccion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cmbTipoTransaccion.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string tipoTransaccion = selectedItem.Content.ToString();
                lblCantidad.Visibility = tipoTransaccion == "Compra" || tipoTransaccion == "Venta" ? Visibility.Visible : Visibility.Collapsed;
                txtCantidad.Visibility = tipoTransaccion == "Compra" || tipoTransaccion == "Venta" ? Visibility.Visible : Visibility.Collapsed;
                txtTipoMoneda.Visibility = tipoTransaccion == "Compra" || tipoTransaccion == "Venta" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void RegistrarTransaccion_Click(object sender, RoutedEventArgs e)
        {
            string tipo = (cmbTipoTransaccion.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(tipo))
            {
                MessageBox.Show("Por favor, seleccione un tipo de transacción.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string descripcion = txtDescripcion.Text;
            decimal monto;
            decimal cantidad;

            if (!decimal.TryParse(txtMonto.Text, out monto))
            {
                MessageBox.Show("Por favor, ingrese un monto válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida para la transacción.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string tipoMoneda = txtTipoMoneda.Text;

            transacciones.Add(new Transaccion { Tipo = tipo, Descripcion = descripcion, Monto = monto, Cantidad = cantidad, TipoMoneda = tipoMoneda });

            txtDescripcion.Clear();
            txtMonto.Clear();
            txtCantidad.Clear();
            txtTipoMoneda.Text = "Q";
        }
       

        private void AgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombreProducto.Text;
            int cantidad;
            decimal precioCompra;
            decimal precioVenta;
            DateTime fechaCaducidad;
            string numeroLote;

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor, ingrese el nombre del producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtCantidadProducto.Text, out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida para el producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtPrecioCompra.Text, out precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show("Por favor, ingrese un precio de compra válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out precioVenta) || precioVenta <= 0)
            {
                MessageBox.Show("Por favor, ingrese un precio de venta válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            fechaCaducidad = dpFechaCaducidad.SelectedDate ?? DateTime.MinValue;
            numeroLote = txtNumeroLote.Text;

            inventario.Add(new Producto { Nombre = nombre, Cantidad = cantidad, PrecioCompra = precioCompra, PrecioVenta = precioVenta, FechaCaducidad = fechaCaducidad, NumeroLote = numeroLote });

            txtNombreProducto.Clear();
            txtCantidadProducto.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            dpFechaCaducidad.SelectedDate = null;
            txtNumeroLote.Clear();
        }
    }
}
